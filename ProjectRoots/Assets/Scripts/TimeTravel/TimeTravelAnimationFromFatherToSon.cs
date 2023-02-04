using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTravelAnimationFromFatherToSon : MonoBehaviour
{
    public GameObject fatherImage;
    public GameObject sonImage;

    public Transform imagePlayerLocalPositionAndRot;
    public Transform imageZoomedLocalPositionAndRot;
    public Transform imageInitPlayerLocalPositionAndRot;

    public float animationSpeed = 1f;
    public float animationZoomSpeed = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnTimeTravel += TimeTraveled;
    }

    private void TimeTraveled(bool isOnPast)
    {
        if(isOnPast)
        {
            fatherImage.SetActive(true);
            StartCoroutine(MovePictureToPlayer(fatherImage));
        }
    }

    private IEnumerator MovePictureToPlayer(GameObject objToMove)
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * animationSpeed;

            objToMove.transform.localPosition = Vector3.Lerp(objToMove.transform.localPosition, imagePlayerLocalPositionAndRot.localPosition, t);
            objToMove.transform.localRotation = Quaternion.Lerp(objToMove.transform.localRotation, imagePlayerLocalPositionAndRot.localRotation, t);

            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(MovePictureToZoomPosition(objToMove));
    }

    private IEnumerator MovePictureToZoomPosition(GameObject objToMove)
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * animationZoomSpeed;

            objToMove.transform.localPosition = Vector3.Lerp(objToMove.transform.localPosition, imageZoomedLocalPositionAndRot.localPosition, t);
            objToMove.transform.localRotation = Quaternion.Lerp(objToMove.transform.localRotation, imageZoomedLocalPositionAndRot.localRotation, t);

            yield return new WaitForEndOfFrame();
        }
        ResetPictures();
    }

    private void ResetPictures()
    {
        fatherImage.transform.localPosition = imageInitPlayerLocalPositionAndRot.localPosition;
        fatherImage.transform.localRotation = imageInitPlayerLocalPositionAndRot.localRotation;
    }
}
