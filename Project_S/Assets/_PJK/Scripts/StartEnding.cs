using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class StartEnding : MonoBehaviour
{
    bool endingtalk = false; //엔딩전대사가 끝났는지?
    private GameObject star = default; //starObject
    private List<GameObject> stars = default; //star
    private int starnumber = default; //star의 갯수
    private DG.Tweening.Sequence StarAppearSequence;
    public GameObject Player = default;

    public Vector3 targetPosition; // 오브젝트가 도착할 목표 위치
    public int rows = 3; // 행 수
    public int objectsPerRow = 10; // 한 행당 오브젝트 수
    public float spacing = 2.0f; // 오브젝트 간 간격
    void Start()
    {
        star = GetComponentInChildren<GameObject>();
    }

    void Update()
    {

    }


    public void startEnding()
    {
        Player = Player.gameObject;

        if (StarManager.starManager.getStarCount >= 22 && endingtalk == true)
        {
            starnumber = StarManager.starManager.getStarCount;
            for (int row = 0; row < rows; row++)
            {
                for (int i = 0; i < objectsPerRow; i++)
                {

                    stars.Add(Instantiate(gameObject, Player.transform));
                    Vector3 newPos = new Vector3(i * spacing, row * spacing, 0); // 새로운 위치 계산
                    stars[i].transform.localPosition = newPos; // 새로운 위치로 오브젝트 이동

                    StarAppearSequence = DOTween.Sequence().SetAutoKill(false).
                    // DOTween을 사용하여 오브젝트를 목표 위치로 이동시킵니다.
                    Append(stars[i].transform.DOSpiral(10f, Vector3.forward, SpiralMode.Expand, 5, 10, 10, true)).
                        Join(stars[i].transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 3f)).
                        Join(stars[i].transform.DOMove(targetPosition, Random.Range(1.0f, 3.0f))
                        .SetDelay(row * objectsPerRow * 0.1f + i * 0.1f) // 딜레이 설정
                        .SetEase(Ease.OutQuad)); // 이징(Ease) 설정
                }
            }
        }
    }



    public void StartEndingtalk()
    {
        endingtalk = true;
        startEnding();
    }
}
