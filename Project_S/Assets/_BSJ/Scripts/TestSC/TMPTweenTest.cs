using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class TMPTweenTest : MonoBehaviour
{
    public TMP_Text tmp_Text;

    DG.Tweening.Sequence sequence;

    public void ChangeText()
    {
        if (sequence != null && sequence.IsPlaying()) { sequence.Kill(); }

        DOTweenTMPAnimator tmpAnim = new DOTweenTMPAnimator(tmp_Text);

        sequence = DOTween.Sequence();
        sequence.SetAutoKill(false).Append(tmpAnim.DOPunchCharOffset(0, new Vector3(0.01f, 0.01f, 0.01f), 0.2f, 5)).
            Join(tmpAnim.DOShakeCharScale(0, 0.2f, 0.5f, 5));
    }
}
