using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTable : MonoBehaviour
{
    // 가능한 조합식 판별 (우선 순위 오름차순)
    [SerializeField]
    private bool[] CheckCombinable = new bool[4];

    // 아이템 그 자체가 저장될 리스트
    [SerializeField]
    private List<GameObject> tableItems = new List<GameObject>();

    // 조합될 아이템
    [SerializeField]
    private List<ItemName> combineTable = new List<ItemName>();

    // 생성될 아이템 프리팹
    [SerializeField]
    private List<GameObject> CombinedItemPrefabs = new List<GameObject>();

    // 생성될 아이템 위치
    [SerializeField]
    private Transform madeItemPos;


    private void OnTriggerEnter(Collider other)
    {
        // 조합 아이템 리스트에 해당 아이템을 저장한다.
        if(other.CompareTag("Item"))
        {
            // 아이템 오브젝트 저장
            SaveItemObj(other.gameObject);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        // 조합 아이템 리스트에 해당 아이템을 제거한다.
        if(other.CompareTag("Item"))
        {
            RemoveItemObj(other.gameObject);
        }
    }

    /// <summary>
    /// 아이템 리스트에 아이템을 추가하여 저장 함수.
    /// </summary>
    /// <param name="itemObj"></param>
    public void SaveItemObj(GameObject itemObj)
    {
        tableItems.Add(itemObj);
    }

    /// <summary>
    /// 아이템 리스트에 저장된 아이템을 제거하는 함수.
    /// </summary>
    /// <param name="itemObj"></param>
    public void RemoveItemObj(GameObject itemObj) 
    {
        tableItems.Remove(itemObj);
    }

    /// <summary>
    /// 아이템을 조합하는 함수.
    /// </summary>
    public void CombineItems()
    {
        // 기존에 조합식에 있던 데이터 삭제
        combineTable.Clear();

        // 조합 테이블 위에 올라와 있는 아이템 전부 검색
        foreach (GameObject item in tableItems) 
        {
            // 아이템 타입 계산
            ItemName itemType = item.GetComponent<CombinableItem>().Type;
            Debug.Log(itemType);

            // 계산된 아이템 타입 리스트에 저장
            combineTable.Add(itemType);
        }

        // 조합이 가능한지 조합식 확인
        CheckCombination();

        // 아이템 생성
        MakeNewItem();

        // 조합 후 조합식 확인 리스트 초기화
        CleanCheckCombinable();
    }

    /// <summary>
    /// 조합 테이블에 올라온 모든 아이템들의 타입을 검사해서 조합이 가능한 조합식 조건 활성화하는 함수.
    /// </summary>
    private void CheckCombination()
    {
        //// 3개 일 경우
        //if (combineTable.Count == 3) 
        //{
        //    // 조합식 확인
        //    // 우선 순위 1 (사과, 포도, 딸기)
        //    if (combineTable.Contains(ItemType.apple) && combineTable.Contains(ItemType.grape) && combineTable.Contains(ItemType.strawberry))
        //    {
        //        CheckCombinable[0] = true;
        //    }
        //}

        //// 2개 일 경우
        //else if (combineTable.Count == 2)
        //{
        //    // 우선 순위 2 (사과, 포도)
        //    if (combineTable.Contains(ItemType.apple) && combineTable.Contains(ItemType.grape))
        //    {
        //        CheckCombinable[1] = true;
        //    }

        //    // 우선 순위 3 (사과, 딸기)
        //    if (combineTable.Contains(ItemType.apple) && combineTable.Contains(ItemType.strawberry))
        //    {
        //        CheckCombinable[2] = true;
        //    }

        //    // 우선 순위 4 (포도, 딸기)
        //    if (combineTable.Contains(ItemType.grape) && (combineTable.Contains(ItemType.strawberry)))
        //    {
        //        CheckCombinable[3] = true;
        //    }
        //}

        //// 나머지 예외 사항
        //else
        //{
        //    Debug.Log("조합할 수 있는 조합식이 없습니다.");
        //}
    }

    /// <summary>
    /// 조합식 조건 초기화하는 함수.
    /// </summary>
    private void CleanCheckCombinable()
    {
        for (int i = 0; i < CheckCombinable.Length; i++) 
        {
            CheckCombinable[i] = false;
        }
    }

    /// <summary>
    /// 조합식에 따라 테이블 위에 있는 아이템을 제거하고 새로운 아이템을 생성하는 함수.
    /// </summary>
    private void MakeNewItem()
    {
        if (CheckCombinable[0])
        {
            DestroyItems();

            GameObject MadeItem = Instantiate(CombinedItemPrefabs[0], this.transform);
            MadeItem.transform.localPosition = Vector3.zero;
            MadeItem.transform.parent = null;

            Debug.Log("슈퍼짱짱과일 생성!");
        }

        else if (CheckCombinable[1])
        {
            DestroyItems();

            GameObject MadeItem = Instantiate(CombinedItemPrefabs[1], this.transform);
            MadeItem.transform.localPosition = Vector3.zero;
            MadeItem.transform.parent = null;

            Debug.Log("슈과일 생성!");
        }

        else if (CheckCombinable[2])
        {
            DestroyItems();

            GameObject MadeItem = Instantiate(CombinedItemPrefabs[2], this.transform);
            MadeItem.transform.localPosition = Vector3.zero;
            MadeItem.transform.parent = null;

            Debug.Log("포과일 생성!");
        }

        else if (CheckCombinable[3])
        {
            DestroyItems();

            GameObject MadeItem = Instantiate(CombinedItemPrefabs[3], this.transform);
            MadeItem.transform.localPosition = Vector3.zero;
            MadeItem.transform.parent = null;

            Debug.Log("짱짱과일 생성!");
        }
    }

    /// <summary>
    /// 조합 테이블 위에 있는 아이템을 제거하는 함수
    /// </summary>
    private void DestroyItems()
    {
        for (int i = 0; i < tableItems.Count; i++) 
        {
            // 아이템 스스로 파괴되도록 지정
            CombinableItem thisItem = tableItems[i].GetComponent<CombinableItem>();
            thisItem.SelfDestroyItem();
        }

        // 테이블 아이템 클리어
        tableItems.Clear();
    }
}
