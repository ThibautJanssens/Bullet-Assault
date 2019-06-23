using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class SceneLoader : ScriptableObject {

    public GlobalString mainMenuName;
    public GlobalString level1Name;
    public GlobalString level2Name;
    public GlobalString level3Name;
    public GlobalString gameOverName;

    //private int score;

    public void MainMenu() {
        SceneManager.LoadScene(mainMenuName.Value);
    }

    public void StartGame() {
        SceneManager.LoadScene(level1Name.Value);
    }

    public void StartLevel2() {
        //score = PlayerPrefs.GetInt("PlayerScore");
        SceneManager.LoadScene(level2Name.Value);
    }

    public void StartLevel3() {
        //score = PlayerPrefs.GetInt("PlayerScore");
        SceneManager.LoadScene(level3Name.Value);
    }

    public void StartGameOver() {
        SceneManager.LoadScene(gameOverName.Value);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
