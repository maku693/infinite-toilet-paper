using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PaperRoll : MonoBehaviour
{
    private const float inch2cm = 2.54F;

    public float pulledLength;
    public float pullSpeed;
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
    private AudioSource pullNoiseAudio;
    [SerializeField]
    private float pullNoiseVolumeMultiplier;

    private void Enable()
    {
        isStopped = false;
    }

    private void Update()
    {
        Pull();
        PlayCoverNoise();
        SetPullNoiseVolume();
    }

    private void Pull()
    {
        if (isStopped) { return; }

        var pullProgress = pullSpeed * pullSpeedMultiplier * Time.deltaTime;
        if (pullProgress < pullProgressTorelance)
        {
            isStopped = true;
            return;
        }
        pulledLength += pullProgress;
        pullSpeed *= pullSpeedDamping;
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

    private void SetPullNoiseVolume()
    {
        pullNoiseAudio.volume = pullSpeed * pullNoiseVolumeMultiplier;
    }
}
