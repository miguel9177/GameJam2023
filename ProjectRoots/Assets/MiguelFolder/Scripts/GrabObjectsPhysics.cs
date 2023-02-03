using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrabObjectsPhysics : MonoBehaviour
{
    private enum State { idle, GrabbingObject}

    [Header("Components")]
    public Rigidbody grabableObjectRigidBody;
    public Collider grabableObjectCollider;
    public Rigidbody playerRigidBody;
    public Collider playerCollider;
    public Transform parentOfGrabbedObjects;

    [Header("Data")]
    private State state;

    private void Start()
    {
        InputManager.Instance.OnPressedE += OnPressedE;
    }

    private void Update()
    {
        CallFunctionsDependingOnState();
    }

    private void CallFunctionsDependingOnState()
    {
        switch(state)
        {
            case State.idle:
                break;
            case State.GrabbingObject:
                GrabbingObjectPhysicsUpdate();
                break;
        }
    }

    private void OnPressedE()
    {
        if(state == State.GrabbingObject)
        {
            DropGrabedObject();
        }
        else
        {
            GrabObject();
        }
    }

    private void GrabObject()
    {
        state = State.GrabbingObject;
        Physics.IgnoreCollision(grabableObjectCollider, playerCollider, true);
    }

    private void DropGrabedObject()
    {
        state = State.idle;
        Physics.IgnoreCollision(grabableObjectCollider, playerCollider, false);
    }

    private void GrabbingObjectPhysicsUpdate()
    {
        //grabableObject.transform.parent = parentOfGrabbedObjects.transform;
        grabableObjectRigidBody.transform.localPosition = parentOfGrabbedObjects.position;
        grabableObjectRigidBody.transform.localRotation = parentOfGrabbedObjects.rotation;
    }
}
