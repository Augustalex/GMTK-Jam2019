using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private EndGameDoor _endGameDoor;
    private Text _text;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
        _endGameDoor = GameObject.FindGameObjectWithTag("EndGameDoor").GetComponent<EndGameDoor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_endGameDoor.HasEnded())
        {
            _text.text = "You survived with " + _endGameDoor.GetScore() + " stuff";
        }
    }
}