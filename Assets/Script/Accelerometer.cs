using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Accelerometer : MonoBehaviour
{
    private static Accelerometer instance;
    public static Accelerometer Instance;


    [Header("Shack Detection")]
    public Action OnShake;
    [SerializeField] private float shakeDetectionThreshold = 2.0f;
    private float accelerometerUpdateInterval = 1.0f / 60.0f;
    private float lowPassKernelWidthInSeconds = 1.0f;
    private float lowPassFilterFactor;
    private Vector3 lowPassValue;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        shakeDetectionThreshold *= shakeDetectionThreshold;
        lowPassValue = Input.acceleration; 
    }

    private void Update()
    {
        Vector3 accleration = Input.acceleration;
        lowPassValue = Vector3.Lerp(lowPassValue, accleration, lowPassFilterFactor);
        Vector3 deltaAcceleration = accleration - lowPassValue;

        if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold)
            OnShake?.Invoke();
    }
}
