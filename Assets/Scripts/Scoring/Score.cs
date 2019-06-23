using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

    public int points;
    public ScoreManager scoreManager;

    public void ScorePoints() {
        scoreManager.AddScore(points);
    }
}
