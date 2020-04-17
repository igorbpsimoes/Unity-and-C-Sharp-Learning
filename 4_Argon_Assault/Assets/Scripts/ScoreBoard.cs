using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

    private int score;
    private Text scoreText;
    // Start is called before the first frame update
    void Start() {
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
    }

    public void ScoreHit(int scoreIncrease) {
        score += scoreIncrease;
        scoreText.text = score.ToString();
    }
}
