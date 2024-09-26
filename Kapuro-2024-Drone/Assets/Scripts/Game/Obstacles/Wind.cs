using System;
using FMODUnity;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [Header("Wind Settings")]
    [SerializeField] private float windPower;
    [SerializeField] private Quaternion windDirection;

    public float WindPower => windPower;
    public Quaternion WindDirection => windDirection;

    private StudioEventEmitter studioEventEmitter;
    
    private void Start()
    {
        transform.rotation = windDirection;
        studioEventEmitter = GetComponent<StudioEventEmitter>();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        studioEventEmitter.Play();
    }
}
