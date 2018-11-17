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
        paperRoll.onStop += () =>
        {
            lastCoverNoisePlayedLength = 0;
        };
    }

    private void Update()
    {
        if (paperRoll.pulledLength - lastCoverNoisePlayedLength > coverNoisePlayRate)
        {
            audio.Play();
            lastCoverNoisePlayedLength = paperRoll.pulledLength;
        }
    }
}
