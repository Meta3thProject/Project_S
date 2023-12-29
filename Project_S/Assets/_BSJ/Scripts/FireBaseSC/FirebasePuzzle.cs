using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebasePuzzle : MonoBehaviour
{
    // 퍼즐의 갯수 30개
    const int GAME_PUZZLE_COUNT = 30;

    // 싱글톤
    public static FirebasePuzzle Instance;

    // 퍼즐 클리어 정보를 딕셔너리 형식으로 DB에 보낼 예정
    public Dictionary<int, bool> PuzzleClearDictionary = new Dictionary<int, bool>();

    private void Awake()
    {
        // { 싱글톤
        if (null == Instance)
        {
            Instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }       // } 싱글톤

        // 퍼즐 딕셔너리 초기화
        PuzzleClearInit();
    }

    /// <summary>
    /// 퍼즐 클리어 딕셔너리 안에 모든 퍼즐의 클리어를 false로 변경하는 메서드.
    /// </summary>
    public void PuzzleClearInit()
    {
        for(int i = 0; i < GAME_PUZZLE_COUNT; i++)
        {
            // 이미 key가 존재한다면, value 변경
            if (PuzzleClearDictionary.ContainsKey(i))
            {
                PuzzleClearDictionary[i] = false;
                continue;
            }

            // key가 없다면 추가
            PuzzleClearDictionary.Add(i, false);
        }
    }

    /// <summary>
    /// 퀘스트 딕셔너리에 퀘스트 클리어를 true로 변경하는 메서드.
    /// </summary>
    /// <param name="_key">몇 번째 퍼즐인가</param>
    public void PuzzleClearUpdateToDB(int _key)
    {
        // 딕셔너리에 키가 이미 존재하는지 확인
        if (PuzzleClearDictionary.ContainsKey(_key))
        {
            // 키가 존재하면 value값을 true로 변환
            PuzzleClearDictionary[_key] = true;
        }

        else
        {
            // 키가 존재하지 않는다면 key 값을 추가 후 true로 변환
            PuzzleClearDictionary.Add(_key, true);
        }

    }
}
