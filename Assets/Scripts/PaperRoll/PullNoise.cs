using UnityEngine;

public class PullNoise : MonoBehaviour
{
    [SerializeField]
    private PaperRoll paperRoll;

    [SerializeField]
    private AudioSource pullNoiseAudio;
    [SerializeField]
    private float pullNoiseVolumeMultiplier;

    private void Update()
    {
        pullNoiseAudio.volume = paperRoll.pullSpeed * pullNoiseVolumeMultiplier;
    }
}
