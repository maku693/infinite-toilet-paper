using System;
using UniRx.Async;
using UnityEngine;
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
    [SerializeField]
    private ScrollHandler scrollHandler;
    [SerializeField]
    private float scrollSpeedMultiplier;

    [SerializeField]
    private new Camera camera;
    [SerializeField]
    private GameObject pulledPaper;
    [SerializeField]
    private RectTransform arrow;
    [SerializeField]
    private float hideArrowUntil;

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
        arrow.gameObject.SetActive(false);

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

        if (paperRoll.pulledLength > hideArrowUntil)
        {
            arrow.gameObject.SetActive(true);
        }

        var paperRollEdge = new Vector3(
            pulledPaper.transform.position.x,
            -paperRoll.pulledLength,
            pulledPaper.transform.position.z
        );
        arrow.position = camera.WorldToScreenPoint(paperRollEdge);
    }
}
