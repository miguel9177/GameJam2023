using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTimelineManager : MonoBehaviour
{
    public static ItemTimelineManager Instance { get; private set; }

    public List<ItemTimeline> interactableObjects = new List<ItemTimeline>();
   
    public class PastObjects
    {
        public Vector3 initLocalPos;
        public Quaternion initLocalRot;
        public ItemTimeline itemTimeline;
        public Rigidbody rigidBody;

    }
    public class FutureObjects
    {
        public Vector3 initPos;
        public Quaternion initRot;
        public ItemTimeline itemTimeline;
        public Rigidbody rigidBody;
    }

    public List<FutureObjects> futureObjects = new List<FutureObjects>();
    public List<PastObjects> pastObjects = new List<PastObjects>();


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


    private void Start()
    {
        GameManager.Instance.OnTimeTravel += TimeTraveled;


        for (int i = 0; i < interactableObjects.Count; i++)
        {
            if (interactableObjects[i].timeline == ItemTimeline.Timeline.Future)
            {
                FutureObjects a = new FutureObjects();
                a.initPos = interactableObjects[i].transform.localPosition;
                a.initRot = interactableObjects[i].transform.localRotation;
                a.itemTimeline = interactableObjects[i];
                if (interactableObjects[i].gameObject.TryGetComponent(out Rigidbody rb_))
                {
                    if (rb_ != null)
                       a.rigidBody= rb_;
                }

                futureObjects.Add(a);
            }else if (interactableObjects[i].timeline == ItemTimeline.Timeline.Past)
            {
                PastObjects a = new PastObjects();
                a.initLocalPos = interactableObjects[i].transform.localPosition;
                a.initLocalRot = interactableObjects[i].transform.localRotation;
                a.itemTimeline = interactableObjects[i];
                if (interactableObjects[i].gameObject.TryGetComponent(out Rigidbody rb_))
                {
                    if (rb_ != null)
                        a.rigidBody = rb_;
                }
                pastObjects.Add(a);
            }
        }
    }

    private void TimeTraveled(bool isOnPast)
    {
        //Debug.Log("IsOnPast: "+ isOnPast);
        if (isOnPast)
        {
            for (int i = 0; i < futureObjects.Count; i++)
            {

                futureObjects[i].itemTimeline.gameObject.SetActive(false);
                
                
            }

            
        }
        else
        {
            for (int i = 0; i < futureObjects.Count; i++)
            {

                futureObjects[i].itemTimeline.gameObject.SetActive(true);


            }
        }
    }
}
