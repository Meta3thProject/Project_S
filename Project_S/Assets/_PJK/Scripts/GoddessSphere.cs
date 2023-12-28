using DG.Tweening;
using UnityEngine;

public class GoddessSphere : MonoBehaviour
{
    public static GoddessSphere goddess;
    public Material SphereMaterial;
    private DG.Tweening.Sequence StarAppearSequence;
    private bool ending = false;
    public ParticleSystem starballEffect;

    private void Awake()
    {
        goddess = this;
    }
    void Start()
    {

        SphereMaterial.color = Color.black;
        Debug.Log(StarManager.starManager.getStarCount);

    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            Debug.Log("공에 별이 닿았는가");
            Debug.LogFormat("별의갯수{0}", StarManager.starManager.getStarCount);
            // 현재 메테리얼의 컬러 값을 가져옵니다
            Color currentColor = SphereMaterial.color;

            float increaseValueR = EndingAfterTalk.endingAfterTalk.Color.r / (float)StarManager.starManager.getStarCount;
            float increaseValueG = EndingAfterTalk.endingAfterTalk.Color.g / (float)StarManager.starManager.getStarCount;
            float increaseValueB = EndingAfterTalk.endingAfterTalk.Color.b / (float)StarManager.starManager.getStarCount;

            currentColor.r = Mathf.Clamp01(currentColor.r + increaseValueR);
            currentColor.g = Mathf.Clamp01(currentColor.g + increaseValueG);
            currentColor.b = Mathf.Clamp01(currentColor.b + increaseValueB);

            StarAppearSequence =
                DOTween.Sequence().SetAutoKill(false).Append(SphereMaterial.DOColor(currentColor, 0.1f));
            PlayParticleSystem();

            if (Mathf.Approximately(currentColor.r, 1.0f))
            {
                Debug.Log(currentColor.r);
                Debug.Log("색깔이 초과됐어?");
                bool ending = true;
                EndingAfterTalk.endingAfterTalk.Light();

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