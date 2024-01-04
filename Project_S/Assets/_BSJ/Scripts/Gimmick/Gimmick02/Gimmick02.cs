using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick02 : MonoBehaviour
{
    // 횃불을 두개 켜야합니다.
    const int GIMMICKCOUNT = 2;

    // 부셔질 문
    private GameObject door;

    // 문이 부셔질 때 나오는 이펙트
    private ParticleSystem doorBreakFX;

    // 횃불이 켜져 있는지 체크하는 배열
    private bool[] isFires = default;

    // 횃불 클리어 체크하는 변수
    int clearCheck = 0;

    private void Awake()
    {
        isFires = new bool[GIMMICKCOUNT] { false, false };

        // 문 && 이펙트 캐싱
        doorBreakFX = transform.GetChild(0).transform.GetComponent<ParticleSystem>();
        door = transform.GetChild(1).transform.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SpiritHand"))
        {
            // 횃불이 전부 켜져있는지 체크
            foreach(bool _noClear in isFires) 
            {
                if(_noClear == false) { clearCheck++; }
            }

            // 횃불이 클리어 되었으면, 기믹2 클리어
            if(clearCheck == 0)
            {
                doorBreakFX.Play();
                door.gameObject.SetActive(false);
            }

            // 클리어 체크 초기화
            clearCheck = 0;
        }
    }

    /// <summary>
    /// 어떤 불이 켜진지 체크
    /// </summary>
    /// <param name="_index">횃불의 인덱스 0 || 1</param>
    public void FireOn(int index_)
    {
        if(index_ == 0 || index_ == 1)
        {
            isFires[index_] = true;
        }

        else { /*DoNothing*/ }
    }
}
