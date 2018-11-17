using System.Collections.Generic;
using UnityEngine;

public class CoverNoise : MonoBehaviour
{
    [SerializeField]
    private PaperRoll paperRoll;

    [SerializeField]
    private AudioSource coverNoiseAudio;
    [SerializeField]
    private List<AudioClip> coverNoises;
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
            coverNoiseAudio.clip = coverNoises[Random.Range(0, coverNoises.Count)];
            coverNoiseAudio.Play();
            lastCoverNoisePlayedLength = paperRoll.pulledLength;
        }
    }
}
