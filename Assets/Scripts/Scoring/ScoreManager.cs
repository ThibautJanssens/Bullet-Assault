using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class ScoreManager : ScriptableObject {

    public ListenableInt score;
    public ListenableInt hiScore;

    public void OnEnable() {
        ResetScore();
    }

    public void OnDisable() {
        PlayerPrefs.SetInt(hiScore.name, hiScore.Value);
    }

    public void AddScore(int delta) {
        score.Value += delta;
        UpdateHiScore();
    }

    public void UpdateHiScore() {
        if (score.Value > hiScore.Value) {
            hiScore.Value = score.Value;
        }
    }

    public void ResetScore() {
        score.Value = 0;
        hiScore.Value = PlayerPrefs.GetInt(hiScore.name, 0);
    }

    public void ResetHiScore() {
        hiScore.Value = 0;
    }
}