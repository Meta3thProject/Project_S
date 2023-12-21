using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButcherShop02Trigger : MonoBehaviour
{
    // 사이즈 정답 예시를 상수로 미리 캐싱
    const int COWSIZE = 0;
    const int PIGSIZE = 1;
    const int CHICKSIZE = 2;

    // 크기 정답 예시를 상수로 미리 캐싱
    const int COWCOUNT = 4;
    const int PIGCOUNT = 4;
    const int CHICKCOUNT = 2;

    // 고기 타입
    [field: SerializeField] public IceCubeType triggerCubeType { get; private set; }

    // 고기의 사이즈 정답과 갯수 정답
    private int sizeCheck;
    private int countCheck;

    private ButcherShop02Clear butcherShop02Clear;
    private MeltIceCube iceCube;

    private ParticleSystem CorrectParticle;
    private ParticleSystem WrongParticle;

    private void Awake()
    {
        butcherShop02Clear = transform.parent.GetComponent<ButcherShop02Clear>();

        // 큐브 타입에 따른 사이즈 정답과 갯수 정답
        SetAnswer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MeltIceCube>() != null)
        {
            TouchIceCube(other);
        }
    }

    /// <summary>
    /// 고기 타입에 따른 초기의 정답 셋팅
    /// </summary>
    private void SetAnswer()
    {
        if (triggerCubeType == IceCubeType.Meat)
        {
            sizeCheck = COWSIZE;
            countCheck = COWCOUNT;
        }

        else if (triggerCubeType == IceCubeType.Pork)
        {
            sizeCheck = PIGSIZE;
            countCheck = PIGCOUNT;
        }

        else if (triggerCubeType == IceCubeType.Chicken)
        {
            sizeCheck = CHICKSIZE;
            sizeCheck = CHICKCOUNT;
        }

        else { /*Do Nothing*/ }
    }

    /// <summary>
    /// 얼음 고기 조각이 트리거에 닿았을 때 실행할 메서드.
    /// </summary>
    private void TouchIceCube(Collider other)
    {
        iceCube = other.GetComponent<MeltIceCube>();

        // 게임오브젝트 비활성화
        other.gameObject.SetActive(false);

        // 맞는 정답이라면, ( 타입과 사이즈가 같아야합니다. )
        if (iceCube.cubeType == triggerCubeType)
        {
            // 성공 이펙트 출력
            CorrectParticle.Play();

            
        }

        // 틀린 정답이라면,
        else if (iceCube.cubeType != triggerCubeType)
        {
            // 실패 이펙트 출력
            WrongParticle.Play();
        }




        //맞다면 클리어 배열을 1로 만들기

        //아니라면 해당 얼음 고기 조각을 뱉어내기.
    }
}
