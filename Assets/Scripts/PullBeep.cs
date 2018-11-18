using System.Collections.Generic;
using UnityEngine;

public class PullBeep : MonoBehaviour
{
    [SerializeField]
    private PaperRoll paperRoll;

    [SerializeField]
    private new AudioSource audio;
    [SerializeField]
    private float coverNoisePlayRate;

    private float lastCoverNoisePlayedLength;

    private void OnEnable()
    {
        lastCoverNoisePlayedLength = 0;
    }

    private void Update()
    {
        var pulledLength = paperRoll.manualPulledLength;
        // Normalize pulledLength
        pulledLength -= pulledLength % coverNoisePlayRate;

        if (pulledLength - lastCoverNoisePlayedLength > 0)
        {
            audio.Play();
            lastCoverNoisePlayedLength = pulledLength;
        }
    }
}
