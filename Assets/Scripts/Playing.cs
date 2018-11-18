using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Playing : MonoBehaviour
{
    [SerializeField]
    private PaperRoll paperRoll;

    [SerializeField]
    private GameObject playingUI;

    [SerializeField]
    private PulledLengthText pulledLengthText;
    [SerializeField]
    private PullHandler pullHandler;

    private void OnEnable()
    {
        playingUI.SetActive(false);
    }

    public async Task Run()
    {
        playingUI.SetActive(true);
        pullHandler.gameObject.SetActive(true);

        Action<float> onPull = null;
        onPull = pullSpeed =>
        {
            paperRoll.Pull(pullSpeed);
            pullHandler.gameObject.SetActive(false);
            pullHandler.onPull -= onPull;
        };
        pullHandler.onPull += onPull;

        var stopTaskSource = new TaskCompletionSource<object>();

        Action onStop = null;
        onStop = () =>
        {
            stopTaskSource.SetResult(null);
            paperRoll.onStop -= onStop;
        };
        paperRoll.onStop += onStop;

        await stopTaskSource.Task;

        await Task.Delay(TimeSpan.FromSeconds(1));

        playingUI.SetActive(false);
    }

    private void Update()
    {
        pulledLengthText.pulledLength = paperRoll.manualPulledLength;
    }
}
