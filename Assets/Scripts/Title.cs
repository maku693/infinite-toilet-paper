using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField]
    private Animator rollingPaperRollAnimator;

    [SerializeField]
    private GameObject titleUI;

    [SerializeField]
    private Button startButton;

    private void OnEnable()
    {
        titleUI.gameObject.SetActive(false);
    }

    public async Task Run()
    {
        titleUI.SetActive(true);

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

        rollingPaperRollAnimator.SetBool("rotate", false);

        titleUI.SetActive(false);
    }
}
