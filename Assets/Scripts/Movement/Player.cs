using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    private float speedModifier;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float jumpForce = 11.0f;
    private int damageCollisionEnemy = 35;
    private int healthRecovery = 40;

    private CharacterController controller; 
    private Vector3 moveVector;
    private float inputDirection;   // x du vecteur de movement
    private float verticalVelocity; // y du vecteur de movement
    private bool doubleJumpUsed = false;

    public SceneLoader sceneLoader;
    private Health health;

    private float fallYCoord;

    //Animation
    private Animator anim;
    private bool grounded;
    private bool facingRight = true;

    // Use this for initialization
    void Start () {
        //LevelManager.Instance.Win();
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        health = GetComponent<Health>();
        fallYCoord = LevelManager.Instance.GetCleanerYHeight();
    }

	// Update is called once per frame
	void Update () {

        if (transform.position.y < fallYCoord) {
            health.Die();
        }

        grounded = IsControllerGrounded();
        anim.SetBool("Ground", grounded);

        if (grounded) {
            doubleJumpUsed = false;
            verticalVelocity = 0;
        }
        //dire a quel vitesse on tombe ou monte
        anim.SetFloat("vSpeed", controller.velocity.y);
        inputDirection = Input.GetAxis("Horizontal") * speedModifier;
        //envoie la valeur de la vitesse a l'animator pour changer d'animation
        anim.SetFloat("Speed", Mathf.Abs(inputDirection));

        //bouge a gauche et ne regarde pas a gauche, on flip
        if (inputDirection > 0 && !facingRight) {
            Flip();
        }
        //on regarde a droite mais ne va pas a droite, on flip
        else if (inputDirection < 0 && facingRight) {
            Flip();
        }

        if ((grounded || !doubleJumpUsed) && Input.GetButtonDown("Jump")) {
            anim.SetBool("Ground", false);
            verticalVelocity = jumpForce;

            if(!grounded && !doubleJumpUsed) {
                verticalVelocity = jumpForce;
                doubleJumpUsed = true;
            }
        }
        if (!grounded) {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        
        moveVector = new Vector3(inputDirection, verticalVelocity, 0);
        controller.Move(moveVector * Time.deltaTime);
    }

    //si le colllider du joueur se hurte a un objet (ex: enemy, mur, sol, etc.)
    private void OnControllerColliderHit(ControllerColliderHit hit) {
        //en fonction de l'objet qu'on touche
        switch (hit.gameObject.tag) {
            case "Life":
                Health healing = gameObject.GetComponent<Health>();
                healing.GiveHp(healthRecovery);
                Destroy(hit.gameObject);
                break;
          case "Enemy":
                if(hit.gameObject.layer.Equals("Bullet")) {
                    break;
                }
                else {
                    Health health = gameObject.GetComponent<Health>();
                    health.TakeDamage(damageCollisionEnemy);
                    Health enemyHealth = hit.gameObject.GetComponent<Health>();
                    enemyHealth.TakeDamage(100);
                    break;
                }
            case "Ammo":
                Weapon weap = gameObject.GetComponentInChildren<WeaponSwitching>().currentEquipedWeapon;
                weap.PickUp();
                Destroy(hit.gameObject);    
                break;
            case "Wall":
                if (Input.GetButtonDown("Jump")) {
                    verticalVelocity = jumpForce;
                    doubleJumpUsed = false;
                }
                break;
            default:
                break;
        }
    }

    //vérifie si le collider du joueur est au sol pour éviter les lag du a la physic de unity
    private bool IsControllerGrounded() {
        Vector3 leftRayStart;
        Vector3 rightRayStart;

        leftRayStart = controller.bounds.center;
        rightRayStart = controller.bounds.center;
        leftRayStart.x -= controller.bounds.extents.x; //bounds.extends.x est le radius en X du collider
        rightRayStart.x += controller.bounds.extents.x;

        Debug.DrawRay(leftRayStart, Vector3.down, Color.red);
        Debug.DrawRay(rightRayStart, Vector3.down, Color.green);

        //Raycast va nous servir pour obtenir un boolean, est-ce qu'on touche quelque chose? taille du raycast= taille du collider / 2 + un buffer
        if (Physics.Raycast(rightRayStart, Vector3.down, (controller.height / 2) + 0.1f)) {
            return true;
        }
        if (Physics.Raycast(leftRayStart, Vector3.down, (controller.height / 2) + 0.1f)) {
            return true;
        }
        return false;
    }

    //lorsque le personnage change de direction, on flip le monde plutot que les animations du personnage
    private void Flip() {
        facingRight = !facingRight;
        //récupère la valeur X du scale de l'objet pour l'inverser sur l'axe des X
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        //empêche la barre de vie de se retourner avec le joueur
        transform.GetChild(0).transform.localScale = new Vector3(theScale.x / 100f, theScale.y / 100f, theScale.z);
    }

    public bool IsFacingRight() {
        return facingRight;
    }
}
