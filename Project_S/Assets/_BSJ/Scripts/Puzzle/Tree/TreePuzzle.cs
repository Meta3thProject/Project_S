using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum TreeType
{
    AppleTree, OrangeTree, PeachTree
}

public class TreePuzzle : InteractableObject, IShakable
{
    [field:SerializeField] public TreeType TreeType { get; private set; }
    private GameObject tree;
    private GameObject treeStump;

    private TreePuzzleClear puzzleClear;
    private ParticleSystem treeHitEffect;

    [SerializeField] private float treeRespawnTime;
    private WaitForSecondsRealtime _treeRespawnTime;

    private void Awake()
    {
        // 초기 셋팅
        currentHp = maxHp;
        isInteractionAble = true;
        interactTime = new WaitForSeconds(nextInteractionTime);

        // 오브젝트 미리 캐싱해두기.
        puzzleClear = transform.parent.transform.GetComponent<TreePuzzleClear>();
        tree = transform.GetChild(0).gameObject;
        treeStump = transform.GetChild(1).gameObject;
        treeHitEffect = transform.GetChild(2).GetComponent<ParticleSystem>();

        treeStump.SetActive(false);

        // 나무 재생성 시간
        _treeRespawnTime = new WaitForSecondsRealtime(treeRespawnTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isInteractionAble == false) { return; }

        // 상호작용 실행
        InteractObject();

        // 다음 상호작용 타이머 코루틴 시작
        if (this.gameObject.activeSelf)
        {
            StartCoroutine(WaitNextInteraction());
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

    //! !IShakable 인터페이스 : 두트윈 쉐이크
    public void Shake()
    {
        if (currentHp > 0)
        {
            transform.DOShakeScale(0.5f, 0.1f).SetEase(Ease.OutElastic);

            // 이펙트 실행
            treeHitEffect.Play();
        }

        // hp가 0 이하가 되면 나무 밑동을 활성화.
        else
        {
            puzzleClear.IncreaseClearCheck((int)TreeType);
            InActiveTree();
        }
    }

    /// <summary>
    /// 나무 재생성 하는 코루틴.
    /// </summary>
    public IEnumerator RespawnTree()
    {
        yield return _treeRespawnTime;
        ActiveTree();
    }

    //! 나무를 비활성화하는 메서드
    private void InActiveTree()
    {
        tree.SetActive(false);
        treeStump.SetActive(true);
    }

    //! 나무를 재생성하는 메서드
    private void ActiveTree()
    {
        tree.SetActive(true);
        treeStump.SetActive(false);
        currentHp = maxHp;
    }
}
