using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class ItemBoxManager : MonoBehaviour
{
    public static ItemBoxManager Instance;

    [field:SerializeField]
    public List<Transform> saveItemPositions { get; private set; }

    [field: SerializeField]
    public List<TMP_Text> saveItemTexts { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

        SetSavePosition();
        SetSaveTMPText();
    }

    /// <summary>
    /// 매니저 자식오브젝트의 아이템 박스 transform을 리스트에 추가하는 메서드.
    /// </summary>
    private void SetSavePosition()
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++) 
        {
            saveItemPositions.Add(transform.GetChild(0).transform.GetChild(i).transform);
        }
    }

    private void SetSaveTMPText()
    {
        for (int i = 0; i < transform.GetChild(1).childCount; i++)
        {
            saveItemTexts.Add(transform.GetChild(1).transform.GetChild(i).GetComponent<TMP_Text>());
        }
    }

    // [SerializeField]
    // private List<ItemBox> items;

    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        foreach (ItemBox itemBox in items)
    //        {
    //            itemBox.SetStartItem();
    //        }
    //    }
    //}

    //public void StartItemSetting()
    //{
    //    foreach (ItemBox itemBox in items)
    //    {
    //        itemBox.SetStartItem();
    //    }
    //}
}
