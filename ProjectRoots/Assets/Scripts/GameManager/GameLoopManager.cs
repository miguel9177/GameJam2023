using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameLoopManager : MonoBehaviour
{
    public static GameLoopManager Instance { get; private set; }
    public OnTriggerEnterSendEvent cardbox;
    public bool boxIsInPlace;
    //public ItemTimelineManager itemTimelineManager;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);

        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cardbox.onTriggerEnter += boxInDestination;
        cardbox.onTriggerExit += boxOutDestination;
        GameManager.Instance.OnTimeTravel += TimeTraveled;
    }

    private void TimeTraveled(bool isOnPast)
    {
        if (!isOnPast) //if we're changing to past
        {
            if (boxIsInPlace) //if box is in place
            {
                
                return; //ignore it
            }
            else
            {
                ReturnFutureItemsToStartingPlace(); //
            }


        }
    }

    private void ReturnFutureItemsToStartingPlace()
    {
        if(ItemTimelineManager.Instance.futureObjects.Count < 0)
        {
            //Debug.Log("got here 2");
            return;
        }
        else
        {
            //Debug.Log("got here 3");
            for (int i = 0; i < ItemTimelineManager.Instance.futureObjects.Count; i++)
            {
                if(ItemTimelineManager.Instance.futureObjects[i].rigidBody != null)
                {
                    ItemTimelineManager.Instance.futureObjects[i].rigidBody.velocity = new Vector3(0f, 0f, 0f);
                }
                ItemTimelineManager.Instance.futureObjects[i].itemTimeline.gameObject.transform.position = ItemTimelineManager.Instance.futureObjects[i].initPos;
                ItemTimelineManager.Instance.futureObjects[i].itemTimeline.gameObject.transform.rotation = ItemTimelineManager.Instance.futureObjects[i].initRot;
                ItemTimelineManager.Instance.futureObjects[i].itemTimeline = ItemTimelineManager.Instance.futureObjects[i].itemTimeline;

                // Debug.Log("Item: " + itemTimelineManager.interactableObjects[i]);
            }
        }
    }

    private void boxInDestination()
    {
        Debug.Log("Box is in Place");
        boxIsInPlace = true;
    }

    private void boxOutDestination()
    {
        Debug.Log("Box is no longer in Place");
        boxIsInPlace = false;
    }
}
