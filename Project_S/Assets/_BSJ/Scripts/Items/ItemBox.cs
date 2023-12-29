using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemBox : MonoBehaviour
{
    // 생성할 아이템
    [SerializeField]
    private GameObject itemPrefab;

    // 아이템 갯수
    public int itemCount { get; private set; }

    // 아이템 타입
    public ItemName itemName { get; private set; }
    [SerializeField]
    private ItemName _ITEMNAME;

    // 갯수 텍스트
    [SerializeField]
    private TextMeshProUGUI textMeshProUGUI;

    private void Awake()
    {
        // 아이템 박스가 가지고 있을 고유의 아이템
        itemName = _ITEMNAME;
    }

    /// <summary>
    /// 아이템 갯수를 지정해 주는 함수.
    /// </summary>
    /// <param name="_count"></param>
    public void SetItemCount(int _count)
    {
        itemCount = _count;

        // 아이템 갯수 텍스트 업데이트
        UpdateCountText(itemCount);
    }

    /// <summary>
    /// 아이템 갯수를 1개 증가시키는 함수.
    /// </summary>
    /// <param name="_itemType"></param>
    public void IncreaseItemCount(int _itemTypeIDX, ItemName _itemName)
    {
        itemCount++;
        switch (_itemTypeIDX)
        {
            // 아이템 타입 0
            case 0:
                if (_itemName == ItemName.wood)          { PlayerInventory.instance.wood++; }
                else if (_itemName == ItemName.stone)    { PlayerInventory.instance.stone++; }
                else if (_itemName == ItemName.iron)     { PlayerInventory.instance.iron++; }
                break;

            // 아이템 타입 1
            case 1:
                // 아직 없음.
                break;

            // 아이템 타입 2
            case 2:
                if (_itemName == ItemName.ice)           { PlayerInventory.instance.ice++; }
                break;

            // 아이템 타입 3
            case 3:
                if (_itemName == ItemName.oil)           { PlayerInventory.instance.oil++; }
                break;

            // 아이템 타입 4
            case 4:
                if (_itemName == ItemName.torch)             { PlayerInventory.instance.torch++; }
                else if (_itemName == ItemName.flashlight)   { PlayerInventory.instance.flashlight++; }
                else if (_itemName == ItemName.brush)        { PlayerInventory.instance.brush++; }
                break;

            // 아이템 타입 5
            case 5:
                if (_itemName == ItemName.toyParts)              { PlayerInventory.instance.toyParts++; }
                else if (_itemName == ItemName.bond)             { PlayerInventory.instance.bond++; }
                else if (_itemName == ItemName.repairedToy)      { PlayerInventory.instance.repairedToy++; }
                else if (_itemName == ItemName.cookie)           { PlayerInventory.instance.cookie++; }
                else if (_itemName == ItemName.wrappingPaper)    { PlayerInventory.instance.wrappingPaper++; }
                else if (_itemName == ItemName.deliciousSnack)   { PlayerInventory.instance.deliciousSnack++; }
                else if (_itemName == ItemName.thickThread)      { PlayerInventory.instance.thickThread++; }
                else if (_itemName == ItemName.knittingNeedles)  { PlayerInventory.instance.knittingNeedles++; }
                else if (_itemName == ItemName.warmShawl)        { PlayerInventory.instance.warmShawl++; }
                else if (_itemName == ItemName.film)             { PlayerInventory.instance.film++; }
                else if (_itemName == ItemName.cameraWithoutFilm){ PlayerInventory.instance.cameraWithoutFilm++; }
                else if (_itemName == ItemName.camera)           { PlayerInventory.instance.camera++; }
                break;

            // 아이템 타입 6
            case 6:
                if (_itemName == ItemName.spiritAxe)             { PlayerInventory.instance.spiritAxe++; }
                else if (_itemName == ItemName.magicMap)         { PlayerInventory.instance.magicMap++; }
                break;

            default: break;
        }

        // 자동저장 LEGACY : BSJ 231229
        // FirebaseManager.instance.UpdateFirebaseInventory(_itemTypeIDX);

        // 아이템 갯수 텍스트 업데이트
        UpdateCountText(itemCount);
    }

    /// <summary>
    /// 아이템 갯수를 1개 감소시키는 함수.
    /// </summary>
    /// <param name="_itemType"></param>
    public void DecreaseItemCount(int _itemTypeIDX, ItemName _itemName) 
    {
        itemCount--;
        switch (_itemTypeIDX)
        {
            // 아이템 타입 0
            case 0:
                if (_itemName == ItemName.wood)              { PlayerInventory.instance.wood--; }
                else if (_itemName == ItemName.stone)        { PlayerInventory.instance.stone--; }
                else if (_itemName == ItemName.iron)         { PlayerInventory.instance.iron--; }
                break;

            // 아이템 타입 1
            case 1:
                // 아직 없음.
                break;

            // 아이템 타입 2
            case 2:
                if (_itemName == ItemName.ice)               { PlayerInventory.instance.ice--; }
                break;

            // 아이템 타입 3
            case 3:
                if (_itemName == ItemName.oil)               { PlayerInventory.instance.oil--; }
                break;

            // 아이템 타입 4
            case 4:
                if (_itemName == ItemName.torch)             { PlayerInventory.instance.torch--; }
                else if (_itemName == ItemName.flashlight)   { PlayerInventory.instance.flashlight--; }
                else if (_itemName == ItemName.brush)        { PlayerInventory.instance.brush--; }
                break;

            // 아이템 타입 5
            case 5:
                if (_itemName == ItemName.toyParts)              { PlayerInventory.instance.toyParts--; }
                else if (_itemName == ItemName.bond)             { PlayerInventory.instance.bond--; }
                else if (_itemName == ItemName.repairedToy)      { PlayerInventory.instance.repairedToy--; }
                else if (_itemName == ItemName.cookie)           { PlayerInventory.instance.cookie--; }
                else if (_itemName == ItemName.wrappingPaper)    { PlayerInventory.instance.wrappingPaper--; }
                else if (_itemName == ItemName.deliciousSnack)   { PlayerInventory.instance.deliciousSnack--; }
                else if (_itemName == ItemName.thickThread)      { PlayerInventory.instance.thickThread--; }
                else if (_itemName == ItemName.knittingNeedles)  { PlayerInventory.instance.knittingNeedles--; }
                else if (_itemName == ItemName.warmShawl)        { PlayerInventory.instance.warmShawl--; }
                else if (_itemName == ItemName.film)             { PlayerInventory.instance.film--; }
                else if (_itemName == ItemName.cameraWithoutFilm){ PlayerInventory.instance.cameraWithoutFilm--; }
                else if (_itemName == ItemName.camera)           { PlayerInventory.instance.camera--; }
                break;

            // 아이템 타입 6
            case 6:
                if (_itemName == ItemName.spiritAxe)             { PlayerInventory.instance.spiritAxe--; }
                else if (_itemName == ItemName.magicMap)         { PlayerInventory.instance.magicMap--; }
                break;

            default: break;
        }

        // 아이템 갯수 텍스트 업데이트
        UpdateCountText(itemCount);
    }

    //! 아이템 타입을 체크해서 int 형으로 리턴해주는 함수
    public int CheckItemType(ItemName _itemType)
    {
        // 아이템 타입 0
        if (_itemType == ItemName.wood || _itemType == ItemName.stone || _itemType == ItemName.iron) 
        { return 0; }

        // 아이템 타입 1

        // 아이템 타입 2
        else if (_itemType == ItemName.ice) 
        { return 2; }

        // 아이템 타입 3
        else if (_itemType == ItemName.oil) 
        { return 3; }

        // 아이템 타입 4
        else if (_itemType == ItemName.torch || _itemType == ItemName.flashlight || _itemType == ItemName.brush) 
        { return 4; }

        // 아이템 타입 5
        else if (_itemType == ItemName.toyParts || _itemType == ItemName.bond || _itemType == ItemName.repairedToy ||
                    _itemType == ItemName.cookie || _itemType == ItemName.wrappingPaper || _itemType == ItemName.deliciousSnack ||
                    _itemType == ItemName.thickThread || _itemType == ItemName.knittingNeedles || _itemType == ItemName.warmShawl ||
                    _itemType == ItemName.film || _itemType == ItemName.cameraWithoutFilm || _itemType == ItemName.camera)
        { return 5; }

        // 아이템 타입 6
        else if (_itemType == ItemName.spiritAxe || _itemType == ItemName.magicMap)
        { return 6; }

        else { return -1; }
    }

    /// <summary>
    /// 프리팹에 담겨져 있는 게임오브젝트를 생성해서 리턴하는 함수.
    /// </summary>
    /// <param name="_ItemType"></param>
    /// <returns></returns>
    public GameObject MakeNewItem(ItemName _ItemName)
    {
        // 아이템 타입의 인덱스를 체크한 뒤,
        int _itemTypeIDX = CheckItemType(_ItemName);

        // 아이템의 수량을 1개 감소시키고,
        DecreaseItemCount(_itemTypeIDX, _ItemName);

        // 새로운 아이템을 생성해서 리턴
        GameObject newItem = Instantiate(itemPrefab);

        // LEGACY : BSJ 231229
        // 파이어 베이스 업데이트 
        // FirebaseManager.instance.UpdateFirebaseInventory(_itemTypeIDX);
        return newItem;
    }

    /// <summary>
    /// 게임 시작 시 처음으로 받아오는 함수.
    /// </summary>
    public void SetStartItem()
    {
        // 아이템 타입 0
        if (itemName == ItemName.wood)          { SetItemCount(PlayerInventory.instance.wood); }
        else if (itemName == ItemName.stone)    { SetItemCount(PlayerInventory.instance.stone); }
        else if (itemName == ItemName.iron)     { SetItemCount(PlayerInventory.instance.iron); }

        // 아이템 타입 1
        // 아직 없음.

        // 아이템 타입 2
        else if (itemName == ItemName.ice)      { SetItemCount(PlayerInventory.instance.ice); }

        // 아이템 타입 3
        else if (itemName == ItemName.oil)      { SetItemCount(PlayerInventory.instance.oil); }

        // 아이템 타입 4
        else if (itemName == ItemName.torch)            { SetItemCount(PlayerInventory.instance.torch); }
        else if (itemName == ItemName.flashlight)       { SetItemCount(PlayerInventory.instance.flashlight); }
        else if (itemName == ItemName.brush)            { SetItemCount(PlayerInventory.instance.brush); }

        // 아이템 타입 5
        else if (itemName == ItemName.toyParts)         { SetItemCount(PlayerInventory.instance.toyParts); }
        else if (itemName == ItemName.bond)             { SetItemCount(PlayerInventory.instance.bond); }
        else if (itemName == ItemName.repairedToy)      { SetItemCount(PlayerInventory.instance.repairedToy); }
        else if (itemName == ItemName.cookie)           { SetItemCount(PlayerInventory.instance.cookie); }
        else if (itemName == ItemName.wrappingPaper)    { SetItemCount(PlayerInventory.instance.wrappingPaper); }
        else if (itemName == ItemName.deliciousSnack)   { SetItemCount(PlayerInventory.instance.deliciousSnack); }
        else if (itemName == ItemName.thickThread)      { SetItemCount(PlayerInventory.instance.thickThread); }
        else if (itemName == ItemName.knittingNeedles)  { SetItemCount(PlayerInventory.instance.knittingNeedles); }
        else if (itemName == ItemName.warmShawl)        { SetItemCount(PlayerInventory.instance.warmShawl); }
        else if (itemName == ItemName.film)             { SetItemCount(PlayerInventory.instance.film); }
        else if (itemName == ItemName.cameraWithoutFilm){ SetItemCount(PlayerInventory.instance.cameraWithoutFilm); }
        else if (itemName == ItemName.camera)           { SetItemCount(PlayerInventory.instance.camera); }

        // 아이템 타입 6
        else if (itemName == ItemName.spiritAxe)        { SetItemCount(PlayerInventory.instance.spiritAxe); }
        else if (itemName == ItemName.magicMap)         { SetItemCount(PlayerInventory.instance.magicMap); }

        // 아이템 갯수 텍스트 업데이트
        UpdateCountText(itemCount);
    }

    /// <summary>
    /// 텍스트의 카운트를 업데이트하는 함수.
    /// </summary>
    /// <param name="_itemCount"></param>
    public void UpdateCountText(int _itemCount)
    {
        textMeshProUGUI.text = _itemCount.ToString();
    }
}
