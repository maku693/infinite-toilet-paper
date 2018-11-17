using System.Threading.Tasks;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Title title;
    [SerializeField]
    private Countdown countdown;
    [SerializeField]
    private Playing playing;
    [SerializeField]
    private Result result;

    private async void OnEnable()
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
