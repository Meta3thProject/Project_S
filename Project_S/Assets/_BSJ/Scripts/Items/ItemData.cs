using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NPC와 상호작용을 해야하는 Item의 데이터베이스 모음.
/// </summary>
public class ItemData : MonoBehaviour
{
    public static ItemData itemdata;

    public ItemDatabase itemDB;

    private void Awake()
    {
        Debug.Log(itemDB.dataArray[54].ID);
    }
}
