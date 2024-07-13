using System;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public event Action ThiefEntered;
    public event Action ThiefNotDetected;

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
