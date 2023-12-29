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
        // 이 퍼즐의 인덱스는 7번입니다.
        pictureClear = transform.root.GetChild(7).GetComponent<StudioPuzzleClear>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isInterative) { return; }

        if(other.GetComponent<StudioPicture>() != null)
        {
            isInterative = true;
            picture = other.GetComponent<StudioPicture>();

            // 그림이 딱 프레임 트리거에 딱 달라붙게 하는 로직
            picture.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.005f);
            picture.EnterFrameTrigger();

            // 타입이 같다면 클리어 체크
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

            // 타입이 같다면 클리어 체크 해제
            if (picture.pictureType == framePictureType)
            {
                pictureClear.DecreaseClearCheck(clearIndex);
            }
        }
    }
}
