using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    public float animationZoomSpeed;
    public Transform cameraDestinationTransform;
    public Camera cam;

    private bool animating = false;
    
    void Start()
    {
        GameManager.Instance.OnDropVaseAtBase += VaseDroppedAtBase;
    }

    private void VaseDroppedAtBase()
    {
        StartCoroutine(MoveCameraToAnimationPosition(cam));
    }

    private IEnumerator MoveCameraToAnimationPosition(Camera cam)
    {
        if (animating)
            yield break;

        animating = true;
        PlayerCamera.stopWorking = true;
        PlayerMovement.stopWorking = true;
        float t = 0;
        Vector3 startingCameraPos = cam.transform.position;
        Quaternion startingCameraRot = cam.transform.rotation;
        while (t < 1)
        {
            t += Time.deltaTime * animationZoomSpeed;

            Quaternion lookOnLook = Quaternion.LookRotation(cameraDestinationTransform.transform.position - cam.transform.position);


            cam.transform.rotation = Quaternion.Slerp(startingCameraRot, lookOnLook, t);

            //Debug.Log(t);
            //cam.transform.position = Vector3.Lerp(startingCameraPos, cameraDestinationTransform.position, t);
            //cam.transform.rotation = Quaternion.Lerp(startingCameraRot, cameraDestinationTransform.rotation, t);

            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(10f);
        PlayerCamera.stopWorking = false;
        PlayerMovement.stopWorking = false;
        animating = false;
        //cam.transform.position = startingCameraPos;
        //cam.transform.rotation = startingCameraRot;
    }
}
