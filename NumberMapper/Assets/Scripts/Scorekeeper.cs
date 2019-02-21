using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Scorekeeper : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;
    public int score = 0;
    bool finished = false;

    public void IncrementScore()
    {
        score++;
        if (!finished)
        {
            text.text = "Moves: " + score;
        }
    }

    // temp
    public void SetTextFinish()
    {
        text.text = "You finished in " + score + " moves";
        finished = true;
    }
}
