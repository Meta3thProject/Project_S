using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick01 : MonoBehaviour
{
    private GameObject log;
    private GameObject transparencyWall;

    private void Awake()
    {
        log = transform.GetChild(0).gameObject;
        transparencyWall = transform.GetChild(1).gameObject;
    }

    /// <summary>
    /// 투명벽 지우는 메서드.
    /// </summary>
    public void DestroyTransparencyWall()
    {
        transparencyWall.gameObject.SetActive(false);
    }
}
