using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class ButcherShop01Clear : MonoBehaviour, IActiveSign
{
    const int PUZZLEINDEX = 12;      // 이 퍼즐의 번호는 12번 입니다.

    // 현재 생성되어 있는 고기의 카운트
    [field: SerializeField] public int iceCubeCount { get; set; }

    // 얼음 프리팹들
    [Header("얼음 프리팹 - 닭고기, 소고기, 돼지고기")]
    [SerializeField] private List<GameObject> iceCube;

    // 얼음 프리팹들의 시작 위치
    [SerializeField] private List<Vector3> iceCubeStartPosition;

    // 레버
    [SerializeField] private GameObject leftLever;
    [SerializeField] private GameObject RightLever;

    // 스팟 트리거
    [SerializeField] private SpotTrigger spotTrigger;

    // 레버의 시작 회전값
    private Quaternion leftLeverRotate;
    private Quaternion RightLeverRotate;

    // 성공 했는지 O, X 띄워주는 캔버스 && 텍스트
    [SerializeField] private GameObject _canvasObj;
    [SerializeField] private TMP_Text _tmp_Text;

    // 퍼즐 클리어 팻말
    [SerializeField] private GameObject clearSign;

    private void Awake()
    {
        // 처음 고기의 시작 갯수는 9개
        iceCubeCount = 9;

        // 레버 시작 위치 캐싱
        leftLeverRotate = leftLever.transform.rotation;
        RightLeverRotate = RightLever.transform.rotation;

        // 얼음 고기 관련 캐싱.
        for (int i = 0; i < transform.GetChild(0).GetChild(0).childCount; i++) 
        {
            // 얼음 고기 오브젝트를 리스트에 추가
            iceCube.Add(transform.GetChild(0).GetChild(0).GetChild(i).gameObject);

            // 얼음 고기 시작 위치 캐싱
            iceCubeStartPosition.Add(transform.GetChild(0).GetChild(0).GetChild(i).gameObject.transform.position);
        }

        // 캔버스 비활성화
        _canvasObj.gameObject.SetActive(false);
    }

    /// <summary>
    /// 아이템을 솥에 넣는 메서드.
    /// </summary>
    public void PutItem()
    {
        // 레버 두트윈
        leftLever.transform.DORotate(new Vector3(-45f, 0, 0), 3f);
        RightLever.transform.DORotate(new Vector3(45f, 0, 0), 3f);

        // 물리 체크 해제
        foreach (GameObject obj in iceCube)
        {
            IceCube iceCube = obj.GetComponent<IceCube>();
            iceCube.Clear_Constraints();
        }
    }

    /// <summary>
    /// 퍼즐 리셋하는 메서드.
    /// </summary>
    public void ResetItem()
    {
        // 캔버스 오브젝트 비활성화
        _tmp_Text.text = "";
        _canvasObj.SetActive(false);

        // 스팟 트리거의 정답 체크 초기화
        spotTrigger.ResetCounting();

        // 시작 고기 갯수 초기화
        iceCubeCount = 9;
        
        // 레버 원위치
        leftLever.transform.rotation = leftLeverRotate;
        RightLever.transform.rotation = RightLeverRotate;

        // 얼음 고기 아이템 모두 활성화
        for (int i = 0; i < transform.GetChild(0).GetChild(0).childCount; i++)
        {
            // 위치 초기화
            GameObject go = transform.GetChild(0).GetChild(0).GetChild(i).gameObject;
            IceCube iceCube = go.GetComponent<IceCube>();
            iceCube.Set_Constraints();
            go.transform.rotation = Quaternion.identity;
            go.transform.position = iceCubeStartPosition[i];
            go.SetActive(true);
        }
    }

    /// <summary>
    /// 클리어하는 메서드.
    /// </summary>
    public void ButcherShopClear()
    {
        // 이미 퍼즐을 클리어 했다면 리턴
        if (PuzzleManager.instance.puzzles[PUZZLEINDEX] == true) { return; }

        // 퍼즐 클리어 체크
        PuzzleManager.instance.puzzles[PUZZLEINDEX] = true;

        // 별의 총 갯수 증가
        StartCoroutine(StarManager.starManager.CallStar());

        // 별 구역의 클리어 체크
        PuzzleManager.instance.CheckPuzzleClear(PUZZLEINDEX, true);

        // 파이어베이스 RDB에 업데이트
        FirebaseManager.instance.PuzzleClearUpdateToDB(PUZZLEINDEX, true);

        // 클리어 팻말 활성화
        ActiveClearSign(true);
    }

    /// <summary>
    /// 클리어하지 못했을 때 X 출력이 되는 메서드.
    /// </summary>
    public void NoClearText()
    {
        // 캔버스 활성화
        _canvasObj.SetActive(true);
        // 테스트 변경
        _tmp_Text.text = "X";
        _tmp_Text.color = Color.red;
    }

    /// <summary>
    /// 클리어 했을 때 O 출력이 되는 메서드.
    /// </summary>
    public void ClearText()
    {
        // 캔버스 활성화
        _canvasObj.SetActive(true);
        // 테스트 변경
        _tmp_Text.text = "O";
        _tmp_Text.color = Color.yellow;
    }

    /// <summary>
    /// 클리어 팻말의 활성화 여부
    /// </summary>
    /// <param name="_isClear"></param>
    public void ActiveClearSign(bool _isClear)
    {
        clearSign.SetActive(_isClear);
    }
}
