using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsWithBoxAndFlower : MonoBehaviour
{
    public PlayerCursor cursor;
    public const string tagOfScissor = "Scissor";
    public const string tagOfBox = "Box";
    public const string tagOfWateringCan = "WateringCan";
    public const string tagOfFlower = "Flower";
    public AudioClip boxCutOpenSound;
    private RaycastHit hit;
    private GrabableItem grabbedItem;

    // Start is called before the first frame update
    void Start()
    {
        cursor.OnGrabbedItem += GrabbedAnItem;
        cursor.OnDroppedItem += DroppedAnItem;
        cursor.OnUpdateHitResults += UpdateHitResults;
        InputManager.Instance.OnPressedMouseLeftClick += PressedMouseLeftClick;
        
    }

    private void UpdateHitResults(RaycastHit hit_)
    {
        hit = hit_;
    }

    private void GrabbedAnItem(GrabableItem obj)
    {
        grabbedItem = obj;
    }

    private void DroppedAnItem()
    {
        grabbedItem = null;
    }

    private void PressedMouseLeftClick()
    {
        if (IsConditionsMetToOpenBox())
            CutBoxOpen();
        if (IsConditionsMetToWaterFlower())
            WaterFlower();
    }
    private bool IsConditionsMetToOpenBox()
    {
        if(grabbedItem == null) return false;

        if(!grabbedItem.CompareTag(tagOfScissor)) return false;

        if(hit.transform == null) return false;

        if (!hit.transform.CompareTag(tagOfBox)) return false;

        return true;
                
    }

    private bool IsConditionsMetToWaterFlower()
    {
        if (grabbedItem == null) return false;

        if (!grabbedItem.CompareTag(tagOfWateringCan)) return false;

        if (hit.transform == null) return false;

        if (!hit.transform.CompareTag(tagOfFlower)) return false;

        return true;

    }

    private void CutBoxOpen()
    {
        Debug.Log("OPENED BOX< SCRIPT BY TIAGO");
        SoundManager.instance.PlaySound(boxCutOpenSound,1);
    }

    private void WaterFlower()
    {
        Debug.Log("Water Flower< SCRIPT BY TIAGO");
        //SoundManager.instance.PlaySound(boxCutOpenSound, 1);
    }
}
