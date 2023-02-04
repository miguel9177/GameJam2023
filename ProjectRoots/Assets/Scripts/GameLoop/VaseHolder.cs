using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseHolder : MonoBehaviour
{
    public GameObject vaseHolder;
    public GameObject dropItem;
    public ItemTimeline Vase;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Vase")
        {
            Debug.Log("Vase Hit");
            GameManager.Instance.DropVaseAtBase();
            Vase.gameObject.transform.position = vaseHolder.transform.position;
            if(Vase.gameObject.TryGetComponent(out Rigidbody rb_))
            {
                rb_.velocity = new Vector3(0f, 0f, 0f);
                rb_.angularVelocity = new Vector3(0f, 0f, 0f);
            }
        }
    }
}
