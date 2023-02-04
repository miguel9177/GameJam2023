using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ChangePlayerPastFuture : MonoBehaviour
{
    [Header("Component")]
    public Volume futureEffect;
    public Volume pastEffect;
    public PlayerMovement playerObj;
    public PlayerCursor playerCursor;

    [Header("Data")]
    public Vector3 playerFatherScale;
    public Vector3 playerSonScale;
    public float playerFatherSpeed;
    public float playerSonSpeed;
    public float playerFatherJumpForce;
    public float playerSonJumpForce;
    public float playerFatherHeight;
    public float playerSonHeight;
    public float playerFatherGrabableRange;
    public float playerSonGrabableRange;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnTimeTravel += TimeTraveled;
        //ChangeToPast();
    }

    private void TimeTraveled(bool isOnPast)
    {
        if (isOnPast)
            ChangeToPast();
        else
            ChangeToFuture();
    }

    private void ChangeToFuture()
    {
        playerObj.transform.localScale = playerSonScale;
        playerObj.moveSpeed = playerSonSpeed;
        playerObj.jumpForce = playerSonJumpForce;
        playerObj.playerHeight = playerSonHeight;
        playerCursor.raycastDistance = playerSonGrabableRange;
        pastEffect.gameObject.SetActive(false);
        futureEffect.gameObject.SetActive(true);        
    }
    private void ChangeToPast()
    {
        playerObj.transform.localScale = playerFatherScale;
        playerObj.moveSpeed = playerFatherSpeed;
        playerObj.jumpForce = playerFatherJumpForce;
        playerObj.playerHeight = playerFatherHeight;
        playerCursor.raycastDistance = playerFatherGrabableRange;
        futureEffect.gameObject.SetActive(false);
        pastEffect.gameObject.SetActive(true);
    }
}
