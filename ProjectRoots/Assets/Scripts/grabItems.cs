using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabItems : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Item"){
            Debug.Log("item is reachable");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3 (1 * Time.deltaTime, 0, 0);
        
    }
}
