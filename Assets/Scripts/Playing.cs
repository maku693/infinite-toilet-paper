using System;
using UniRx.Async;
using UnityEngine;

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
    [SerializeField]
    private ScrollHandler scrollHandler;
    [SerializeField]
    private float scrollSpeedMultiplier;

    private void OnEnable()
    {
        playingUI.SetActive(false);
        scrollHandler.gameObject.SetActive(false);

        Action<float> onPull = null;
        onPull = pullSpeed =>
        {
            paperRoll.Pull(pullSpeed);
            pullHandler.gameObject.SetActive(false);
        };
        pullHandler.onPull += onPull;

        Action<float> onScroll = null;
        onScroll = scrollSpeed =>
        {
            paperRoll.Pull(scrollSpeed * scrollSpeedMultiplier);
            scrollHandler.gameObject.SetActive(false);
        };
        scrollHandler.onScroll += onScroll;
    }

    public async UniTask Run()
    {
        playingUI.SetActive(true);
        pullHandler.gameObject.SetActive(true);
        scrollHandler.gameObject.SetActive(true);

        var stopTaskSource = new UniTaskCompletionSource();

        Action onStop = null;
        onStop = () =>
        {
            stopTaskSource.TrySetResult();
            paperRoll.onStop -= onStop;
        };
        paperRoll.onStop += onStop;

        await stopTaskSource.Task;

        await UniTask.Delay(TimeSpan.FromSeconds(1));

        playingUI.SetActive(false);
    }

    private void Update()
    {
        pulledLengthText.pulledLength = paperRoll.manualPulledLength;
    }
}
