using UnityEngine;

[DefaultExecutionOrder(-1)]
public class PaperRoll : MonoBehaviour
{
    private const float inch2cm = 2.54F;

    [SerializeField]
    private float initialLength;
    public float manualPulledLength
    {
        get;
        private set;
    }
    public float pulledLength => initialLength + manualPulledLength;

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

        if (manualPulledLength != 0.0F && pullSpeed < pullSpeedTorelance)
        {
            isStopped = true;
            return;
        }

        manualPulledLength += pullSpeed * pullSpeedMultiplier * Time.deltaTime;
        pullSpeed *= pullSpeedDamping;
    }
}
