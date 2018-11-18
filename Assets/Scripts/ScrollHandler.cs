using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollHandler : MonoBehaviour
{
    public event Action<float> onScroll;

    private void Update()
    {
        var scroll = Math.Abs(Input.GetAxis("Mouse ScrollWheel"));
        if (scroll != 0.0F)
        {
            onScroll.Invoke(scroll);
        }
    }
}
