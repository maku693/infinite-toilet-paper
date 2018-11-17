using System;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField]
    private TMP_Text countdownText;
    [SerializeField]
    private string readyText;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip readyAudioClip;
    [SerializeField]
    private AudioClip countAudioClip;
    [SerializeField]
    private AudioClip startAudioClip;

    private void OnEnable()
    {
        countdownText.gameObject.SetActive(false);
    }

    public async Task BeginCountdown()
    {
        countdownText.gameObject.SetActive(true);

        audioSource.clip = readyAudioClip;
        audioSource.Play();
        countdownText.text = readyText;
        await Task.Delay(TimeSpan.FromSeconds(1));

        audioSource.clip = countAudioClip;
        var counts = Enumerable.Range(1, 3).Reverse();
        foreach (var count in counts)
        {
            audioSource.Play();
            countdownText.text = count.ToString();
            await Task.Delay(TimeSpan.FromSeconds(1));
        }

        audioSource.clip = startAudioClip;
        audioSource.Play();

        countdownText.gameObject.SetActive(false);
    }
}
