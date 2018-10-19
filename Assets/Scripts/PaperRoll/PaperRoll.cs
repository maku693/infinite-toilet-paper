using UnityEngine;

[DefaultExecutionOrder(-1)]
public class PaperRoll : MonoBehaviour
{
    private const float inch2cm = 2.54F;

    public float pulledLength;
    public float pullSpeed;
    [SerializeField]
    private float pullSpeedMultiplier;
    [SerializeField]
    private float pullSpeedFriction;
    private float pullSpeedDamping => 1.0F - pullSpeedFriction;
    [SerializeField]
    private float pullSpeedTorelance;

    private bool isStopped;

    private void Enable()
    {
        isStopped = false;
    }

    private void Update()
    {
        if (isStopped) { return; }

        if (pulledLength != 0.0F && pullSpeed < pullSpeedTorelance)
        {
            isStopped = true;
            return;
        }

        pulledLength += pullSpeed * pullSpeedMultiplier * Time.deltaTime;
        pullSpeed *= pullSpeedDamping;
    }
}
