using System;
using UnityEngine;
using UnityEngine.Events;

public class Sensor : MonoBehaviour
{
    [SerializeField] private UnityEvent ThiefEntered;
    [SerializeField] private UnityEvent ThiefNotDetected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Thief>())
        {
            ThiefEntered?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Thief>())
        {
            ThiefNotDetected?.Invoke();
        }            
    }
}
