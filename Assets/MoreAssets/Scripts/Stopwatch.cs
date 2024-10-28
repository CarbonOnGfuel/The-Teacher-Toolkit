using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stopwatch : MonoBehaviour
{
    public TMP_Text stopwatchText;
    public Button startButton;
    public Button pauseButton;
    public Button resetButton;

    private bool isStopwatchRunning = false;
    private float stopwatchTime = 0f;
    private Coroutine stopwatchCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        UpdateStopwatchDisplay();
        pauseButton.interactable = false;
        resetButton.interactable = false;
    }

    public void StartStopwatch()
    {
        if (!isStopwatchRunning)
        {
            isStopwatchRunning = true;
            startButton.interactable = false;
            pauseButton.interactable = true;
            resetButton.interactable = true;

            stopwatchCoroutine = StartCoroutine(UpdateStopwatch());
        }
    }

    public void PauseStopwatch()
    {
        if (isStopwatchRunning)
        {
            isStopwatchRunning = false;
            startButton.interactable = true;
            pauseButton.interactable = false;

            StopCoroutine(stopwatchCoroutine);
        }
    }

    public void ResetStopwatch()
    {
        isStopwatchRunning = false;
        stopwatchTime = 0f;
        UpdateStopwatchDisplay();

        startButton.interactable = true;
        pauseButton.interactable = false;
        resetButton.interactable = false;

        if (stopwatchCoroutine != null)
        {
            StopCoroutine(stopwatchCoroutine);
        }
    }

    private IEnumerator UpdateStopwatch()
    {
        while (isStopwatchRunning)
        {
            yield return null; // Update every frame
            stopwatchTime += Time.deltaTime;  // Accumulate time with deltaTime
            UpdateStopwatchDisplay();
        }
    }

    private void UpdateStopwatchDisplay()
    {
        int hours = Mathf.FloorToInt(stopwatchTime / 3600);
        int minutes = Mathf.FloorToInt((stopwatchTime % 3600) / 60);
        int seconds = Mathf.FloorToInt(stopwatchTime % 60);
        int milliseconds = Mathf.FloorToInt((stopwatchTime * 1000) % 1000);

        stopwatchText.text = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}", hours, minutes, seconds, milliseconds);
    }
}
