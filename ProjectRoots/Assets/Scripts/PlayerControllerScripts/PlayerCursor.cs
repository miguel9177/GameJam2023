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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance, grabableObjectMask))
        {
            cursor.sprite = cursorGreen;
        }
        else
        {
            cursor.sprite = cursorBlack;
        }

        //debug
        //Debug.DrawRay(transform.position, transform.forward * 10000000, Color.red);
        //Debug.Log("Did not Hit");
        
    }

   
}
