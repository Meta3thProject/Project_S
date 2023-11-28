using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class DOTween_Test : MonoBehaviour
{
    // 두 트윈 시퀀스
    DG.Tweening.Sequence itemSortSequence;

    // 이동할 타겟 트랜스폼
    [SerializeField]
    private GameObject target;

    // 정렬이 될 때, 위로 얼만큼 올라갈 것인지
    [SerializeField]
    private float UpFromSort;
    [SerializeField]
    private float UpFromTarget;

    // 올라갈 위치를 가진 벡터3
    [SerializeField]
    private Vector3 _UpFromSort;
    [SerializeField]
    private Vector3 _TargetVector;

    // 리지드바디
    private Rigidbody _RigidBody;

    private void Awake()
    {
        _RigidBody = GetComponent<Rigidbody>();

        _UpFromSort = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + UpFromSort, gameObject.transform.position.z);
        _TargetVector = new Vector3(target.transform.position.x, target.transform.position.y + UpFromTarget, target.transform.position.z);
    }

    /// <summary>
    /// 아이템을 정렬하는 DOTween 시퀀스를 실행하는 함수.
    /// </summary>
    public void SortItemOriginPos()
    {
        SetRandomPositionY();
        _RigidBody.isKinematic = true;

        itemSortSequence = DOTween.Sequence().SetAutoKill(false).
            Append(transform.DOMove(_UpFromSort, 3)).OnStart(() =>
            {
                // 이펙트 풀에서 이펙트 가져오기.
                GameObject effect = EffectPool.instance.GetEffect(EffectType.TestEffect01);
                effect.gameObject.SetActive(true);
                effect.transform.position = this.transform.position;
            }).
            Join(transform.DOShakeScale(1, 0.5f, 10, 90)).
            Join(transform.DORotate(new Vector3(180, 180, 180), 3)).

            Append(transform.DOSpiral(3f, Vector3.forward, SpiralMode.ExpandThenContract, 1, 10)).
            Join(transform.DOMove(_TargetVector, 3).SetEase(Ease.InSine)).
            Join(transform.DORotate(new Vector3(180, 180, 180), 3)).OnComplete(() =>
            {
                _RigidBody.isKinematic = false;
            });
    }

    /// <summary>
    /// 랜덤하게 목표 Y 포지션을 정해서 Vector3에 대입하는 함수.
    /// </summary>
    private void SetRandomPositionY()
    {
        UpFromSort = Random.Range(1f, 5f);
        _UpFromSort = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + UpFromSort, gameObject.transform.position.z);

        UpFromTarget = 1f;
    }
}
