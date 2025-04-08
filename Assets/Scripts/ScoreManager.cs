using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    private int score = 0;

    public void Addscore(int value){
        // 引数として受け取った値をスコアとして加算し，テキストを更新
        score += value;
        scoreText.text = "Score : " + score;
    }
}
