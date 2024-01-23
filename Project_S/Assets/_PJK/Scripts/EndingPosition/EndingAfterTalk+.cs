using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public partial class EndingAfterTalk : MonoBehaviour
{
    //플레이어의 카메라
    public Camera PlayerCamera;
    //bool값으로 엔딩대화가 끝났는지 구분
    //별 오브젝트
    [SerializeField]
    private GameObject star; //starObject
    //별을 담을 리스트
    private List<GameObject> stars = default; //star
    private DG.Tweening.Sequence StarAppearSequence;
    public GameObject Player = default;
    public GameObject endingstarposition = default;
    public GameObject Sphere = default;
    private Vector3 targetPosition; // 오브젝트가 도착할 목표 위치
    public Canvas EndingCanvas;
    public Material Sky_Mat;
    public bool enableFog = true; // Fog를 사용할지 여부
    public float fogDensity = 0f; // Fog 밀도
    public Color Color = default;//new Color(0.5f, 0.5f, 0.5f);  Fog 색상 (R, G, B 값으로 설정)
    private PlayerStat playerstat;
    private int starnumber;
    public Animator GoddessAnimator;

    public TextMeshProUGUI endingCanvasText;


    public void Light(Color EndingColor)
    {
        RenderSettings.fog = enableFog; // Fog 사용 여부 설정
        
        // TODO : 출력 이전에 문 MBTI 계산한것 추가해주기
        // TODO : 이미 결정된 우위 N/S 스탯에 N/S 총합의 20%를 더해준다.

        endingCanvasText.text = playerstat.GetMBTIStat();
        
        if (enableFog)
        {

            RenderSettings.fogColor = EndingColor;
            RenderSettings.fogMode = FogMode.Exponential; // Fog 모드 설정 (Exponential, ExponentialSquared, Linear 중 선택)

            StartCoroutine(SetFogDensity(EndingColor, () =>
            {

                if (RenderSettings.fogDensity > 0.9f)
                {
                    EndingCameratoUI.endingCameratoUI.gotoending();
                    StartCoroutine(SetForErase(EndingColor));
                }

            }));

        }


        GoddessSphere.goddess.EndingMBTI();//Material색깔 초기화
    }

    public void MBTIColor(out Color mbticolor)
    {

        string MBTI = "infp";
        //if (playerstat.GetMBTIStat() != null)
        //{
        //    MBTI = playerstat.GetMBTIStat();

        //}
        switch (MBTI)
        {
            case "enfp":
                mbticolor = new Color(230f / 255f, 50f / 255f, 49f / 255f);
                break;
            case "entp":
                mbticolor = new Color(255f / 255f, 128f / 255f, 13f / 255f);
                break;
            case "enfj":
                mbticolor = new Color(116f / 255f, 76f / 255f, 40f / 255f);
                break;
            case "entj":
                mbticolor = new Color(9f / 255f, 125f / 255f, 236f / 255f);
                break;
            case "esfj":
                mbticolor = new Color(186f / 255f, 188f / 255f, 3f / 255f);
                break;
            case "estj":
                mbticolor = new Color(8f / 255f, 37f / 255f, 105f / 255f);
                break;
            case "esfp":
                mbticolor = new Color(255f / 255f, 209f / 255f, 18f / 255f);
                break;
            case "estp":
                mbticolor = new Color(214f / 255f, 237f / 255f, 0f / 255f);
                break;
            case "infj":
                mbticolor = new Color(180f / 255f, 180f / 255f, 182f / 255f);
                break;
            case "intp":
                mbticolor = new Color(35f / 255f, 24f / 255f, 22f / 255f);
                break;
            case "infp":
                mbticolor = new Color(255f / 255f, 255f / 255f, 255f / 255f);
                break;
            case "intj":
                mbticolor = new Color(130f / 255f, 103f / 255f, 184f / 255f);
                break;
            case "isfj":
                mbticolor = new Color(253f / 255f, 228f / 255f, 198f / 255f);
                break;
            case "istp":
                mbticolor = new Color(202f / 255f, 186f / 255f, 248f / 255f);
                break;
            case "isfp":
                mbticolor = new Color(254f / 255f, 207f / 255f, 215f / 255f);
                break;
            case "istj":
                mbticolor = new Color(269f / 255f, 226f / 255f, 245f / 255f);
                break;
            default:
                mbticolor = Color.black; // 기본값 설정
                break;
        }

        Debug.LogFormat(MBTI);

    }

    IEnumerator SetFogDensity(Color EndingColor, Action onComplete)
    {
        while (fogDensity <= 1f) // 1에 도달할 때까지 증가
        {
            fogDensity += 0.01f;

            RenderSettings.fogColor = EndingColor; // Fog 색깔 설정
            RenderSettings.fogDensity = fogDensity; // Fog 밀도 설정


            yield return new WaitForSeconds(0.01f);
        }
        onComplete?.Invoke();
    }
    IEnumerator SetForErase(Color EndingColor)
    {
        while (fogDensity >= 0f) // 0에 도달할 때까지 감소
        {
            fogDensity -= 0.01f;

            RenderSettings.fogDensity = fogDensity; // Fog 밀도 설정

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSecondsRealtime(3f);

        // 로그인씬으로 갑니다.
        FirebaseManager.instance.LogOut(true);
    }
}
