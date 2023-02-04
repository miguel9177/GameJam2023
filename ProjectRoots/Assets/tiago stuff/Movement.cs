using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

   public float speed;
   public Rigidbody RB;
    Vector2 moveDir;
    Vector3 lookDir;
    public Camera camera;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        lookDir = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse Z")).normalized;
    }

    private void FixedUpdate()
    {
        RB.velocity = new Vector3(moveDir.x * speed, RB.velocity.y, moveDir.y * speed);
       camera.transform.rotation= new Quaternion(lookDir.x, lookDir.y, lookDir.z, camera.transform.rotation.w);
    }
}
