using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NPC와 상호작용을 해야하는 Item의 데이터베이스 모음.
/// </summary>
public class ItemDataManager : MonoBehaviour
{
    public static ItemDataManager instance;

    public ItemDatabase itemDB;

    // { JSH 24.1.9
    // 조합 후 생성될 아이템 프리팹 리스트
    public List<GameObject> combinedItems;
    // ID를 통해 쉽게 찾기 위한 딕셔너리
    public Dictionary<int, GameObject> idToCombinedItem;
    // } JSH 24.1.9

    private void Awake()
    {
        instance = this;

        // { JSH 24.1.9
        idToCombinedItem = new Dictionary<int, GameObject>();

        for (int i = 0; i < combinedItems.Count; i++)
        {
            // 리스트를 딕셔너리로
            idToCombinedItem.Add(combinedItems[i].GetComponent<ItemData>().itemID, combinedItems[i]);
        }

        // 사용이 끝났으므로 할당 해제
        combinedItems = null;
        // } JSH 24.1.9
    }

    public void InstanceItem(int id_, Vector3 pos_)
    {
        Instantiate(idToCombinedItem[id_], pos_, Quaternion.identity);
    }
}
