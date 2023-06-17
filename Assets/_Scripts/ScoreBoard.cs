using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    TMP_Text text;
    int score;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
        text.text = score.ToString();
    }
    public void IncreaseScore(int ammountToIncrease) 
    {
        score += ammountToIncrease;
        text.text = score.ToString();
    }

    public void PrintScore() 
    {
        Debug.Log($"Score is {score}");
    }

   
}
