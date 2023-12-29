using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTestPuzzle : MonoBehaviour
{
    // 직렬화로 순서대로 할당
    public List<GameObject> fireBaseList;

    // 불을 붙인 횟수: 불이 붙일 때 증가
    public int ignitionCnt = default;

    // 순서대로 켜지는지 체크하는 함수
    public void IgnitionOrder()
    {
        // 불 붙이는 함수에서 마지막에 실행
        for (int i = 0; i < ignitionCnt; i++)
        {
            // 불이 켜졌나 확인
            if (fireBaseList[i].GetChildObj("Fire").activeSelf) { /* Do Nothing */ }
            // 불 붙인 횟수에 맞게 불이 켜져있으면 순서대로 킨거다
            else
            {
                // 불 붙인 횟수 리셋
                ignitionCnt = 0;
                // 지금까지 붙여진 불 꺼짐
                for (int j = 0; j < fireBaseList.Count; j++)
                {
                    GameObject fire = fireBaseList[j].GetChildObj("Fire");
                    // 켜진 불만 끈다
                    if (fire.activeSelf) { Unignite(j); }
                }

                // 불이 다 꺼지면 함수 종료
                return;
            }
        }

        // 만약 불 붙인 횟수와 모든 불 갯수가 같으면
        if (ignitionCnt == fireBaseList.Count)
        {
            OpenDoor();
        }
    }

    // 문 개방 함수
    private void OpenDoor()
    {
        transform.position += Vector3.up * 5;
    }

    // 인덱스를 매개변수로 받아서 불 점화
    public void Ignite(int idx_)
    {
        // 불 붙인 횟수 증가
        ignitionCnt += 1;

        // 해당 인덱스의 Fire 탐색 후 활성화
        fireBaseList[idx_].GetChildObj("Fire").SetActive(true);

        Invoke("IgnitionOrder", 0.5f);
    }

    // 인덱스를 매개변수로 받아서 불 소화
    public void Unignite(int idx_)
    {
        // 해당 인덱스의 Fire 탐색 후 비활성화
        fireBaseList[idx_].GetChildObj("Fire").SetActive(false);
    }
}
