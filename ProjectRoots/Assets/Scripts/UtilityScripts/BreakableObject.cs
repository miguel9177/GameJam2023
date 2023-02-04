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

    private void Start()
    {
        DisableBreakablePartsCollisions();
    }

    [ContextMenu("BreakObject")]
    public void BreakObject()
    {
        if (rb == null)
            return;

        objectColl.enabled = false;
        StoreBreakablePartPos();
        EnableBreakablePartsCollisions();

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
