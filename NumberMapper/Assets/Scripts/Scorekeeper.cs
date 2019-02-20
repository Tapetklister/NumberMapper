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

    public void IncrementScore()
    {
        score++;
        text.text = "Moves: " + score;
    }
}
