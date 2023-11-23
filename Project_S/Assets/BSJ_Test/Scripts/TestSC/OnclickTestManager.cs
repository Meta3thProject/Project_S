using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnclickTestManager : MonoBehaviour
{
    [SerializeField]
    private Button btnClick;

    private void Awake()
    {
        btnClick.onClick.AddListener(() => { Debug.Log("버튼 클릭"); });
    }
}
