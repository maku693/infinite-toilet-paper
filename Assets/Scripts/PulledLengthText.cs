using UnityEngine;
using TMPro;

public class PulledLengthText : MonoBehaviour
{
    [SerializeField]
    private PaperRoll paperRoll;

    [SerializeField]
    private string pulledLengthFormat;
    private string pulledLengthString => paperRoll.manualPulledLength.ToString(pulledLengthFormat);

    [SerializeField]
    private TMP_Text text;

    private void Update()
    {
        text.text = pulledLengthString;
    }
}
