using DG.Tweening;
using UnityEngine;

public class GoddessSphere : MonoBehaviour
{
    public static GoddessSphere goddess;
    public Material SphereMaterial;
    private DG.Tweening.Sequence StarAppearSequence;
    public ParticleSystem starballEffect;
    Color currentColor;
    Color EndingColor = default;
    float increaseValueR, increaseValueG, increaseValueB;
    private void Awake()
    {
        goddess = this;
    }
    void Start()
    {

        SphereMaterial.color = Color.black;
    }


    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Endingstar"))
        {
            // 처음 충돌 시 MBTI 색상을 가져옴
            if (EndingColor == default)
            {
                EndingAfterTalk.endingAfterTalk.MBTIColor(out EndingColor);

                increaseValueR = (float)(EndingColor.r / (float)StarManager.starManager.getStarCount);
                increaseValueG = (float)(EndingColor.g / (float)StarManager.starManager.getStarCount);
                increaseValueB = (float)(EndingColor.b / (float)StarManager.starManager.getStarCount);
            }

            currentColor.r = Mathf.Clamp01(currentColor.r + increaseValueR);
            currentColor.g = Mathf.Clamp01(currentColor.g + increaseValueG);
            currentColor.b = Mathf.Clamp01(currentColor.b + increaseValueB);

            StarAppearSequence =
                DOTween.Sequence().SetAutoKill(false).Append(SphereMaterial.DOColor(currentColor, 0.1f));
            PlayParticleSystem();


            if (Mathf.Approximately(currentColor.r, EndingColor.r) &&Mathf.Approximately(currentColor.g, EndingColor.g) &&Mathf.Approximately(currentColor.b, EndingColor.b))
            {
                EndingAfterTalk.endingAfterTalk.Light(EndingColor);
            }
        }
    }
    public void EndingMBTI()
    {
        SphereMaterial.color = Color.black;

    }

    private void PlayParticleSystem()
    {
        if (starballEffect != null)
        {
            // 파티클 시스템을 특정 위치에서 재생
            GameObject starballbangeffect = Instantiate(starballEffect, transform.position, Quaternion.identity).gameObject;

            // 파티클 시스템 재생이 끝나면 삭제
            Destroy(starballbangeffect, starballEffect.main.duration);
        }

    }


}