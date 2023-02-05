using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAdder : MonoBehaviour
{
    private Rigidbody rb;
    public float gravityForce;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector3.down * gravityForce, ForceMode.Acceleration);
    }
}
