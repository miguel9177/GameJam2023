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
    public float animationPauseTime = 0.5f;
    public float animationZoomSpeed = 0.3f;
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnBeginTimeTravel += TimeTraveled;
    }

    private void TimeTraveled(bool isOnPast)
    {
        if(isOnPast)
        {
            fatherImage.SetActive(true);
            StartCoroutine(MovePictureToPlayer(fatherImage));
        }
        else
        {
            sonImage.SetActive(true);
            StartCoroutine(MovePictureToPlayer(sonImage));
        }
    }

    private IEnumerator MovePictureToPlayer(GameObject objToMove)
    {
        float t = 0;
        Vector3 startingPicturePos = objToMove.transform.localPosition;
        Quaternion startingPictureRot = objToMove.transform.localRotation;
        while (t < 1)
        {
            t += Time.deltaTime * animationSpeed;

            objToMove.transform.localPosition = Vector3.Lerp(startingPicturePos, imagePlayerLocalPositionAndRot.localPosition, t);
            objToMove.transform.localRotation = Quaternion.Lerp(startingPictureRot, imagePlayerLocalPositionAndRot.localRotation, t);

            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(animationPauseTime);
        StartCoroutine(MovePictureToZoomPosition(objToMove));
    }

    private IEnumerator MovePictureToZoomPosition(GameObject objToMove)
    {
        float t = 0;
        Vector3 startingPicturePos = objToMove.transform.localPosition;
        Quaternion startingPictureRot = objToMove.transform.localRotation;
        while (t < 1)
        {
            t += Time.deltaTime * animationZoomSpeed;
            Debug.Log(t);
            objToMove.transform.localPosition = Vector3.Lerp(startingPicturePos, imageZoomedLocalPositionAndRot.localPosition, t);
            objToMove.transform.localRotation = Quaternion.Lerp(startingPictureRot, imageZoomedLocalPositionAndRot.localRotation, t);

            yield return new WaitForEndOfFrame();
        }
        ResetPictures();
    }

    private void ResetPictures()
    {
        fatherImage.transform.localPosition = imageInitPlayerLocalPositionAndRot.localPosition;
        fatherImage.transform.localRotation = imageInitPlayerLocalPositionAndRot.localRotation;
        sonImage.SetActive(false);
        fatherImage.SetActive(false);
    }
}
