using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxInit : MonoBehaviour
{
    // 스카이박스 머테리얼
    [SerializeField] Material skyboxMaterial;

    // material tiling 초기값
    Vector2 initTiling = Vector2.one;

    private void Awake()
    {
        skyboxMaterial.mainTextureScale = initTiling;
    }
}
