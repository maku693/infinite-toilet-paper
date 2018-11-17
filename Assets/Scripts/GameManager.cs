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
        await countdown.Run();
        await playing.Run();
        await result.Run();

        await GameLoop();
    }
}
