using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerQuest : MonoBehaviour
{
    // 싱글톤
    public static PlayerQuest Instance;

    [Header("Zone5")]
    public bool zone5_Quest00;
    public bool zone5_Quest01;
    public bool zone5_Quest02;
    public bool zone5_Quest03;
    public bool zone5_Quest04;
    public bool zone5_Quest05;
    public bool zone5_Quest06;
    public bool zone5_Quest07;
    public bool zone5_Quest08;
    public bool zone5_Quest09;
    public bool zone5_Quest10;

    // 퀘스트 정보를 딕셔너리 형식으로 DB로 보낼 예정
    public Dictionary<int, bool> Zone5_QuestDictionary = new Dictionary<int, bool>();

    private void Awake()
    {
        if (null == Instance)
        {
            Instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        // 초기 퀘스트 초기화 셋팅하기
        QuestInit();

        // 초기 딕셔너리 셋팅하기 ( 5은 zone5 테스트  )
        SetQuestDictionry(5);
    }

    /// <summary>
    /// 퀘스트 딕셔너리에 퀘스트 클리어를 체크하는 함수
    /// </summary>
    /// <param name="_Zone"></param>
    /// <param name="key"></param>
    public void ClearQuest(Dictionary<int, bool> _Zone, int key)
    {
        // 딕셔너리에 키가 이미 존재하는지 확인
        if (_Zone.ContainsKey(key))
        {
            // 키가 존재하면 value값을 true로 변환
            _Zone[key] = true;
        }
    }

    /// <summary>
    /// 모든 아이템 변수 초기화 하는 함수.
    /// </summary>
    public void QuestInit()
    {
        zone5_Quest00 = false; zone5_Quest01 = false; zone5_Quest02 = false; 
        zone5_Quest03 = false; zone5_Quest04 = false; zone5_Quest05 = false; zone5_Quest06 = false;
        zone5_Quest07 = false; zone5_Quest08 = false; zone5_Quest09 = false; zone5_Quest10 = false;
    }

    /// <summary>
    /// DB에 저장될 퀘스트 딕셔너리를 셋팅하는 함수.
    /// </summary>
    /// <param name="_ZoneType"></param>
    public void SetQuestDictionry(int _ZoneType)
    {
        switch (_ZoneType)
        {
            case 5:
                Zone5_QuestDictionary.Add(301500, zone5_Quest00);
                Zone5_QuestDictionary.Add(301501, zone5_Quest01);
                Zone5_QuestDictionary.Add(301502, zone5_Quest02);
                Zone5_QuestDictionary.Add(301503, zone5_Quest03);
                Zone5_QuestDictionary.Add(301504, zone5_Quest04);
                Zone5_QuestDictionary.Add(301505, zone5_Quest05);
                Zone5_QuestDictionary.Add(301506, zone5_Quest06);
                Zone5_QuestDictionary.Add(301507, zone5_Quest07);
                Zone5_QuestDictionary.Add(301508, zone5_Quest08);
                Zone5_QuestDictionary.Add(301509, zone5_Quest09);
                Zone5_QuestDictionary.Add(301510, zone5_Quest10);
                break;

            default:
                break;
        }
    }

    //! 5는 zone 5 테스트
    /// <summary>
    /// DB에 저장될 딕셔너리를 초기화 하는 함수.
    /// </summary>
    /// <param name="_itemType"></param>
    public void QuestDictionaryInit(int _zoneType)
    {
        switch (_zoneType)
        {
            case 5:
                Zone5_QuestDictionary.Clear();
                break;

            default:
                break;
        }
    }
}
