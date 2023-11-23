using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemName
{
    // 아이템 타입 0
    wood, stone, iron,              
    // 아이템 타입 1
    // 아이템 타입 2
    ice,                            
    // 아이템 타입 3
    oil,
    // 아이템 타입 4
    torch, flashlight, brush,
    // 아이템 타입 5
    toyParts, bond, repairedToy, cookie, wrappingPaper, deliciousSnack, thickThread, knittingNeedles, warmShawl, film, cameraWithoutFilm, camera,
    // 아이템 타입 6
    spiritAxe, magicMap
}

public class CombinableItem : MonoBehaviour
{
    public ItemName Type {  get; private set; }

    [SerializeField]
    private ItemName type;

    private void SetType(ItemName type)
    {
        this.Type = type;
    }

    private void Awake()
    {
        SetType(type);
    }

    /// <summary>
    /// 본인 스스로를 제거하는 함수
    /// </summary>
    public void SelfDestroyItem()
    {
        transform.position = Vector3.zero;
        Destroy(this.gameObject);
    }
}
