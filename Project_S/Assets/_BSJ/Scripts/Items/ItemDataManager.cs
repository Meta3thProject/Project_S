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

    private void Awake()
    {
        instance = this;
    }
}
