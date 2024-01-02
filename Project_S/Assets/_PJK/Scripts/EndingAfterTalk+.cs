using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EndingAfterTalk : MonoBehaviour
{
    //플레이어의 카메라
    public Camera PlayerCamera;
    //bool값으로 엔딩대화가 끝났는지 구분
    bool endingtalk = false; //엔딩전대사가 끝났는지?
    //별 오브젝트
    [SerializeField]
    private GameObject star; //starObject
    //별을 담을 리스트
    private List<GameObject> stars = default; //star
    private int starnumber = default; //star의 갯수
    private DG.Tweening.Sequence StarAppearSequence;
    public GameObject Player = default;
    public GameObject endingstarposition = default;
    public GameObject Sphere = default;

    private Vector3 targetPosition; // 오브젝트가 도착할 목표 위치
    public Canvas EndingCanvas;
    public Material Sky_Mat;
    public bool enableFog = true; // Fog를 사용할지 여부
    public float fogDensity = 0.01f; // Fog 밀도
    public Color Color = default;//new Color(0.5f, 0.5f, 0.5f);  Fog 색상 (R, G, B 값으로 설정)

    public void Light()
    {
        RenderSettings.fog = enableFog; // Fog 사용 여부 설정
        MBTIColor();
        if (enableFog)
        {

            RenderSettings.fogColor = Color;
            RenderSettings.fogMode = FogMode.Exponential; // Fog 모드 설정 (Exponential, ExponentialSquared, Linear 중 선택)
            RenderSettings.fogDensity = fogDensity; // Fog 밀도 설정
        }

        GoddessSphere.goddess.EndingMBTI();//Material색깔 초기화
    }

    private void MBTIColor()
    {

        /*   
          if(MBTI= enfp)
          {
              Color = new Color(230,50,49);
          }
          else if(MBTI = entp)
          {
              Color = new Color(255,128,13);
          }
          else if (MBTI = enfj)
          {
              Color = new Color(116,76,40);
          }
          else if (MBTI = entj)
          {
              Color = new Color(9,125,236);
          }
          else if (MBTI = esfj)
          {
              Color = new Color(186,188,3);
          }
          else if (MBTI = estj)
          {
              Color = new Color(8,37,105);
          }
          else if (MBTI = esfp)
          {
              Color = new Color(255,209,18);
          }
          else if (MBTI = estp)
          {
              Color = new Color(214,237,0);
          }
          else if (MBTI = infj)
          {
              Color = new Color(180,180,182);
          }
          else if (MBTI = intp)
          {
              Color = new Color(35,24,22);
          }
          else if (MBTI = infp)
          {
              Color = new Color(255,255,255);
          }
          else if (MBTI = intj)
          {
              Color = new Color(130,103,184);
          }
          else if (MBTI = isfj)
          {
              Color = new Color(253,228,198);
          }
          else if (MBTI = istp)
          {
              Color = new Color(202,186,248);
          }
          else if (MBTI = isfp)
          {
              Color = new Color(254,207,215);
          }
          else if (MBTI = istj)
          {
              Color = new Color(269,226,245);
          }

          */



    }

    IEnumerator SEtFogDensity()
    {
        for (int i = 0; i < 100; i++)
        {
            fogDensity += (0.01f * i); 
            RenderSettings.fogDensity = fogDensity; // Fog 밀도 설정


            yield return new WaitForSeconds(0.1f);
        }
    }
}
