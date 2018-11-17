using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PaperRoll paperRoll;

    [SerializeField]
    private Title title;

    [SerializeField]
    private Countdown countdown;

    [SerializeField]
    private PulledLengthText pulledLengthText;
    [SerializeField]
    private PullHandler pullHandler;

    [SerializeField]
    private Result result;

    private async Task OnEnable()
    {
        pulledLengthText.gameObject.SetActive(false);
        pullHandler.gameObject.SetActive(false);

        await title.Run();
        await GameLoop();
    }

    private async Task GameLoop()
    {
        paperRoll.gameObject.SetActive(true);
        await countdown.BeginCountdown();
        await Playing();
        await result.ShowResult(paperRoll.manualPulledLength);
        paperRoll.gameObject.SetActive(false);

        await GameLoop();
    }

    private async Task Playing()
    {
        pulledLengthText.gameObject.SetActive(true);
        pullHandler.gameObject.SetActive(true);

        Action<float> onPull = null;
        onPull = pullSpeed =>
        {
            paperRoll.Pull(pullSpeed);
            pullHandler.onPull -= onPull;
            pullHandler.gameObject.SetActive(false);
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

        pulledLengthText.gameObject.SetActive(false);
    }

    private void Update()
    {
        pulledLengthText.pulledLength = paperRoll.manualPulledLength;
    }
}
