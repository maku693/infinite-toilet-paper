using System;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class CountdownText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;

    public async Task Countdown()
    {
        var counts = Enumerable.Range(1, 3).Reverse();
        foreach (var count in counts)
        {
            text.text = count.ToString();
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}
