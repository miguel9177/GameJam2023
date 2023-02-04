using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ChangePastFuture : MonoBehaviour
{
    [Header("Component")]
    public Volume futureEffect;
    public Volume pastEffect;
    public PlayerMovement playerObj;

    [Header("Data")]
    public Vector3 playerFatherScale;
    public Vector3 playerSonScale;
    public float playerFatherSpeed;
    public float playerSonSpeed;
    public float playerFatherJumpForce;
    public float playerSonJumpForce;


    // Start is called before the first frame update
    void Start()
    {
        ChangeToPast();
    }    

    private void ChangeToFuture()
    {
        playerObj.transform.localScale = playerSonScale;
        playerObj.moveSpeed = playerSonSpeed;
        playerObj.jumpForce = playerSonJumpForce;
        pastEffect.gameObject.SetActive(false);
        futureEffect.gameObject.SetActive(true);        
    }
    private void ChangeToPast()
    {
        playerObj.transform.localScale = playerFatherScale;
        playerObj.moveSpeed = playerFatherSpeed;
        playerObj.jumpForce = playerFatherJumpForce;
        futureEffect.gameObject.SetActive(false);
        pastEffect.gameObject.SetActive(true);
    }
}
