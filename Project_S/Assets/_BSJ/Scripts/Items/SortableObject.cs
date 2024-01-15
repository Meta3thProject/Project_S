//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using DG.Tweening;
//using UnityEngine.UIElements;
//using static UnityEngine.GraphicsBuffer;
//using TMPro;

//public class SortableObject : MonoBehaviour
//{
//    // 캐싱해둘 스크립트
//    SaveableObject saveableObj;

//    // 정렬하는 아이템 이름
//    [SerializeField] private ItemName itemName;

//    // 두 트윈 시퀀스
//    DG.Tweening.Sequence itemSortSequence;

//    // 이동할 타겟 트랜스폼
//    [SerializeField] private Transform target;

//    // 정렬이 될 때, 위로 얼만큼 올라갈 것인지
//    [SerializeField] private float UpFromSort;
//    [SerializeField] private float UpFromTarget;

//    // 올라갈 위치를 가진 벡터3
//    [SerializeField] private Vector3 _UpFromSort;
//    [SerializeField] private Vector3 _TargetVector;

//    // { 텍스트 두트윈을 위한 변수
//    [SerializeField]
//    private TMP_Text tmp_Text;

//    DG.Tweening.Sequence sequence;
//    // } 텍스트 두트윈을 위한 변수

//    // 리지드바디
//    private Rigidbody _RigidBody;

//    private void Start()
//    {
//        // 데이터 캐싱
//        saveableObj = GetComponent<SaveableObject>();
//        target = saveableObj.saveTarget;
//        itemName = saveableObj.itemName;

//        tmp_Text = ItemBoxManager.Instance.saveItemTexts[0];

//        _RigidBody = GetComponent<Rigidbody>();

//        // 두트윈 시 위로 얼마나 올라갈 것인지 Vector3 셋팅
//        _UpFromSort = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + UpFromSort, gameObject.transform.position.z);
//        // 도착 지점 위로 얼마나 올라갈 것인지 Vector3 셋팅
//        _TargetVector = new Vector3(target.position.x, target.position.y + UpFromTarget, target.position.z);
//    }

//    /// <summary>
//    /// 아이템을 정렬하는 DOTween 시퀀스를 실행하는 메서드.
//    /// </summary>
//    public void SortItemOriginPos()
//    {
//        SetRandomPositionY();
//        _RigidBody.isKinematic = true;

//        itemSortSequence = DOTween.Sequence().SetAutoKill(false).
//            Append(transform.DOMove(_UpFromSort, 3)).OnStart(() =>
//            {
//                // 이펙트 풀에서 이펙트 가져오기.
//                GetEffect(EffectType.SortEffect01, this.transform.position);
//            }).
//            Join(transform.DOShakeScale(1, 0.5f, 10, 90)).
//            Join(transform.DORotate(new Vector3(180, 180, 180), 3)).

//            Append(transform.DOSpiral(3f, Vector3.forward, SpiralMode.ExpandThenContract, 1, 10)).
//            Join(transform.DOMove(_TargetVector, 3).SetEase(Ease.InSine)).
//            Join(transform.DORotate(new Vector3(180, 180, 180), 3)).OnComplete(() =>
//            {
//                _RigidBody.isKinematic = false;

//                // 이펙트 풀에서 이펙트 가져오기.
//                GetEffect(EffectType.SortEffect02, this.transform.position);
//            });
//    }

//    /// <summary>
//    /// 랜덤하게 목표 Y 포지션을 정해서 Vector3에 대입하는 메서드.
//    /// </summary>
//    private void SetRandomPositionY()
//    {
//        UpFromSort = Random.Range(0.5f, 1f);
//        _UpFromSort = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + UpFromSort, gameObject.transform.position.z);

//        UpFromTarget = 1f;
//    }

//    /// <summary>
//    /// 스택형 아이템을 정렬하는 메서드.
//    /// </summary>
//    public void SortStackItemOriginPos()
//    {
//        SetRandomPositionY();
//        _RigidBody.isKinematic = true;

//        itemSortSequence = DOTween.Sequence().SetAutoKill(false).
//            Append(transform.DOMove(_UpFromSort, 3)).OnStart(() =>
//            {
//                // 이펙트 풀에서 이펙트 가져오기.
//                GetEffect(EffectType.SortEffect01, this.transform.position);

//                // 이펙트 풀에서 이펙트 가져오기.
//                GetEffect(EffectType.SortEffect02, 3.0f, _UpFromSort);
//            }).
//            Join(transform.DOShakeScale(1, 0.5f, 10, 90)).
//            Join(transform.DORotate(new Vector3(180, 180, 180), 3)).
//            Join(transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 3)).

//            Append(transform.DOSpiral(3f, Vector3.forward, SpiralMode.ExpandThenContract, 1, 10)).
//            Join(transform.DOMove(_TargetVector, 3).SetEase(Ease.InSine)).
//            Join(transform.DORotate(new Vector3(180, 90, 180), 3)).OnComplete(() =>
//            {
//                _RigidBody.isKinematic = false;

//                // 이펙트 풀에서 이펙트 가져오기.
//                GetEffect(EffectType.SortEffect02, this.transform.position);

//                // 텍스트 두트윈
//                ChangeText();
//            });
//    }

//    /// <summary>
//    /// 이펙트 풀에서 이펙트를 가져와서 활성화 시키는 메서드
//    /// </summary>
//    /// <param name="effectType"></param>
//    private void GetEffect(EffectType effectType, Vector3 _position)
//    {
        
//        GetEffect(effectType, 0f, _position);
//    }

//    private void GetEffect(EffectType effectType, float delayTime, Vector3 _position)
//    {
//        GameObject effect = EffectPool.instance.GetEffect(effectType);
//        effect.gameObject.SetActive(true);
//        effect.transform.position = _position;

//        // 이펙트 실행
//        StartCoroutine(DoPlayFx(effect, delayTime));
//    }

//    //! 파티클 시스템을 사용해서 이펙트의 재생 시점을 결정하는 함수
//    IEnumerator DoPlayFx(GameObject effectObj, float delayTime)
//    {
//        ParticleSystem effect_ = effectObj.GetComponent<ParticleSystem>();
//        yield return new WaitForSeconds(delayTime);

//        effect_.Play();
//    }


//    /// <summary>
//    /// 텍스트 두트윈을 하는 함수.
//    /// </summary>
//    public void ChangeText()
//    {
//        if (sequence != null && sequence.IsPlaying()) { sequence.Kill(); }

//        DOTweenTMPAnimator tmpAnim = new DOTweenTMPAnimator(tmp_Text);

//        sequence = DOTween.Sequence();
//        sequence.SetAutoKill(false).Append(tmpAnim.DOPunchCharOffset(0, new Vector3(0.01f, 0.01f, 0.01f), 0.2f, 5)).
//            Join(tmpAnim.DOShakeCharScale(0, 0.2f, 0.5f, 5)).
//            OnComplete(() =>
//            {
//                AfterSortItemCount(itemName, true);
//            });
//    }

//    /// <summary>
//    /// 정렬 두트윈 종료 시 실제적으로 아이템을 증가시키는 메서드.
//    /// </summary>
//    private void AfterSortItemCount(ItemName _ItemName, bool _PlusOrNinus)
//    {
//        PlayerInventory.instance.PlusOrNinusItemCount(_ItemName, _PlusOrNinus);
//        tmp_Text.text = PlayerInventory.instance.wood.ToString();
//    }
//}
