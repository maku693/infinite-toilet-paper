using UnityEngine;

public class PaperGrower : MonoBehaviour
{
    [SerializeField]
    private PaperRoll paperRoll;

    [SerializeField]
    private GameObject paper;
    private Vector3 initialPaperPosition;

    private void Start()
    {
        initialPaperPosition = paper.transform.position;
    }

    private void Update()
    {
        var position = paper.transform.position;
        position.y = initialPaperPosition.y * paperRoll.pulledLength;
        paper.transform.position = position;

        var localScale = new Vector3(1, paperRoll.pulledLength, 1);
        // Apply the mesh's rotation
        localScale = paper.transform.rotation * localScale;
        paper.transform.localScale = localScale;
    }
}
