using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndingDoor : MonoBehaviour
{
    private bool doorOpen = false;

    /// <summary>
    /// 별의 갯수가 15개 이상일 때 엔딩의 문이 열리는 메서드.
    /// </summary>
    public void EndingDoorOpen()
    {
        if(doorOpen) { return; }

        for(int i = 0; i < this.transform.childCount; i++) 
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        doorOpen = true;
    }
}
