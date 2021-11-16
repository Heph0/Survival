using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Diagnostics;

using UnityEngine.UI;

public class Platform : MonoBehaviour
{
    //https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.stopwatch?view=net-5.0
    private Stopwatch stopWatch;
    //https://docs.unity3d.com/2017.3/Documentation/ScriptReference/UI.Text-text.html
    public Text timerHUD;

    // Start is called before the first frame update
    void Start()
    {
        stopWatch = new Stopwatch();
        timerHUD.text = "RunTime: 00:00:00.00";
    }

    // Update is called once per frame
    void Update()
    {
        //https://docs.microsoft.com/en-us/dotnet/api/system.timespan?view=net-5.0
        TimeSpan ts = stopWatch.Elapsed;

        // Format and display the TimeSpan value.
        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
        timerHUD.text = "RunTime: " + elapsedTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        //starts the clock
        if (collision.gameObject.name == "Player")
        {
            stopWatch.Start();
        }
    }
    void OnCollisionExit(Collision collision)
    {
        //stops the clock
        if (collision.gameObject.name == "Player")
        {
            stopWatch.Stop();
        }
    }
}
