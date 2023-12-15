using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using DG.Tweening;

public class PublicTree : InteractableObject
{
    [SerializeField] private ParticleSystem treeHitEffect;

    private void Awake()
    {
        // 초기 셋팅
        currentHp = maxHp;
        isInteractionAble = true;
        interactTime = new WaitForSeconds(nextInteractionTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("충돌됨?");
        if (isInteractionAble == false) { return; }

        if (other.CompareTag("Axe"))
        {
            isInteractionAble = false;

            // 상호작용 실행
            InteractObject();

            // 다음 상호작용 타이머 코루틴 시작
            if (this.gameObject.activeSelf)
            {
                StartCoroutine(WaitNextInteraction());
            }
        }
    }

    /// <summary>
    /// 나무가 상호작용하는 함수.
    /// </summary>
    public override void InteractObject()
    {
        currentHp--;
        Shake();

        // 연속된 상호작용 불가능
        isInteractionAble = false;
    }

    //! 아이템 드랍 용 두트윈 시퀀스s
    private DG.Tweening.Sequence itemDropSqeunce;

    /// <summary>
    /// 두트윈 쉐이크 하는 메서드.
    /// </summary>
    public void Shake()
    {
        if (currentHp > 0)
        {
            treeHitEffect.Play();
            transform.DOShakeScale(0.5f, 0.1f).SetEase(Ease.OutElastic);
        }

        // hp가 0 이하가 되면 아이템을 드랍한다.
        else
        {
            itemDropSqeunce = DOTween.Sequence().SetAutoKill(false).
                Append(transform.DOShakeScale(0.5f, 0.1f).SetEase(Ease.OutElastic)).
                OnComplete(() =>
                {
                    // 아이템 드랍
                    DropItem();
                });
        }
    }
}
