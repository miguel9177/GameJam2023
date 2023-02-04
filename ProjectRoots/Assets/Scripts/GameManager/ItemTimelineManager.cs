using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTimelineManager : MonoBehaviour
{
    public ItemTimeline[] futureObjects;
    public ItemTimeline[] pastObjects;


    private void Start()
    {
        GameManager.Instance.OnTimeTravel += TimeTraveled;
    }

    private void TimeTraveled(bool isOnPast)
    {
        //Debug.Log("IsOnPast: "+ isOnPast);
        if (isOnPast)
        {
            for (int i = 0; i < futureObjects.Length; i++)
            {
                futureObjects[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < pastObjects.Length; i++)
            {

                if (!pastObjects[i].isGrabbable)
                {
                    pastObjects[i].isGrabbable= true;
                }
            }
        }
        else
        {
            for (int i = 0; i < futureObjects.Length; i++)
            {
                futureObjects[i].gameObject.SetActive(true);
            }

            for (int i = 0; i < pastObjects.Length; i++)
            {
                if (pastObjects[i].isGrabbable)
                {
                    pastObjects[i].isGrabbable= false;
                }
            }
        }
    }
}
