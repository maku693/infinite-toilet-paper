using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PullHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 beginDrag;
    private float beginDragTime;

    public event Action<float> onPull;

    public void OnBeginDrag(PointerEventData eventData)
    {
        beginDrag = eventData.position;
        beginDragTime = Time.time;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Do nothing
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var pullDistance = (eventData.position - beginDrag).magnitude / Screen.dpi;
        var pullDuration = Time.time - beginDragTime;
        var pullSpeed = pullDistance / pullDuration;
        onPull.Invoke(pullSpeed);
    }
}
