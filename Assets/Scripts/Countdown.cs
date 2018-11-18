using System;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField]
    private PaperRoll paperRoll;
    [SerializeField]
    private PullBeep pullBeep;

    [SerializeField]
    private GameObject countdownUI;
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
        countdownUI.SetActive(false);
    }

    public async Task Run()
    {
        paperRoll.gameObject.SetActive(false);
        paperRoll.gameObject.SetActive(true);
        pullBeep.gameObject.SetActive(false);
        pullBeep.gameObject.SetActive(true);

        countdownUI.SetActive(true);

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

        countdownUI.SetActive(false);
    }
}
