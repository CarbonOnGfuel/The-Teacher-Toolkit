using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TMP_Dropdown hoursDropdown;
    public TMP_Dropdown minutesDropdown;
    public TMP_Dropdown secondsDropdown;

    public TMP_Text TimerText;
    public GameObject InvalidTimeText;
    public GameObject pauseBtn;
    public GameObject startBtn;

    public Button startButton;
    public Button pauseButton;
    public Button stopButton;
    public Button okButton;
    public Button resetButton;

    public AudioSource alarmSound;

    private int totalSeconds;
    private float remainingTime;
    private bool isTimerRunning = false;
    private bool isPaused = false;

    private Coroutine timerCoroutine;

    void Start()
    {
        startButton.onClick.AddListener(StartOrResumeTimer);
        pauseButton.onClick.AddListener(PauseTimer);
        stopButton.onClick.AddListener(StopTimer);
        okButton.onClick.AddListener(UpdateTimerDisplayWithDropdownValues);
        resetButton.onClick.AddListener(ResetDropdownValues);

        InvalidTimeText.SetActive(false);

        pauseButton.interactable = true;
        stopButton.interactable = false;
    }

    public void StartOrResumeTimer()
    {
        // If the timer is paused, resume it
        if (isPaused)
        {
            timerCoroutine = StartCoroutine(RunTimer());
            isTimerRunning = true;
            isPaused = false;
            EnablePause();
        }
        else if (!isTimerRunning && totalSeconds > 0)
        {
            remainingTime = totalSeconds;  // Set the remaining time
            timerCoroutine = StartCoroutine(RunTimer());
            isTimerRunning = true;
            pauseButton.interactable = true;
            stopButton.interactable = true;
            EnablePause();
        }
        else if (totalSeconds <= 0)
        {
            ShowInvalidText();
            pauseButton.enabled = false;
            startButton.enabled = true;
            return;
        }
    }

    private void EnablePause()
    {
        pauseBtn.SetActive(true);
        startBtn.SetActive(false);
    }

    private void DisablePause()
    {
        pauseBtn.SetActive(false);
        startBtn.SetActive(true);
    }

    private void EnableStart()
    {
        startBtn.SetActive(true);
    }

    private void ShowInvalidText()
    {
        InvalidTimeText.SetActive(true);
        Invoke("HideInvalidText", 3f);
    }

    private void HideInvalidText()
    {
        InvalidTimeText.SetActive(false);
    }

    private IEnumerator RunTimer()
    {
        while (remainingTime > 0)
        {
            UpdateTimerDisplay();
            yield return new WaitForSeconds(1f);
            remainingTime--;
        }

        // Timer has reached 0
        remainingTime = 0; // Ensure remaining time is set to 0
        UpdateTimerDisplay(); // Update the display to show 00:00:00

        // Play the alarm sound
        if (alarmSound != null)
        {
            alarmSound.Play();
        }

        // Reset the button states
        isTimerRunning = false; // Set the timer running state to false
        pauseButton.interactable = false; // Disable pause button
        startButton.interactable = true; // Enable start button for new timer

    }

    public void PauseTimer()
    {
        if (isTimerRunning && timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            isTimerRunning = false;
            isPaused = true;
            DisablePause();
        }
    }

    public void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
        remainingTime = 0;
        UpdateTimerDisplay();
        isTimerRunning = false;
        isPaused= false;
        pauseButton.interactable = false;
        stopButton.interactable = false;
        startButton.interactable = true;
        EnableStart();

        if (alarmSound.isPlaying)
        {
            alarmSound.Stop();
        }
    }

    public void UpdateTimerDisplayWithDropdownValues()
    {
        int hours = hoursDropdown.value;
        int minutes = minutesDropdown.value;
        int seconds = secondsDropdown.value;

        totalSeconds = (hours * 3600) + (minutes * 60) + seconds;  // Calculate the total seconds

        // Temporarily update the remainingTime to show in the label
        remainingTime = totalSeconds;
        UpdateTimerDisplay(); // Update the timer label to reflect the selected time
    }

    private void UpdateTimerDisplay()
    {
        int hours = Mathf.FloorToInt(remainingTime / 3600);
        int minutes = Mathf.FloorToInt((remainingTime % 3600) / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        TimerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
    }

    public void ResetDropdownValues()
    {
        // Reset the dropdown values
        hoursDropdown.value = 0;
        minutesDropdown.value = 0;
        secondsDropdown.value = 0;

        // Update the timer label to reflect the reset values
        totalSeconds = 0;
        remainingTime = 0;
        UpdateTimerDisplay();  // Clear the timer display after resetting
    }


}
