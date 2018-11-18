using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField]
    private PaperRoll paperRoll;

    [SerializeField]
    private GameObject resultUI;

    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private string scoreFormat;

    [SerializeField]
    private AudioSource finishAudio;

    [SerializeField]
    private Button restartButton;

    private void OnEnable()
    {
        resultUI.SetActive(false);
    }

    public async Task Run()
    {
        scoreText.text = paperRoll.manualPulledLength.ToString(scoreFormat);
        resultUI.SetActive(true);

        finishAudio.Play();

        var restartTaskSource = new TaskCompletionSource<object>();

        UnityAction onClick = null;
        onClick = () =>
        {
            restartTaskSource.SetResult(null);
            restartButton.onClick.RemoveListener(onClick);
        };
        restartButton.onClick.AddListener(onClick);

        await restartTaskSource.Task;

        resultUI.SetActive(false);
    }
}
