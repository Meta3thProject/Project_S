using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : InteractableObject, IShakable
{
    [SerializeField]
    private GameObject treeHitEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (isInteractionAble == false) { return; }

        // 이펙트 풀에서 이펙트 가져오기.
        GameObject effect = EffectPool.instance.GetEffect(EffectType.TreeHitEffect);
        effect.gameObject.SetActive(true);
        effect.transform.position = this.transform.position;

        // 상호작용 실행
        InteractObject();

        // 다음 상호작용 타이머 코루틴 시작
        if(this.gameObject.activeSelf)
        {
            StartCoroutine(NextInteractionTime());
        }
        
    }

    /// <summary>
    /// 나무가 상호작용하는 함수.
    /// </summary>
    public override void InteractObject()
    {
        hp--;
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
        if (hp > 0)
        {
            transform.DOShakeScale(0.5f, 0.1f).SetEase(Ease.OutElastic);
        }

        // hp가 0 이하가 되면 아이템을 드랍한다.
        else
        {
            itemDropSqeunce = DOTween.Sequence().SetAutoKill(false).
                Append(transform.DOShakeScale(0.5f, 0.1f).SetEase(Ease.OutElastic)).
                OnComplete ( () =>
                {   
                    // 이펙트 풀에서 이펙트 가져오기.
                    GameObject effect = EffectPool.instance.GetEffect(EffectType.SortEffect01);
                    effect.gameObject.SetActive(true);
                    effect.transform.position = this.transform.position;

                    // 아이템 드랍
                    DropItem();
                });
        }
    }
}
