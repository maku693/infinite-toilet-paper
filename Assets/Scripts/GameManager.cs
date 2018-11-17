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
    private Animator rollingPaperRollAnimator;

    [SerializeField]
    private GameObject titleText;
    [SerializeField]
    private GameObject guidanceText;
    [SerializeField]
    private Button startButton;

    [SerializeField]
    private Countdown countdown;

    [SerializeField]
    private PulledLengthText pulledLengthText;
    [SerializeField]
    private PullHandler pullHandler;

    private async Task OnEnable()
    {
        titleText.SetActive(false);
        guidanceText.SetActive(false);
        startButton.gameObject.SetActive(false);
        pulledLengthText.gameObject.SetActive(false);
        pullHandler.gameObject.SetActive(false);

        await StartScreen();
        await GameLoop();
    }

    private async Task GameLoop()
    {
        await countdown.BeginCountdown();
        await Playing();

        await GameLoop();
    }

    private async Task StartScreen()
    {
        titleText.SetActive(true);
        guidanceText.SetActive(true);
        startButton.gameObject.SetActive(true);

        rollingPaperRollAnimator.SetBool("rotate", true);

        var startTaskSource = new TaskCompletionSource<object>();

        UnityAction onClick = null;
        onClick = () =>
        {
            startTaskSource.SetResult(null);
            startButton.onClick.RemoveListener(onClick);
        };
        startButton.onClick.AddListener(onClick);

        await startTaskSource.Task;

        titleText.SetActive(false);
        guidanceText.SetActive(false);
        startButton.gameObject.SetActive(false);

        rollingPaperRollAnimator.SetBool("rotate", false);
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
        paperRoll.gameObject.SetActive(true);
    }

    private void Update()
    {
        pulledLengthText.pulledLength = paperRoll.manualPulledLength;
    }
}
