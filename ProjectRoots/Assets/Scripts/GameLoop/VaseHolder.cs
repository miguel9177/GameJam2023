using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseHolder : MonoBehaviour
{
    public GameObject vaseHolder;
    public Rigidbody dropItem;
    public ItemTimeline Vase;

    public float dropItemForceSpeed = 2f;
    public float timeToWaitForObjectToFall = 1f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Vase")
        {
            Debug.Log("Vase Hit");
            GameManager.Instance.DropVaseAtBase();
            Vase.gameObject.transform.position = vaseHolder.transform.position;
            Vase.gameObject.transform.rotation = vaseHolder.transform.rotation;
            if (Vase.gameObject.TryGetComponent(out Rigidbody rb_))
            {
                rb_.velocity = new Vector3(0f, 0f, 0f);
                rb_.angularVelocity = new Vector3(0f, 0f, 0f);
            }
            dropItem.isKinematic = false;
            dropItem.useGravity = true;
            StartCoroutine(DestroyVase());
        }
    }

    private IEnumerator DestroyVase()
    {
        yield return new WaitForSeconds(timeToWaitForObjectToFall);
        dropItem.AddForce(Vector3.right * dropItemForceSpeed, ForceMode.VelocityChange);
    }
}
