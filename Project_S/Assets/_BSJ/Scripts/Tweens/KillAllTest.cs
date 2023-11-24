using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KillAllTest : MonoBehaviour, ITweenKillAll
{
    public void KillAllTween()
    {
        DOTween.KillAll();
    }
}
