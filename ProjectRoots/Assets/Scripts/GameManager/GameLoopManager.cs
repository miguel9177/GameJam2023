using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameLoopManager : MonoBehaviour
{
    public static GameLoopManager Instance { get; private set; }
    public OnTriggerEnterSendEvent cardbox;
    public GameObject table;
    public Rigidbody tableDoor;
    public GrabableItem scissor;
    public MeshFilter cardBoxMesh;
    public Mesh meshToSwitchTo;
    public Vector3 eulerAnglesOfOpenBox;
    public MeshRenderer dirtMesh;
    public Color colorOfWater;
    [Header("Prefab from project")]
    public GrabableItem bigFlowerInInactiveParent;
    public Transform posToSpawnFlower;
    public Rigidbody rbOfStoolInsideCardBox;
    public Transform posOfStoolInsideCardBox;
    public Transform vaseFlowerPosAndRot;
    public GameObject vase;
    public bool boxIsInPlace;
    public bool isFlowerWatered;
    public bool isBigFlowerSpawn = false;
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
            
            if (isFlowerWatered && !isBigFlowerSpawn)
            {
                isBigFlowerSpawn = true;
                bigFlowerInInactiveParent.transform.parent = null;
                for (int i = 0; i < bigFlowerInInactiveParent.coll.Length; i++)
                    bigFlowerInInactiveParent.coll[i].enabled = true;

                bigFlowerInInactiveParent.rb.isKinematic = false;
                bigFlowerInInactiveParent.rb.useGravity = true;
            }
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
                ItemTimelineManager.Instance.futureObjects[i].itemTimeline.gameObject.transform.localPosition = ItemTimelineManager.Instance.futureObjects[i].initPos;
                ItemTimelineManager.Instance.futureObjects[i].itemTimeline.gameObject.transform.localRotation = ItemTimelineManager.Instance.futureObjects[i].initRot;
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

    public void OpenTable()
    {
        tableDoor.isKinematic = false;
        tableDoor.useGravity = true;
        tableDoor.transform.parent = null;
        table.gameObject.layer = LayerMask.NameToLayer("Default");
      

        scissor.rb.isKinematic = false;
        scissor.rb.useGravity = true;

        for (int i = 0; i < scissor.coll.Length; i++)
        {
            scissor.coll[i].enabled = true;
        }
    }

    public void OpenCardBox()
    {
        rbOfStoolInsideCardBox.transform.parent = null;
        rbOfStoolInsideCardBox.transform.position = posOfStoolInsideCardBox.position;
        rbOfStoolInsideCardBox.transform.rotation = posOfStoolInsideCardBox.rotation;
        rbOfStoolInsideCardBox.isKinematic = false;
        rbOfStoolInsideCardBox.useGravity = true;
        rbOfStoolInsideCardBox.transform.parent = null;
        Collider collOfBox = rbOfStoolInsideCardBox.gameObject.GetComponent<Collider>();
        collOfBox.enabled = true;
        Physics.IgnoreCollision(collOfBox, cardbox.gameObject.GetComponent<Collider>());
        cardBoxMesh.mesh = meshToSwitchTo;
        cardbox.gameObject.layer = 0;
        cardBoxMesh.transform.eulerAngles = eulerAnglesOfOpenBox;
        cardBoxMesh.gameObject.layer = 0;
    }

    public void WaterFlower()
    {
        dirtMesh.material.color = colorOfWater;
        isFlowerWatered = true;
        
    }

    public void PutFlowerInVase()
    {
        bigFlowerInInactiveParent.transform.parent = vase.transform;
        bigFlowerInInactiveParent.transform.position = vaseFlowerPosAndRot.position;
        bigFlowerInInactiveParent.transform.rotation = vaseFlowerPosAndRot.rotation;

        GameManager.Instance.ForceDropObject();
        GameManager.Instance.WonGame();
    }
}
