using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    [field: SerializeField] public int itemID { get; private set; }

    private void Start()
    {
        // 모델의 이름을 DB와 대조해서 ID를 넣어주는 로직
        for(int i = 0; i < ItemDataManager.instance.itemDB.dataArray.Length; i++)
        {
            if(this.name.ToString() == ItemDataManager.instance.itemDB.dataArray[i].MODEL)
            {
                itemID = ItemDataManager.instance.itemDB.dataArray[i].ID;
                break;
            }

            else { /*DoNothing*/ }
        }
    }
}
