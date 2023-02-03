using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrabObjectsPhysics : MonoBehaviour
{
    private enum State { idle, GrabbingObject}

    [Header("Components")]
    public Camera playerCam;
    public Rigidbody grabableObjectRigidBody;
    public Collider grabableObjectCollider;
    public Rigidbody playerRigidBody;
    public Collider playerCollider;
    public Transform parentOfGrabbedObjects;

    [Header("Data")]
    public float objectMovementSpeed = 90;
    private Vector3 grabbedObjectRot;
    private State state;

    private void Start()
    {
        InputManager.Instance.OnPressedE += OnPressedE;
    }

    private void FixedUpdate()
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
        grabbedObjectRot = grabableObjectRigidBody.transform.eulerAngles;
        grabableObjectRigidBody.useGravity = false;
        Physics.IgnoreCollision(grabableObjectCollider, playerCollider, true);
    }

    private void DropGrabedObject()
    {
        state = State.idle;
        Physics.IgnoreCollision(grabableObjectCollider, playerCollider, false);
        grabableObjectRigidBody.useGravity = true;
    }

    private void GrabbingObjectPhysicsUpdate()
    {
        //grabableObject.transform.parent = parentOfGrabbedObjects.transform;
        Vector3 newPos = Vector3.Lerp(grabableObjectRigidBody.transform.position, parentOfGrabbedObjects.position, Time.deltaTime * objectMovementSpeed);
        grabableObjectRigidBody.MovePosition(newPos);
        grabableObjectRigidBody.MoveRotation(Quaternion.LookRotation(playerCam.transform.forward));

        //Vector3 direction = transform.position - grabableObjectRigidBody.transform.position;
        //grabableObjectRigidBody.transform.eulerAngles = grabbedObjectRot;
        //float distance = Vector3.Distance(transform.position, grabableObjectRigidBody.transform.position);
        //if (distance > 0.1f)
        //{
        //    grabableObjectRigidBody.velocity = direction.normalized * objectMovementSpeed;
        //    grabableObjectRigidBody.rotation = Quaternion.LookRotation(playerCam.transform.forward);
        //}
        //else
        //{
        //    grabableObjectRigidBody.velocity = Vector3.zero;
        //}
    }
}
