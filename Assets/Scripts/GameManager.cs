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
    private Playing playing;
    [SerializeField]
    private Result result;

    private async Task OnEnable()
    {
        await title.Run();
        await GameLoop();
    }

    private async Task GameLoop()
    {
        paperRoll.gameObject.SetActive(true);
        await countdown.BeginCountdown();
        await playing.Run();
        await result.ShowResult(paperRoll.manualPulledLength);
        paperRoll.gameObject.SetActive(false);

        await GameLoop();
    }
}
