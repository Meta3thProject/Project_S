using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 상호 작용한 물체의 추상클래스
public abstract class InteractableObject : MonoBehaviour
{
    // 물체의 최대 체력
    [field: SerializeField]
    public int maxHp {  get; protected set; }

    // 물체의 최대 체력
    [field: SerializeField]
    public int currentHp { get; protected set; }

    // 다음 상호작용까지의 시간
    [field: SerializeField]
    public float nextInteractionTime { get; protected set; }

    // 다음 상호작용까지의 시간 WaitforSeconds 캐싱
    protected WaitForSeconds interactTime;

    // 상호 작용 가능한지 조건 체크
    [field: SerializeField]
    public bool isInteractionAble { get; protected set; }

    // 물체가 파괴되었을 때 드랍될 아이템
    [SerializeField] protected GameObject dropedItem;

    protected void OnDisable()
    {
        isInteractionAble = true;
    }

    /// <summary>
    /// 어떻게 상호작용할 것인지에 대한 추상 메서드.
    /// </summary>
    public abstract void InteractObject();

    /// <summary>
    /// 아이템을 드랍하는 함수.
    /// </summary>
    public void DropItem()
    {
        GameObject _dropItem = Instantiate(dropedItem);
        _dropItem.transform.position = this.transform.position;
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// 다음 상호작용까지 대기시간을 체크해주는 코루틴.
    /// </summary>
    /// <returns></returns>
    protected IEnumerator WaitNextInteraction()
    {
        yield return interactTime;
        isInteractionAble = true;
        yield return null;
    }
}
