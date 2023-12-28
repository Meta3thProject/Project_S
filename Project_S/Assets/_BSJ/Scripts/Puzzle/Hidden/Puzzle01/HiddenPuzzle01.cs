using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 과녁을 맞춰서 그림을 좌우반전하는 퍼즐
public class HiddenPuzzle01 : MonoBehaviour
{
    // 이 퍼즐의 번호는 0번입니다. 퍼즐의 총 갯수 (0, 1, 2, 3)
    const int PUZZLEINDEX = 0;

    // 게임 오브젝트 두개를 활성화 비활성화 할 예정
    private GameObject falseMirrorPicture;
    private GameObject trueMirrorPicture;

    // 두트윈 시퀀스
    private DG.Tweening.Sequence pictureChangeTween;

    // 현재 그림이 뒤집혀 져있는지 체크
    private bool isPictureMirror;
    // 현재 트위닝 중인지 체크
    private bool isTweening;

    // 히든 퍼즐 클리어 스크립트
    private HiddenPuzzleClear hiddenPuzzleClear;

    private void Awake()
    {
        // 그림 오브젝트 캐싱
        falseMirrorPicture = transform.GetChild(0).GetChild(0).gameObject;
        trueMirrorPicture = transform.GetChild(0).GetChild(1).gameObject;

        // 초기값 세팅
        isTweening = false;
        isPictureMirror = false;
        ChangePicture(isPictureMirror);

        // 클리어 스크립트 캐싱
        hiddenPuzzleClear = transform.root.GetComponent<HiddenPuzzleClear>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 과녁에 정령의 손이 닿으면,
        if(other.CompareTag("SpiritHand"))
        {
            if(isTweening == true) { return; }

            isTweening = true;
            // 현재 상태에 따라서 이미지 뒤집기.
            if (isPictureMirror)
            {
                isPictureMirror = false;
                ChangePicture(isPictureMirror);
            }

            else
            {
                isPictureMirror = true;
                ChangePicture(isPictureMirror);
            }
            
        }
    }

    /// <summary>
    /// 그림이 뒤집혀 있음에 따라 1번, 2번 그림 오브젝트를 활성화하고 비활성화하는 메서드.
    /// </summary>
    /// <param name="_isMirror"></param>
    private void ChangePicture(bool _isMirror)
    {
        if (_isMirror)
        {
            pictureChangeTween = DOTween.Sequence().SetAutoKill(false).
                Append(falseMirrorPicture.transform.DOShakeScale(0.2f,0.5f)).OnComplete(() =>
                {
                    trueMirrorPicture.SetActive(true);
                    falseMirrorPicture.SetActive(false);
                    isTweening = false;

                    // 클리어 여부 판단
                    hiddenPuzzleClear.IncreaseClearCheck(PUZZLEINDEX);
                });
        }

        else
        {
            pictureChangeTween = DOTween.Sequence().SetAutoKill(false).
                Append(trueMirrorPicture.transform.DOShakeScale(0.2f, 0.5f)).OnComplete(() =>
                {
                    trueMirrorPicture.SetActive(false);
                    falseMirrorPicture.SetActive(true);
                    isTweening = false;

                    // 클리어 여부 판단
                    hiddenPuzzleClear.DecreaseClearCheck(PUZZLEINDEX);
                });
        }
    }
}
