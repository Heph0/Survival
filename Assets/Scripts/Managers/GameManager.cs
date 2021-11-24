using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Diagnostics;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text winLoseText;
    public Text timerText;
    public GameObject winLoseMenu;

    private TimeSpan winConditionTimer;
    private string result;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        winLoseMenu.gameObject.SetActive(false);
        result = "";
        winConditionTimer = new TimeSpan(0,0,30);    //30 seconds
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        winLoseMenu.gameObject.SetActive(Victory());
        player.GetComponent<Player>().isVictory = Victory();
        winLoseText.text = result;
    }

    bool Victory()
    {
        //if attacked by enemy
        if (player.GetComponent<Player>().isAlive)
        {
            //if timer victory
            string tempStrTimer = timerText.text.Substring(9, timerText.text.Length-9);
            //https://docs.microsoft.com/en-us/dotnet/api/system.timespan.parse?view=net-5.0#System_TimeSpan_Parse_System_String_
            TimeSpan tempTsTimer = TimeSpan.Parse(tempStrTimer);
            //https://docs.microsoft.com/en-us/dotnet/api/system.timespan.compare?view=net-5.0#System_TimeSpan_Compare_System_TimeSpan_System_TimeSpan_
            result = "VICTORY";
            return TimeSpan.Compare(tempTsTimer, winConditionTimer) >= 0;
        }
        result = "DEFEAT";
        return true;
    }
}
