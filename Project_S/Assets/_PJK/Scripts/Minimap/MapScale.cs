using BNG;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapScale : GrabbableEvents
{
    public static MapScale instance;
    //플레이어 위치
    public GameObject Player;
    //카메라
    public GameObject minimapCamera = default;
    //카메라에서 받아오는 하위카메라컴포넌트
    private Camera cam = default;
    //카메라 텍스쳐
    public RenderTexture minimapCameraTexture = default;
    //카메라텍스쳐를 입힐 이미지
    public RawImage minimapImage = default;

    public GameObject outLine = default;
    private RectTransform outline = default;
    //지역정보를 위한 이름
    public TextMeshProUGUI MapName = default;
    public Image WorldMap = default;
    //각 카메라 위치
    public GameObject zone1pos;
    public GameObject zone2pos;
    public GameObject zone3pos;
    public GameObject zone4pos;
    public GameObject zone5pos;

    public TextMeshProUGUI zoneClear;



    //현재 플레이어가 어느 존에 있는지?
    private bool iszone1 = true;
    private bool iszone2 = false;
    private bool iszone3 = false;
    private bool iszone4 = false;
    private bool iszone5 = false;
    //현재 켜진 맵이 ZoneMap인지 WorldMap인지?
    private bool isZoneMap = false;
    private bool isWorldMap = true;
    //맵이 켜져있는지?
    public bool isMapOpen = false;
    //위치 지정 현재 플레이어의 위치
    private Vector3 pos = default;

    public GameObject[] player = new GameObject[5];

    //카메라세팅 기본Zone세팅//Zone4세팅//Zone5세팅
    private Rect zone1Rect = new Rect(0f, 0.55f, 1f, 1f);
    private Rect zone2Rect = new Rect(0.2f, 0.2f, 0.6f, 0.6f);
    private Rect zone3Rect = new Rect(0.2f, 0.2f, 0.6f, 0.6f);
    private Rect zone4Rect = new Rect(0.2f, 0.41f, 0.57f, 1f);
    private Rect zone5Rect = new Rect(0.2f, 0.2f, 0.6f, 0.6f);
    //미니맵 마크를 가져올 미니맵마커매니져인스턴스

    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        // { 싱글톤
        if (null == instance)
        {
            instance = this;

            // LEGACY : 메인 씬에서만 PuzzleManager를 사용할 예정
            // DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        cam = minimapCamera.GetComponent<Camera>();
        //minimapImage.texture = minimapMaxCameraTexture;
        outline = outLine.GetComponent<RectTransform>();
    }




    private void Update()
    {
        if (input.RightTriggerDown && thisGrabber.HeldGrabbable.tag.Equals("MiniMap"))
        {



            if (isZoneMap == false)
            {
                isZoneMap = true;
                isWorldMap = false;
            }
            else if (isWorldMap == false)
            {
                isZoneMap = false;
                isWorldMap = true;
            }

        }




        if (isMapOpen == false)
        {
            return;
        }
        else if (isMapOpen == true)
        {
            pos = NPCManager.Instance.player.transform.position;
        }


        //zone1map
        if (-27f <= pos.x && pos.x <= 0f && 11.35f < pos.z && pos.z < 86f)
        {
            iszone1 = true;
            iszone2 = false;
            iszone3 = false;
            iszone4 = false;
            iszone5 = false;
            if (isZoneMap == true)
            {
                Zone1();
                //TODO : zone 1 퍼즐 체크해서 몇개 깼는지 체크하는 메서드 추가.

            }
            else if (isWorldMap == true)
            {
                player[0].SetActive(true);
            }
        }
        //zone2map
        else if (-72f <= pos.x && pos.x <= 105f && -115.8f < pos.z && pos.z < 11.35f)
        {
            iszone1 = false;
            iszone2 = true;
            iszone3 = false;
            iszone4 = false;
            iszone5 = false;
            if (isZoneMap == true)
            {
                Zone2();

            }
            else if (isWorldMap == true)
            {
                player[1].SetActive(true);
            }

        }
        //zone3map
        else if (-71f <= pos.x && -70f < pos.z && pos.z < -20f)
        {
            iszone1 = false;
            iszone2 = false;
            iszone3 = true;
            iszone4 = false;
            iszone5 = false;
            if (isZoneMap == true)
            {
                Zone3();

            }
            else if (isWorldMap == true)
            {
                player[2].SetActive(true);
            }

        }
        //zone4map
        else if (94.5f <= pos.x && -111.3f < pos.z && pos.z < -25.5f)
        {
            iszone1 = false;
            iszone2 = false;
            iszone3 = false;
            iszone4 = true;
            iszone5 = false;
            if (isZoneMap == true)
            {
                Zone4();

            }
            else if (isWorldMap == true)
            {
                player[3].SetActive(true);
            }

        }
        //zone5map
        else if (-47f <= pos.x && pos.x < 71f && pos.z < -120f)
        {
            iszone1 = false;
            iszone2 = false;
            iszone3 = false;
            iszone4 = false;
            iszone5 = true;
            if (isZoneMap == true)
            {
                Zone5();

            }
            else if (isWorldMap == true)
            {
                player[4].SetActive(true);
            }

        }
        //worldmap
        if (isWorldMap == true)
        {
            cam.orthographicSize = 76f;
            minimapCamera.transform.position = zone2pos.transform.position;
            cam.rect = zone4Rect;
            MapName.text = "전체지도";
        }

    }
    public void Zone1()
    {

        cam.orthographicSize = 33.8f;
        minimapCamera.transform.position = zone1pos.transform.position;
        cam.rect = zone1Rect;
        outline.anchoredPosition = new Vector2(0f, 0f);
        outline.sizeDelta = new Vector2(900f, 900f);
        MapName.text = "정령의 섬";
    }
    public void Zone2()
    {

        cam.orthographicSize = 130f;
        minimapCamera.transform.position = zone2pos.transform.position;
        cam.rect = zone2Rect;
        outline.anchoredPosition = new Vector2(-10.3f, 0);
        outline.sizeDelta = new Vector2(720.47f, 910f);
        MapName.text = "중앙 섬";
    }
    //zone3map
    public void Zone3()
    {

        cam.orthographicSize = 58.6f;
        minimapCamera.transform.position = zone3pos.transform.position;
        cam.rect = zone3Rect;
        outline.anchoredPosition = new Vector2(-10.3f, 0);
        outline.sizeDelta = new Vector2(720.47f, 910f);
        MapName.text = "도구섬";

    }
    //zone4map
    public void Zone4()
    {

        cam.orthographicSize = 55f;
        minimapCamera.transform.position = zone4pos.transform.position;
        cam.rect = zone4Rect;
        outline.anchoredPosition = new Vector2(-50f, 0);
        outline.sizeDelta = new Vector2(638f, 900f);
        MapName.text = "외부섬";
    }

    //zone5map
    public void Zone5()
    {

        cam.orthographicSize = 79.5f;
        minimapCamera.transform.position = zone5pos.transform.position;
        cam.rect = zone5Rect;
        outline.anchoredPosition = new Vector2(0f, 0);
        outline.sizeDelta = new Vector2(900f, 900f);
        MapName.text = "대형 정육점";
    }






    public void Zone()
    {
        if (iszone1 == true)
        {
            minimapCamera.transform.position = zone1pos.transform.position;

        }
        else if (iszone2 == true)
        {
            minimapCamera.transform.position = zone2pos.transform.position;

        }

        else if (iszone3 == true)
        {
            minimapCamera.transform.position = zone3pos.transform.position;

        }
        else if (iszone4 == true)
        {
            minimapCamera.transform.position = zone4pos.transform.position;

        }

        else if (iszone5 == true)
        {
            minimapCamera.transform.position = zone5pos.transform.position;

        }

    }


    public void changetoWorldMap()
    {
        isWorldMap = true;
        isZoneMap = false;
        //minimapImage.texture = minimapZone1CameraTexture;


    }

    public void changetoZoneMap()
    {
        isZoneMap = true;
        isWorldMap = false;
        //minimapImage.texture = minimapZone1CameraTexture;
    }

    // BSJ 퍼즐 클리어 예시
    public void CheckZone1ClearPercent(int zoneIndex_)
    {
        int zoneClearCount = 0;

        if (zoneIndex_ == 1)
        {
            const int PUZZLECOUNT = 1;

            // zone1의 퍼즐의 번호들
            const int zone1_Puzzle01 = 13;

            // 퍼즐 번호의 배열
            int[] zone1Puzzle = new int[PUZZLECOUNT] { zone1_Puzzle01 };

            // 퍼즐 매니저의 클리어 배열이 클리어 되었는지 체크하는 로직
            for (int i = 0; i < PUZZLECOUNT; i++)
            {
                if (PuzzleManager.instance.puzzles[zone1Puzzle[i]] == true)
                {
                    zoneClearCount++;
                }
            }

            zoneClear.text = $"클리어 : {zoneClearCount} / {PUZZLECOUNT}";
        }

        else if (zoneIndex_ == 2)
        {
            const int PUZZLECOUNT = 12;

            // zone1의 퍼즐의 번호들
            const int zone2_Puzzle01 = 13;

            // TODO 12개의 퍼즐들

            // 퍼즐 번호의 배열
            //int[] zone2Puzzle = new int[PUZZLECOUNT] { zone2_Puzzle01 };

            // 퍼즐 매니저의 클리어 배열이 클리어 되었는지 체크하는 로직
            //for (int i = 0; i < PUZZLECOUNT; i++)
            //{
            //    if (PuzzleManager.instance.puzzles[zone1Puzzle[i]] == true)
            //    {
            //        zoneClearCount++;
            //    }
            //}

            zoneClear.text = $"클리어 : {zoneClearCount} / {PUZZLECOUNT}";
        }



    }
}
