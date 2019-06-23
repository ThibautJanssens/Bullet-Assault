using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    [SerializeField]
    public List<ObjectBehaviour> dieBehaviours;
    [SerializeField]
    private Image HealthBar;
    [SerializeField]
    private float fill;
    [SerializeField]
    public UnityEvent OnDie;
    
    private int maxHealth = 100;
    public int currentHealth { get; set; }
    public ListenableInt playerLife = null;

    private void OnEnable() {
        currentHealth = maxHealth;
        IsPlayer();
        fill = currentHealth;
    }

    private void Update() {
        fill = (float) currentHealth / 100;
        HealthBar.fillAmount = fill;    
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Die();
        IsPlayer();
    }

    public void Die() {
        OnDie.Invoke();
        foreach (var item in dieBehaviours) {
            item.Execute(gameObject);
        }
    }

    public void GiveHp(int heal) {
        currentHealth += heal;
        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;
        IsPlayer();
    }

    private void IsPlayer() {
        if (gameObject.CompareTag("Player")) {
            playerLife.Value = currentHealth;
        }
    }
}