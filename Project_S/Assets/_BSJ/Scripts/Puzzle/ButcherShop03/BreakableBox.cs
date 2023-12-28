using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BreakableBox : MonoBehaviour
{
    BreakBoxEffectSave breakBoxEffectSave;
    DG.Tweening.Sequence tweenSequence;

    bool isTweening;

    private void Awake()
    {
        // 이펙트 캐싱
        breakBoxEffectSave = transform.parent.GetChild(0).GetComponent<BreakBoxEffectSave>();

        isTweening = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 도끼가 닿았을 때,
        if(other.CompareTag("Axe"))
        {
            // 현재 트윈 중이 아니라면
            if (isTweening == false)
            {
                isTweening = true;
                TweeningBreakBox();
            }
        }
    }

    /// <summary>
    /// 상자가 부셔질 때 실행되는 트윈 시퀀스를 실행하는 메서드.
    /// </summary>
    private void TweeningBreakBox()
    {
        // 트윈 시간 (0.5f는 연구된 시간)
        float tweenTime = 0.5f;

        tweenSequence = DOTween.Sequence().SetAutoKill(false).
            Append(transform.DOPunchPosition(new Vector3(0.25f, 0.25f, 0.25f), tweenTime, 5)).
            Join(transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), tweenTime)).OnComplete(() =>
            {
                // 이펙트 호출
                breakBoxEffectSave.BreakBoxEffectPlay(transform.position + Vector3.up);

                // 게임 오브젝트 비활성화
                transform.gameObject.SetActive(false);
            });
    }
}
