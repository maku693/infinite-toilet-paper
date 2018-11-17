using System;
using UnityEngine;
using TMPro;

public class PulledLengthText : MonoBehaviour
{
    [NonSerialized]
    public float pulledLength;
    [SerializeField]
    private string pulledLengthFormat;
    private string pulledLengthString => pulledLength.ToString(pulledLengthFormat);

    [SerializeField]
    private TMP_Text text;

    private void Update()
    {
        text.text = pulledLengthString;
    }
}
