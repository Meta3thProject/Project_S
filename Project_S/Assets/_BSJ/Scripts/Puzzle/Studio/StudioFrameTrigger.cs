using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudioFrameTrigger : MonoBehaviour
{
    [SerializeField] private PictureType framePictureType;
    [SerializeField] private int clearIndex;

    private StudioPuzzleClear pictureClear;
    private StudioPicture picture;

    private bool isInterative = false;

    private void Awake()
    {
        pictureClear = transform.parent.GetComponent<StudioPuzzleClear>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<StudioPicture>() != null && isInterative == false)
        {
            isInterative = true;
            picture = other.GetComponent<StudioPicture>();

            picture.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.005f);
            picture.EnterFrameTrigger();

            if (picture.pictureType == framePictureType)
            {
                pictureClear.IncreaseClearCheck(clearIndex);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<StudioPicture>() != null)
        {
            isInterative = false;
            picture = other.GetComponent<StudioPicture>();

            picture.ExitFrameTrigger();

            if (picture.pictureType == framePictureType)
            {
                pictureClear.DecreaseClearCheck(clearIndex);
            }
        }
    }
}
