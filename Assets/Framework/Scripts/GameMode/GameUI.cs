using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    public Text scoreText;
    public Text levelText;
    public Text livesText;

    private void Update()
    {
        scoreText.text = "Score: " + GameData.score;
        levelText.text = "Level: " + GameData.level;
        livesText.text = "Lives: " + GameData.lives;
    }

}
