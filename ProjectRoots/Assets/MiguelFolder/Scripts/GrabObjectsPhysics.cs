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

        bool lockXPos = false;
        bool lockYPos = false;
        bool lockZPos = false;

        if (currentGrabedItem.freezeAxisPosition.x != 0)
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

        if(currentGrabedItem.lockPositions != null)
        {
            if(currentGrabedItem.lockPositions.blockXPos)
            {
                lockXPos = LockXPos(newPos);
            }
            if (currentGrabedItem.lockPositions.blockYPos)
            {
                lockYPos = LockYPos(newPos);
            }
            if (currentGrabedItem.lockPositions.blockZPos)
            {
                lockZPos = LockZPos(newPos);
            }
        }

        if (lockXPos || lockYPos || lockZPos)
            Debug.Log("LOCK OBJECT");
        else
            currentGrabedItem.rb.MovePosition(newPos);

        if(!currentGrabedItem.freezeRotation)
            currentGrabedItem.rb.MoveRotation(Quaternion.LookRotation(playerCam.transform.forward) * Quaternion.Euler(currentGrabedItem.rotOfObjectWhenGrabbingIt));
    }

    private bool LockXPos(Vector3 newPos)
    {
        if(newPos.x < currentGrabedItem.lockPositions.xPosLimiter.x || newPos.x > currentGrabedItem.lockPositions.xPosLimiter.y)
        {
            return true;
        }
        return false;
    }

    private bool LockYPos(Vector3 newPos)
    {
        if (newPos.y < currentGrabedItem.lockPositions.yPosLimiter.x || newPos.y > currentGrabedItem.lockPositions.yPosLimiter.y)
        {
            return true;
        }
        return false;
    }

    private bool LockZPos(Vector3 newPos)
    {
        if (newPos.z < currentGrabedItem.lockPositions.zPosLimiter.x || newPos.z > currentGrabedItem.lockPositions.zPosLimiter.y)
        {
            return true;
        }
        return false;
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
