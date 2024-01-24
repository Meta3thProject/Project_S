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
    public TextMeshProUGUI endingCanvasTextDetail;
    private string mbtiDetail = default;

    public void Light(Color EndingColor)
    {
        RenderSettings.fog = enableFog; // Fog 사용 여부 설정
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

        string MBTI = "ESTJ";
        if (playerstat.GetMBTIStat() != null)
        {
            MBTI = playerstat.GetMBTIStat();

        }
        switch (MBTI)
        {
            case "ENFP":
                mbticolor = new Color(230f / 255f, 50f / 255f, 49f / 255f);
                endingCanvasTextDetail.text = "당신은 멋있는 그림과 더불어 끝내주는 상상력으로 세계적인 화가가 되었습니다.";
                endingCanvasTextDetail.color = mbticolor;

                break;
            case "ENTP":
                mbticolor = new Color(255f / 255f, 128f / 255f, 13f / 255f);
                endingCanvasTextDetail.text = "당신은 풍부한 호기심과 상상력을 바탕으로 훌륭한 과학자가 되었습니다.";
                endingCanvasTextDetail.color = mbticolor;

                break;
            case "ENFJ":
                mbticolor = new Color(116f / 255f, 76f / 255f, 40f / 255f);
                endingCanvasTextDetail.text = "당신은 멋진 카리스마와 더불어 다른 사람의 잠재력을 파악하는 능력으로 뛰어난 코치가 되었습니다.";
                endingCanvasTextDetail.color = mbticolor;
                break;
            case "ENTJ":
                mbticolor = new Color(9f / 255f, 125f / 255f, 236f / 255f);
                endingCanvasTextDetail.text = "당신은 남들을 이끄는 리더십과 뛰어난 전략을 토대로 멋진 지휘관이 되었습니다.";
                endingCanvasTextDetail.color = mbticolor;
                break;
            case "ESFJ":
                mbticolor = new Color(186f / 255f, 188f / 255f, 3f / 255f);
                endingCanvasTextDetail.text = "당신은 다른 사람의 말을 잘 들어주며 다른 사람의 마음을 치료하는 심리상담사가 되었습니다.";
                endingCanvasTextDetail.color = mbticolor;
                break;
            case "ESTJ":
                mbticolor = new Color(8f / 255f, 37f / 255f, 105f / 255f);
                endingCanvasTextDetail.text = "당신은 논리와 경험을 바탕으로 하는 언변과 결단력으로 뛰어난 변호사가 되었습니다.";
                endingCanvasTextDetail.color = mbticolor;
                break;
            case "ESFP":
                mbticolor = new Color(255f / 255f, 209f / 255f, 18f / 255f);
                endingCanvasTextDetail.text = "당신은 재치 넘치는 유머 감각을 바탕으로 누구나 알아주는 개그맨이 되었습니다.";
                endingCanvasTextDetail.color = mbticolor;
                break;
            case "ESTP":
                mbticolor = new Color(214f / 255f, 237f / 255f, 0f / 255f);
                endingCanvasTextDetail.text = "당신은 사그라지지 않는 열정을 바탕으로 뛰어난 소방관이 되었습니다.";
                endingCanvasTextDetail.color = mbticolor;
                break;
            case "INFJ":
                mbticolor = new Color(180f / 255f, 180f / 255f, 182f / 255f);
                endingCanvasTextDetail.text = "당신은 타인의 마음을 공감해주는 훌륭한 상담사가 되었습니다.";
                endingCanvasTextDetail.color = mbticolor;
                break;
            case "INTP":
                mbticolor = new Color(35f / 255f, 24f / 255f, 22f / 255f);
                endingCanvasTextDetail.text = "당신은 많은 아이디어와 뛰어난 문제 이해력으로 멋진 건축가가 되었습니다.";
                endingCanvasTextDetail.color = mbticolor;
                break;
            case "INFP":
                mbticolor = new Color(255f / 255f, 255f / 255f, 255f / 255f);
                endingCanvasTextDetail.text = "당신은 뛰어난 중재력을 바탕으로 무역가가 되었습니다.";
                endingCanvasTextDetail.color = mbticolor;
                break;
            case "INTJ":
                mbticolor = new Color(130f / 255f, 103f / 255f, 184f / 255f);
                endingCanvasTextDetail.text = "당신은 훌륭한 판단력을 바탕으로 존경받는 판사가 되었습니다.";
                endingCanvasTextDetail.color = mbticolor;
                break;
            case "ISFJ":
                mbticolor = new Color(253f / 255f, 228f / 255f, 198f / 255f);
                endingCanvasTextDetail.text = "당신은 뛰어난 지원력을 바탕으로 복지 회사의 사장이 되었습니다.";
                endingCanvasTextDetail.color = mbticolor;
                break;
            case "ISTP":
                mbticolor = new Color(202f / 255f, 186f / 255f, 248f / 255f);
                endingCanvasTextDetail.text = "당신은 뛰어난 문제 대응력을 바탕으로 멋진 파일럿이 되었습니다.";
                endingCanvasTextDetail.color = mbticolor;
                break;
            case "ISFP":
                mbticolor = new Color(254f / 255f, 207f / 255f, 215f / 255f);
                endingCanvasTextDetail.text = "당신은 충만한 감성과 이해력으로 세계적인 작곡가가 되었습니다.";
                endingCanvasTextDetail.color = mbticolor;
                break;
            case "ISTJ":
                mbticolor = new Color(269f / 255f, 226f / 255f, 245f / 255f);
                endingCanvasTextDetail.text = "당신은 엄청난 집중력과 고도의 침착함으로 훌륭한 조사관이 되었습니다.";
                endingCanvasTextDetail.color = mbticolor;
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
