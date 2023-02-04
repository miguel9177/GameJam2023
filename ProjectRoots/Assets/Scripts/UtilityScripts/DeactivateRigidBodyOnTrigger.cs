using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateRigidBodyOnTrigger : MonoBehaviour
{
    private float initMass;
    public OnTriggerEnterSendEvent OnTriggerEnter;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        initMass = rb.mass;
        OnTriggerEnter.onTriggerEnter += EnteredTrigger;
        OnTriggerEnter.onTriggerExit += ExitedTrigger;
    }

    private void EnteredTrigger()
    {
        rb.mass = 99999999999;
    }

    private void ExitedTrigger()
    {
        rb.mass = initMass;
    }
}
