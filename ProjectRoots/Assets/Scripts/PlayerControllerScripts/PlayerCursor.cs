using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCursor : MonoBehaviour
{
    [Header("Components")]
    public Image cursor;
    public Sprite cursorBlack;
    public Sprite cursorGreen;

    [Header("Data")]
    public float raycastDistance;
    public LayerMask grabableObjectMask;

    [Header("Behaviour")]
    private RaycastHit hit;

    private bool grabbingObject = false;

    #region Events
    public Action<GrabableItem> OnGrabbedItem;
    public Action OnDroppedItem;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.OnPressedE += OnPressedE;    
    }

    private void OnPressedE()
    {
        if (!grabbingObject)
            TryGrabItem();
        else
        {
            OnDroppedItem?.Invoke();
            grabbingObject = false;
        }
    }

    private void TryGrabItem()
    {
        if(hit.transform != null)
        {
            if(hit.transform.TryGetComponent(out GrabableItem grabItem_))
            {
                OnGrabbedItem?.Invoke(grabItem_);
                grabbingObject = true;
            }
        }
    }

    private void FixedUpdate()
    {
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance, grabableObjectMask))
        {
            cursor.sprite = cursorGreen;
        }
        else
        {
            cursor.sprite = cursorBlack;
        }        
    }

   
}
