using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TapToStart : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private AudioSource startAudio;

    public void OnPointerClick(PointerEventData eventData)
    {
        startAudio.Play();
    }
}
