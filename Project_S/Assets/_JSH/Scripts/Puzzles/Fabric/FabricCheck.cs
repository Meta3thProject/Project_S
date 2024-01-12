using System.Collections.Generic;
using UnityEngine;

public class FabricCheck : MonoBehaviour, IActiveSign
{
    // { BSJ PuzzleManager 에 맞춰서 추가된 변수들
    // 이 퍼즐의 인덱스
    const int PUZZLEINDEX = 10;
    // 퍼즐 클리어 팻말
    [SerializeField] private GameObject clearSign;
    // } BSJ PuzzleManager 에 맞춰서 추가된 변수들

    public PlayerStat playerStat;

    public List<GameObject> fabrics;


    private void Awake()
    {
        fabrics = new List<GameObject>();
    }

    // 클리어 팻말의 활성화 여부
    public void ActiveClearSign(bool _isClear)
    {
        clearSign.SetActive(_isClear);
    }

    public void CompleteCheck()
    {
        // 이미 완료된 상태라면 함수 종료
        if (PuzzleManager.instance.puzzles[PUZZLEINDEX] == true) { return; }

        FabricStat stat_ = GetComponent<FabricCheck>().CheckFabric();

        if (stat_ == null) { return; }
        else if (stat_ != null)
        {
            // 퍼즐 클리어 체크
            PuzzleManager.instance.puzzles[PUZZLEINDEX] = true;

            // 별의 총 갯수 증가
            StartCoroutine(StarManager.starManager.CallStar());

            // MBTI 상승
            playerStat.AddPoint(stat_.target.ToString(), stat_.amount);

            // 별 구역의 클리어 체크
            PuzzleManager.instance.CheckPuzzleClear(PUZZLEINDEX, true);

            // 파이어베이스 RDB에 업데이트
            FirebaseManager.instance.PuzzleClearUpdateToDB(PUZZLEINDEX, true);

            // 팻말 활성화
            ActiveClearSign(true);
        }
    }

    public FabricStat CheckFabric()
    {
        for (int i = 0; i < fabrics.Count; i++)
        {
            if (fabrics[i].GetComponent<FabricStat>() != null)
            {
                return fabrics[i].GetComponent<FabricStat>();
            }
        }

        return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 리스트에 추가
        fabrics.Add(other.gameObject);

        CompleteCheck();
    }

    private void OnTriggerExit(Collider other)
    {
        // 리스트에서 제거
        fabrics.Remove(other.gameObject);
    }
}
