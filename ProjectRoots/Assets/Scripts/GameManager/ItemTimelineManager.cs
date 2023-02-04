using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ItemTimelineManager : MonoBehaviour
{
    public static ItemTimelineManager Instance { get; private set; }

    public ItemTimeline[] interactableObjects;
   
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


        for (int i = 0; i < interactableObjects.Length; i++)
        {
            if (interactableObjects[i].timeline == ItemTimeline.Timeline.Future)
            {
                FutureObjects a = new FutureObjects();
                a.initPos = interactableObjects[i].transform.position;
                a.initRot = interactableObjects[i].transform.rotation;
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
                a.initLocalPos = interactableObjects[i].transform.position;
                a.initLocalRot = interactableObjects[i].transform.rotation;
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
