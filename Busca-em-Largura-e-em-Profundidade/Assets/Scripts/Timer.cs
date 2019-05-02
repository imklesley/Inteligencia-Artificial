using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text text;
    private float theTime;
    public float speed = 1;
    private bool playing;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        if (playing != true) return;

        theTime += Time.deltaTime * speed;

        var minutes = Mathf.Floor((theTime % 3600) / 60).ToString("00");
        var seconds = (theTime % 60).ToString("00");

        text.text = minutes + ":" + seconds;
    }

    public void ClickPlay()
    {
        playing = true;
    }

    public void ClickStop()
    {
        playing = false;
    }
}