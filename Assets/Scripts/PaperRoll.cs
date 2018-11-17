using System;
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

    public float pullSpeed
    {
        get;
        private set;
    }
    [SerializeField]
    private float pullSpeedMultiplier;
    [SerializeField]
    private float pullSpeedFriction;
    private float pullSpeedDamping => 1.0F - pullSpeedFriction;
    [SerializeField]
    private float pullSpeedTorelance;

    public event Action onStop;

    public void Pull(float speed)
    {
        pullSpeed = speed;
    }

    private void OnEnable()
    {
        pullSpeed = 0.0F;
        manualPulledLength = 0.0F;
    }

    private void Update()
    {
        if (manualPulledLength != 0.0F && pullSpeed < pullSpeedTorelance)
        {
            pullSpeed = 0.0F;
            onStop.Invoke();
        }

        manualPulledLength += pullSpeed * pullSpeedMultiplier * Time.deltaTime;
        pullSpeed *= pullSpeedDamping;
    }
}
