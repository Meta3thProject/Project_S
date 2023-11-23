using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxManager : MonoBehaviour
{
    [SerializeField]
    private List<ItemBox> items;

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

    public void StartItemSetting()
    {
        foreach (ItemBox itemBox in items)
        {
            itemBox.SetStartItem();
        }
    }
}
