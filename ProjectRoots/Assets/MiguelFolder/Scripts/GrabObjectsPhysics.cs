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

        DeactivateColliderAtGrab();
    }

    public void DropGrabedObject()
    {
        if (currentGrabedItem == null)
            return;

        state = State.idle;
        currentGrabedItem.rb.useGravity = true;
        ActivateColliderAtDrop();
        for (int i = 0; i < currentGrabedItem.coll.Length; i++)
            Physics.IgnoreCollision(currentGrabedItem.coll[i], playerCollider, false);
    }

    private void GrabbingObjectPhysicsUpdate()
    {
        Vector3 newPos = Vector3.Lerp(currentGrabedItem.transform.position, playerGrabLocation.transform.position, Time.deltaTime * objectMovementSpeed);

        if(currentGrabedItem.freezeAxisPosition.x != 0)
        {
            newPos.x = currentGrabedItem.transform.position.x;
        }
        if (currentGrabedItem.freezeAxisPosition.y != 0)
        {
            newPos.y = currentGrabedItem.transform.position.y;
        }
        if (currentGrabedItem.freezeAxisPosition.z != 0)
        {
            newPos.z = currentGrabedItem.transform.position.z;
        }

        currentGrabedItem.rb.MovePosition(newPos);

        if(!currentGrabedItem.freezeRotation)
            currentGrabedItem.rb.MoveRotation(Quaternion.LookRotation(playerCam.transform.forward) * Quaternion.Euler(currentGrabedItem.rotOfObjectWhenGrabbingIt));
    }

    private void DeactivateColliderAtGrab()
    {
        if (currentGrabedItem.deactivateColliderAtGrab == true)
        {
            for (int i = 0; i < currentGrabedItem.coll.Length; i++)
            {
                currentGrabedItem.coll[i].enabled = false;
            }
        }
    }

    private void ActivateColliderAtDrop()
    {
        if (currentGrabedItem.deactivateColliderAtGrab == true)
        {
            for (int i = 0; i < currentGrabedItem.coll.Length; i++)
            {
                currentGrabedItem.coll[i].enabled = true;
            }
        }
    }
}
