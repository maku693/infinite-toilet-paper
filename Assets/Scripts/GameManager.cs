using System;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PaperRoll paperRoll;

    [SerializeField]
    private CountdownText countdownText;

    [SerializeField]
    private PulledLengthText pulledLengthText;
    [SerializeField]
    private PullHandler pullHandler;

    private async Task OnEnable()
    {
        paperRoll.gameObject.SetActive(false);
        countdownText.gameObject.SetActive(false);
        pulledLengthText.gameObject.SetActive(false);
        pullHandler.gameObject.SetActive(false);

        await GameLoop();
    }

    private async Task GameLoop()
    {
        await Countdown();
        await Playing();

        await GameLoop();
    }

    private async Task Countdown()
    {
        paperRoll.gameObject.SetActive(true);
        countdownText.gameObject.SetActive(true);

        await countdownText.Countdown();

        countdownText.gameObject.SetActive(false);
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
            paperRoll.onStop -= (onStop);
        };
        paperRoll.onStop += onStop;

        await stopTaskSource.Task;

        pulledLengthText.gameObject.SetActive(false);
        paperRoll.gameObject.SetActive(false);
    }

    private void Update()
    {
        pulledLengthText.pulledLength = paperRoll.manualPulledLength;
    }
}
