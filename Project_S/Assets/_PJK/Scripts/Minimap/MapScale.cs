using BNG;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
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
    public GameObject worldmap = default;
    public GameObject zonemap = default;

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
    public bool iszone1 = true;
    public bool iszone2 = false;
    public bool iszone3 = false;
    public bool iszone4 = false;
    public bool iszone5 = false;
    //현재 켜진 맵이 ZoneMap인지 WorldMap인지?
    public bool isZoneMap = true;
    public bool isWorldMap = false;
    //맵이 켜져있는지?
    public bool isMapOpen = false;
    //위치 지정 현재 플레이어의 위치
    private Vector3 pos = default;

    public GameObject[] player = new GameObject[5];

    //카메라세팅 기본Zone세팅//Zone4세팅//Zone5세팅
    private Rect zone1Rect = new Rect(0f, 0f, 1f, 1f);
    private Rect zone2Rect = new Rect(0f, 0f, 1f, 1f);
    private Rect zone3Rect = new Rect(0f, 0f, 1f, 1f);
    private Rect zone4Rect = new Rect(0f, 0f, 1f, 1f);
    private Rect zone5Rect = new Rect(0f, 0f, 1f, 1f);
    //미니맵 마크를 가져올 미니맵마커매니져인스턴스


    // HSJ_ 240115
    private Vector3 offset = new Vector3(-0.1f, 0f, 0f);
    private SoundManager soundManager = default;

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
        worldmap.SetActive(false);
        isWorldMap = false;
        isZoneMap = true;

        // HSJ_ 240117
        soundManager = SoundManager.Instance;
    }



    private void Update()
    {
        pos = NPCManager.Instance.player.transform.position;

        // LEGACY :
        // pos로 실시간으로 해당위치를 받아와서 Sound를 교체해줘야하는데 맵을 열었을 때만 zone을 업데이트 하므로
        // 실시간으로 포지션을 업데이트 하도록 변경함

        //if (isMapOpen == false)
        //{
        //    if(this.transform.parent != null && this.transform.parent.name == "HolsterRight")
        //    {
                
        //        this.transform.localPosition = offset;
        //    }       // if : SnapZone에 들어 갔을 떄 포지션 Offset으로 맞춰주기
        //    pos = NPCManager.Instance.player.transform.position;
        //    return;
        //}
        //else if (isMapOpen == true)
        //{
        //    pos = NPCManager.Instance.player.transform.position;
        //}

        if (Input.GetKeyDown(KeyCode.O)||(InputBridge.Instance.RightTriggerDown || InputBridge.Instance.LeftTriggerDown) && ((thisGrabber.HandSide == ControllerHand.Left) || (thisGrabber.HandSide == ControllerHand.Right)))
        {
            Debug.Log("O버튼 누름");
            if (isMapOpen == true && isWorldMap == false && isZoneMap == true)
            {
                isWorldMap = true;
                isZoneMap = false;
                worldmap.SetActive(true);
                zonemap.SetActive(false);
            }
            else if (isMapOpen == true && isWorldMap == true && isZoneMap == false)
            {
                isWorldMap = false;
                isZoneMap = true;
                worldmap.SetActive(false);
                zonemap.SetActive(true);
            }
        }

        //zone1map
        if (-27f <= pos.x && pos.x <= 2f && 11.35f < pos.z && pos.z < 86f)
        {
            soundManager.ChangeBGM(SoundManager.BGMState.TUTORIAL);            
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
            soundManager.ChangeBGM(SoundManager.BGMState.MAIN);
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
        else if (pos.x < -54)
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
        else if (95f < pos.x)
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
            worldmap.SetActive(true);
            MapName.text = "전체지도";
        }

    }


    public void Zone1()
    {
        cam.orthographicSize = 29f;
        minimapCamera.transform.position = zone1pos.transform.position;
        cam.rect = zone1Rect;
        outline.anchoredPosition = new Vector2(0f, 0f);
        outline.sizeDelta = new Vector2(900f, 900f);
        MapName.text = "정령의 섬";
    }
    public void Zone2()
    {

        cam.orthographicSize = 75f;
        minimapCamera.transform.position = zone2pos.transform.position;
        cam.rect = zone2Rect;
        outline.anchoredPosition = new Vector2(0f, 0);
        outline.sizeDelta = new Vector2(900f, 900f);
        MapName.text = "중앙 섬";
    }
    //zone3map
    public void Zone3()
    {

        cam.orthographicSize = 58.6f;
        minimapCamera.transform.position = zone3pos.transform.position;
        cam.rect = zone3Rect;
        outline.anchoredPosition = new Vector2(0f, 0);
        outline.sizeDelta = new Vector2(900f, 900f);
        MapName.text = "도구섬";

    }
    //zone4map
    public void Zone4()
    {

        cam.orthographicSize = 42.9f;
        minimapCamera.transform.position = zone4pos.transform.position;
        cam.rect = zone4Rect;
        outline.anchoredPosition = new Vector2(0f, 0);
        outline.sizeDelta = new Vector2(900f, 900f);
        MapName.text = "외부섬";
    }

    //zone5map
    public void Zone5()
    {

        cam.orthographicSize = 42.9f;
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
