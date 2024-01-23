using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PublicTree : InteractableObject
{
    private void Awake()
    {
        // 초기 셋팅
        currentHp = maxHp;
        isInteractionAble = true;
        interactTime = new WaitForSeconds(nextInteractionTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isInteractionAble == false) { return; }

        Axe axe = other.GetComponent<Axe>();
        if (other.CompareTag("Axe") && axe.IsBladeOn)
        {
            isInteractionAble = false;

            // HSJ_ 230115
            axe.DoShake();
            SoundManager.Instance.PlaySfxClip("SE_Item_axe_tree", other.ClosestPoint(this.transform.position),1f);

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
            transform.DOShakeScale(0.5f, 0.1f).SetEase(Ease.OutElastic);
        }

        // hp가 0 이하가 되면 아이템을 드랍한다.
        else if(currentHp <= 0)
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
