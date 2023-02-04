using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnterSendEvent : MonoBehaviour
{
    public Action onTriggerEnter;
    public Action onTriggerExit;

    private const string tagOfDestinationBox = "BoxDestination";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagOfDestinationBox))
            onTriggerEnter?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagOfDestinationBox)) 
            onTriggerExit?.Invoke();

    }
}
