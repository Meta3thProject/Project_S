using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // 싱글톤
    public static PlayerInventory instance;

    // 저장할 아이템 갯수들
    [Header("ItemType0")]           // 아이템 타입 0 : 일반 자원
    public int wood;                // 100008 : 목재
    public int stone;               // 100009 : 돌멩이
    public int iron;                // 100010 : 철

    [Space]
    [Header("ItemType1")]           // 아이템 타입 1 : 공용 자원

    [Space]
    [Header("ItemType2")]           // 아이템 타입 2 : 에피소드 자원
    public int ice;                 // 101001 : 얼음

    [Space]
    [Header("ItemType3")]           // 아이템 타입 3 : 특수 아이템 재료
    public int oil;                 // 100011 : 기름

    [Space]
    [Header("ItemType4")]           // 아이템 타입 4 : 특수 아이템
    public int torch;               // 100004 : 토치
    public int flashlight;          // 100005 : 손전등
    public int brush;               // 100006 : 붓

    [Space]
    [Header("ItemType5")]           // 아이템 타입 5 : 퀘스트 재료 / 아이템
    public int toyParts;            // 101002 : 장난감 부품
    public int bond;                // 101003 : 본드
    public int repairedToy;         // 101004 : 수리한 장난감
    public int cookie;              // 101005 : 쿠기
    public int wrappingPaper;       // 101006 : 포장지
    public int deliciousSnack;      // 101007 : 맛있는 과자
    public int thickThread;         // 101008 : 두꺼운 실
    public int knittingNeedles;     // 101009 : 뜨개질바늘
    public int warmShawl;           // 101010 : 따뜻한 목도리
    public int film;                // 101011 : 필름
    public int cameraWithoutFilm;   // 101012 : 필름 없는 카메라
    public int camera;              // 101013 : 카메라

    [Space]
    [Header("ItemType6")]           // 아이템 타입 6 : 벨트 아이템
    public int spiritAxe;           // 100000 : 정령의 도끼
    public int magicMap;            // 100001 : 마법 지도


    // 아이템 정보를 딕셔너리 형식으로 DB로 보낼 예정.
    public Dictionary<string, int> itemType0 = new Dictionary<string, int>();
    public Dictionary<string, int> itemType1 = new Dictionary<string, int>();
    public Dictionary<string, int> itemType2 = new Dictionary<string, int>();
    public Dictionary<string, int> itemType3 = new Dictionary<string, int>();
    public Dictionary<string, int> itemType4 = new Dictionary<string, int>();
    public Dictionary<string, int> itemType5 = new Dictionary<string, int>();
    public Dictionary<string, int> itemType6 = new Dictionary<string, int>();

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        // 초기 아이템 초기화 셋팅하기
        ItemInit();

        // 초기 딕셔너리 셋팅하기 ( 6은 아이템 타입 )
        for(int i = 0; i <= 6; i++)
        {
            SetItemsDictionry(i);
        }
    }


    /// <summary>
    /// 모든 아이템 변수 초기화 하는 함수.
    /// </summary>
    public void ItemInit()
    {
        wood = 0; stone = 0; iron = 0;

        ice = 0;
        oil = 0;
        torch = 0; flashlight = 0; brush = 0;
        toyParts = 0; bond = 0; repairedToy = 0; cookie = 0; wrappingPaper = 0; deliciousSnack = 0; thickThread = 0; knittingNeedles = 0; warmShawl = 0; film = 0; cameraWithoutFilm = 0; camera = 0;
        spiritAxe = 0; magicMap = 0;
    }

    /// <summary>
    /// DB에 저장될 딕셔너리를 셋팅하는 함수.
    /// </summary>
    /// <param name="_itemType"></param>
    public void SetItemsDictionry(int _itemType)
    {
        switch(_itemType)
        {
            // 아이템 타입 0 셋팅
            case 0:
                itemType0.Add("wood", wood);
                itemType0.Add("stone", stone);
                itemType0.Add("iron", iron);
                break;

            // 아이템 타입 1 셋팅
            case 1:
                // 아직 아무것도 없음.
                break;

            // 아이템 타입 2 셋팅
            case 2:
                itemType2.Add("ice", ice);
                break;

            // 아이템 타입 3 셋팅
            case 3:
                itemType3.Add("oil", oil);
                break;

            // 아이템 타입 4 셋팅
            case 4:
                itemType4.Add("torch", torch);
                itemType4.Add("flashlight", flashlight);
                itemType4.Add("brush", brush);
                break;

            // 아이템 타입 5 셋팅
            case 5:
                itemType5.Add("toyParts", toyParts);
                itemType5.Add("bond", bond);
                itemType5.Add("repairedToy", repairedToy);
                itemType5.Add("cookie", cookie);
                itemType5.Add("wrappingPaper", wrappingPaper);
                itemType5.Add("deliciousSnack", deliciousSnack);
                itemType5.Add("thickThread", thickThread);
                itemType5.Add("knittingNeedles", knittingNeedles);
                itemType5.Add("warmShawl", warmShawl);
                itemType5.Add("film", film);
                itemType5.Add("cameraWithoutFilm", cameraWithoutFilm);
                itemType5.Add("camera", camera);
                break;

            case 6:
                itemType6.Add("spiritAxe", spiritAxe);
                itemType6.Add("magicMap", magicMap);
                break;

        }
    }

    /// <summary>
    /// DB에 저장될 딕셔너리를 초기화 하는 함수.
    /// </summary>
    /// <param name="_itemType"></param>
    public void ItemDictionaryInit(int _itemType)
    {
        switch(_itemType)
        {
            case 0:
                itemType0.Clear();
                break;
            case 1:
                itemType1.Clear();
                break;
            case 2:
                itemType2.Clear();
                break;
            case 3:
                itemType3.Clear();
                break;
            case 4:
                itemType4.Clear();
                break;
            case 5:
                itemType5.Clear();
                break;
            case 6:
                itemType6.Clear();
                break;
        }
    }

    //! TEST : 사과 갯수 증가하는 함수
    public void PlusWood()
    {
        wood++;
        PlusItemCount(itemType0, "wood");
    }

    /// <summary>
    /// 아이템 딕셔너리에 값을 추가하는 함수.
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="key"></param>
    public void PlusItemCount(Dictionary<string, int> dictionary, string key)
    {
        // 딕셔너리에 키가 이미 존재하는지 확인
        if (dictionary.ContainsKey(key))
        {
            // 키가 존재하면 값을 증가시킴
            dictionary[key]++;
        }
        else { /* Do Nothing */ }
    }
}
