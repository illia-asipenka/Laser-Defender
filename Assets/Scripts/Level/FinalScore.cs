using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalScore : MonoBehaviour
{
    
    void Start()
    {
        TextMeshProUGUI myScore = GetComponent<TextMeshProUGUI>();
        myScore.text = ScoreKeeper.playerPoints.ToString();
        ScoreKeeper.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
