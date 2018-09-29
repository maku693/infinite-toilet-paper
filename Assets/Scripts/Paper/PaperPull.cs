using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperPull : MonoBehaviour
{
    private const float inch2cm = 2.54F;
    private float pulledLength;
    [SerializeField]
    private string pulledLengthFormat;
    private string pulledLengthString => pulledLength.ToString(pulledLengthFormat) + "m";

    private bool isPulled;
    private Vector2 pullStart;
    private float pullStartTime;
    private float pullSpeed;

    [SerializeField]
    private float pullSpeedMultiplier;
    [SerializeField]
    private float pullSpeedFriction;
    private float pullSpeedDamping => 1.0F - pullSpeedFriction;

    [SerializeField]
    private float pullProgressTorelance;
    private bool isStopped;

    [SerializeField]
    private AudioSource coverNoiseAudio;
    [SerializeField]
    private List<AudioClip> coverNoises;
    [SerializeField]
    private float coverNoisePlayRate;
    private float lastCoverNoisePlayedLength;

    [SerializeField]
    private Text pulledLengthText;

    private void Enable()
    {
        isPulled = false;
        isStopped = false;
    }

    private void Start()
    {
        // For demonstration
        pullSpeed = 10F;
    }

    private void Update()
    {
        Pull();
        PlayCoverNoise();
        UpdatePulledLengthText();
    }

    private void Pull()
    {
        if (isStopped) { return; }

        GetPullInput();

        var pullProgress = pullSpeed * pullSpeedMultiplier * Time.deltaTime;
        if (pullProgress < pullProgressTorelance)
        {
            isStopped = true;
            return;
        }
        pulledLength += pullProgress;
        pullSpeed *= pullSpeedDamping;
    }

    private void GetPullInput()
    {
        if (isPulled) { return; }

        if (Input.touchCount == 0) { return; }

        var touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            pullStart = touch.position;
            pullStartTime = Time.time;
            return;
        }

        if (touch.phase != TouchPhase.Ended) { return; }
        isPulled = true;

        var pullDistance = (touch.position - pullStart).magnitude;
        var pullDistanceInCm = pullDistance / Screen.dpi * inch2cm;
        var pullDuration = Time.time - pullStartTime;
        pullSpeed = pullDistanceInCm / pullDuration;
    }

    private void PlayCoverNoise()
    {
        if (pulledLength - lastCoverNoisePlayedLength > coverNoisePlayRate)
        {
            coverNoiseAudio.clip = coverNoises[Random.Range(0, coverNoises.Count)];
            coverNoiseAudio.Play();
            lastCoverNoisePlayedLength = pulledLength;
        }
    }

    private void UpdatePulledLengthText()
    {
        pulledLengthText.text = pulledLengthString;
    }
}
