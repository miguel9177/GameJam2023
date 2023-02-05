using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [Serializable]
    public class BreakableParts
    {
        public Rigidbody rb;
        public Collider coll;
        [HideInInspector] public Vector3 posBeforeBreaking;
        [HideInInspector] public Quaternion rotBeforeBreaking;
    }

    public Rigidbody rb;
    public float assembleSpeed = 1;
    public List<BreakableParts> allBreakableParts;
    public Collider objectColl;
    public Collider objectToDestroyVase;
    public AudioClip audioToPlayOnBreak;
    public float audioVolume;
    public bool loseGameAtBreak = false;
    [HideInInspector] public bool isBroken = false;

    private bool acting = false;

    private void Start()
    {
        DisableBreakablePartsCollisions();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == objectToDestroyVase)
            BreakObject();
    }

    [ContextMenu("BreakObject")]
    public void BreakObject()
    {
        if (rb == null)
            return;

        objectColl.enabled = false;
        StoreBreakablePartPos();
        EnableBreakablePartsCollisions();
        SoundManager.instance.PlaySound(audioToPlayOnBreak, audioVolume);
        isBroken = true;

        if (loseGameAtBreak)
            Invoke("CallLoseGame", 1);

    }

    [ContextMenu("AssembleObject")]
    public void AssembleObject()
    {
        objectColl.enabled = false;
        DisableBreakablePartsCollisions();
        StartCoroutine(AssembleObjectCoroutine());
    }

    private IEnumerator AssembleObjectCoroutine()
    {
        if (acting)
            yield break;

        acting = true;
        float t = 0;
        while(t < 1)
        {
            t += Time.deltaTime * assembleSpeed;
            Debug.Log(t);
            for (int i = 0; i < allBreakableParts.Count; i++)
            {
                allBreakableParts[i].rb.detectCollisions = false;
                allBreakableParts[i].coll.enabled = false;
                allBreakableParts[i].rb.transform.position = Vector3.Lerp(allBreakableParts[i].rb.transform.position, allBreakableParts[i].posBeforeBreaking, t);
                allBreakableParts[i].rb.transform.rotation = Quaternion.Lerp(allBreakableParts[i].rb.transform.rotation, allBreakableParts[i].rotBeforeBreaking, t);
            }
            yield return new WaitForEndOfFrame();
        }
        objectColl.enabled = true;
        rb.useGravity = true;
        rb.detectCollisions = true;
        acting = false;
    }

    private void CallLoseGame()
    {
        GameManager.Instance.LostGame();
    }

    private void DisableBreakablePartsCollisions()
    {
       
        for (int i = 0; i < allBreakableParts.Count; i++)
        {
            allBreakableParts[i].rb.isKinematic = true;
            allBreakableParts[i].rb.useGravity = false;
            allBreakableParts[i].rb.detectCollisions = false;
            allBreakableParts[i].coll.enabled = false;
        }
    }

    private void EnableBreakablePartsCollisions()
    {
        rb.useGravity = false;
        rb.detectCollisions = false;
        for (int i = 0; i < allBreakableParts.Count; i++)
        {
            allBreakableParts[i].rb.isKinematic = false;
            allBreakableParts[i].rb.detectCollisions = true;
            allBreakableParts[i].rb.useGravity = true;
            allBreakableParts[i].coll.enabled = true;
        }
    }

    private void StoreBreakablePartPos()
    {
        for (int i = 0; i < allBreakableParts.Count; i++)
        {
            allBreakableParts[i].posBeforeBreaking = allBreakableParts[i].rb.transform.position;
            allBreakableParts[i].rotBeforeBreaking = allBreakableParts[i].rb.transform.rotation;
            
        }
    }
}
