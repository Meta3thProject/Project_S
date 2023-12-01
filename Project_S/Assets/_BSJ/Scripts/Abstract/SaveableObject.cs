using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveableObject : MonoBehaviour
{
    [field: SerializeField] public ItemName itemName { get; private set; }     // 이 아이템의 이름
    [field: SerializeField] public Transform saveTarget { get; private set; }  // 인벤토리에 저장될 때 어디에 저장될지 미리 캐싱

    private void Start()
    {
        saveTarget = ItemBoxManager.Instance.saveItemPositions[(int)itemName].transform;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            SaveItem();
        }
    }

    /// <summary>
    /// 아이템을 저장시키는 메서드.
    /// </summary>
    public void SaveItem()
    {
        this.gameObject.transform.position = saveTarget.position;
    }
}
