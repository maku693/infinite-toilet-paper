using UnityEngine;
using UnityEngine.EventSystems;

public class TapToStart : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private AudioSource startAudio;

    public void OnPointerClick(PointerEventData eventData)
    {
        startAudio.Play();
    }
}
