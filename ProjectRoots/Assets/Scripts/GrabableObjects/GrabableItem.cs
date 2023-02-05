using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableItem : MonoBehaviour
{ 
    public Rigidbody rb;
    public Collider[] coll;
    public bool deactivateColliderAtGrab;
    public Vector3 rotOfObjectWhenGrabbingIt;
    [Header("Put this to diferent then 0 to be able to freeze the specific axis")]
    public Vector3 freezeAxisPosition = Vector3.zero;
    public bool freezeRotation = false;
    [Header("This can be null")]
    public LockPositions lockPositions = null;
}
