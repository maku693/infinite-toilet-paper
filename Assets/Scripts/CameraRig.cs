using UnityEngine;

public class CameraRig : MonoBehaviour
{
    [SerializeField]
    private new Camera camera;

    [SerializeField]
    private PaperRoll paperRoll;

    private Vector3 initialPosition;
    [SerializeField]
    private float verticalMovementMultiplier;

    private float initialSize;
    [SerializeField]
    private float sizeMultiplier;

    private void Start()
    {
        initialPosition = camera.transform.position;
        initialSize = camera.orthographicSize;
    }

    private void Update()
    {
        var movement = initialPosition * paperRoll.pulledLength;
        movement.y *= -1 * verticalMovementMultiplier;
        camera.transform.position = initialPosition + movement;

        var size = initialSize + paperRoll.pulledLength * sizeMultiplier;
        camera.orthographicSize = size;
    }
}
