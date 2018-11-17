using UnityEngine;
using UnityEngine.EventSystems;

public class PullHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private PaperRoll paperRoll;

    private Vector2 beginDrag;
    private float beginDragTime;

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

        paperRoll.Pull(pullSpeed);

        Destroy(gameObject);
    }
}
