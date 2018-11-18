using UniRx.Async;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField]
    private PaperRoll paperRoll;

    [SerializeField]
    private Animator rollingPaperRollAnimator;

    [SerializeField]
    private GameObject titleUI;

    [SerializeField]
    private Button startButton;

    public async UniTask Run()
    {
        paperRoll.gameObject.SetActive(true);
        titleUI.SetActive(true);

        rollingPaperRollAnimator.SetBool("rotate", true);

        var startTaskSource = new UniTaskCompletionSource();

        UnityAction onClick = null;
        onClick = () =>
        {
            startTaskSource.TrySetResult();
            startButton.onClick.RemoveListener(onClick);
        };
        startButton.onClick.AddListener(onClick);

        await startTaskSource.Task;

        rollingPaperRollAnimator.SetBool("rotate", false);

        titleUI.SetActive(false);
    }
}
