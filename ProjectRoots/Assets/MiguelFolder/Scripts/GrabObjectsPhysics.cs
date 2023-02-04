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
    public PlayerCursor playerCursor;
    public Collider playerCollider;
    public GameObject playerGrabLocation;

    [Header("Data")]
    public float objectMovementSpeed = 90;
    //private Vector3 grabbedObjectRot;
    private State state;

    private GrabableItem currentGrabedItem;


    private void Start()
    {
        playerCursor.OnGrabbedItem += GrabObject;
        playerCursor.OnDroppedItem += DropGrabedObject;
        GameManager.Instance.OnForcedDropObject += DropGrabedObject;
        GameManager.Instance.OnDropVaseAtBase += DropGrabedObject;
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

    private void GrabObject(GrabableItem item)
    {
        if (item.gameObject.TryGetComponent(out ItemTimeline itemTimeLine) )
        {
            //if (!itemTimeLine.isGrabbable) {
            if(itemTimeLine.timeline == ItemTimeline.Timeline.Past && !GameManager.Instance.isOnPast) { 
                return;
            
            }
        }
        state = State.GrabbingObject;
        currentGrabedItem = item;
        currentGrabedItem.rb.useGravity = false;
        for(int i = 0; i < currentGrabedItem.coll.Length; i++)
            Physics.IgnoreCollision(currentGrabedItem.coll[i], playerCollider, true);
    }

    public void DropGrabedObject()
    {
        if (currentGrabedItem == null)
            return;

        state = State.idle;
        currentGrabedItem.rb.useGravity = true;
        for (int i = 0; i < currentGrabedItem.coll.Length; i++)
            Physics.IgnoreCollision(currentGrabedItem.coll[i], playerCollider, false);
    }

    private void GrabbingObjectPhysicsUpdate()
    {
        Vector3 newPos = Vector3.Lerp(currentGrabedItem.transform.position, playerGrabLocation.transform.position, Time.deltaTime * objectMovementSpeed);
        currentGrabedItem.rb.MovePosition(newPos);
        currentGrabedItem.rb.MoveRotation(Quaternion.LookRotation(playerCam.transform.forward));
    }
}
