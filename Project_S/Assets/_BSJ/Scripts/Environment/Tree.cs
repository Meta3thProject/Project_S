using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour, IShakable
{
    public void Shake()
    {
        transform.DOShakeScale(0.5f, 0.1f).SetEase(Ease.OutElastic);
    }
}
