using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public static bool gamePaused = false;
    public GameObject pauseMenuUI;

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (gamePaused) {
                Resume();
            }
            else {
                Pause();
            }
        }
	}

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; //remet le jeu en vitesse normale
        gamePaused = false;
    }

    private void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; //freeze le jeu
        gamePaused = true;
    }

    public void LoadMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
