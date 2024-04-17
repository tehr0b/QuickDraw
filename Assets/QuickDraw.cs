using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QuickDraw : MonoBehaviour
{
    [SerializeField] Image image;
    
    [FormerlySerializedAs("offColor")] [SerializeField] Color waitingColor = Color.gray;

    [SerializeField] Color readyColor = Color.yellow;

    [SerializeField] Color doneColor = Color.green;

    [SerializeField] float minSeconds = 8.0f;

    [SerializeField] float maxSeconds = 15.0f;

    [SerializeField] TextMeshProUGUI timeOutput;

    [SerializeField] KeyCode input = KeyCode.Space;

    [SerializeField] Button beginButton;

    enum State
    {
        Idle,
        Waiting,
        Ready
    }

    private State state = State.Idle;

    private float lastStart;
        
    [UsedImplicitly]
    public async Task Play()
    {
        beginButton.enabled = false;
        timeOutput.text = "";
        state = State.Waiting;
        image.color = waitingColor;
        await Task.Delay(TimeSpan.FromSeconds(Random.Range(minSeconds, maxSeconds)));
        state = State.Ready;
        image.color = readyColor;
        lastStart = Time.time;
    }

    void Update()
    {
        if (Input.GetKeyDown(input))
        {
            if (state == State.Ready)
            {
                ProcessReadyState();
            }
            else if (state == State.Idle)
            {
                Play();
            }
        }
    }

    private void ProcessReadyState()
    {
        state = State.Idle;
        image.color = doneColor;
        
        var score = Time.time - lastStart;
        timeOutput.text = $"Score: {score:##.000} seconds";
        
        beginButton.enabled = true;
    }
    
}
