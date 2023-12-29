using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;
using TMPro;

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

    // 현재 바구니에 들어간 고기의 갯수
    private int countInPot;

    // 스크립트 캐싱
    private ButcherShop02Clear butcherShop02Clear;
    private MeltIceCube iceCube;

    // 이펙트
    private ParticleSystem CorrectParticle;

    // 얼음 고기 조각을 뱉어내기 위해 필요한 값들
    private Transform target;

    [Header("틀린 얼음 고기를 뱉어내는 속도")]
    [SerializeField] private float speed;

    // TMP_Text
    private TMP_Text ox_text;
    private TMP_Text clear_text;

    private void Awake()
    {
        // 이 퍼즐의 인덱스는 15번입니다.
        butcherShop02Clear = transform.root.GetChild(15).GetComponent<ButcherShop02Clear>();

        // 이펙트 캐싱
        CorrectParticle = transform.GetChild(0).GetComponent<ParticleSystem>();

        // 튕겨내기 위한 타겟 위치 캐싱
        target = transform.GetChild(1).transform;
        ox_text = transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>();
        clear_text = transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>();

        // 큐브 타입에 따른 사이즈 정답과 갯수 정답
        SetAnswer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MeltIceCube>() != null)
        {
            iceCube = other.GetComponent<MeltIceCube>();
            other.gameObject.SetActive(false);

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
            countCheck = CHICKCOUNT;
        }

        else { /*Do Nothing*/ }
    }

    /// <summary>
    /// 얼음 고기 조각이 트리거에 닿았을 때 실행할 메서드.
    /// </summary>
    private void TouchIceCube(Collider other)
    {
        // 맞는 정답이라면, ( 타입과 사이즈가 같아야합니다. )
        if (iceCube.cubeType == triggerCubeType && iceCube.nowSize == sizeCheck)
        {
            // 현재 고기 갯수 이하라면 성공
            if (countInPot < countCheck)
            {
                // 성공 이펙트 출력 후 고기를 바구니에 넣기.
                CorrectParticle.Play();

                // O 텍스트 출력
                OutputText(true);

                // 바구니에 고기 갯수 추가하기.
                countInPot++;

                // 정답이라면 클리어 배열에 정답이라고 추가하기
                if(countInPot == countCheck)
                {
                    butcherShop02Clear.IncreaseClearCheck((int)triggerCubeType);
                    clear_text.text = "성공!";
                    clear_text.color = Color.yellow;
                }
            }

            // 현재 고기 갯수 이상이라면 실패
            else if (countInPot >= countCheck)
            {
                // X 텍스트 출력
                OutputText(false);

                // TODO : 바구니에서 고기 튀어 오르게 하기.
                other.gameObject.SetActive(true);
                iceCube.SpitOutToPot(target, speed);
            }
        }

        // 틀린 정답이라면 ( 타입 또는 사이즈가 다를 때 )
        else if (iceCube.cubeType != triggerCubeType || iceCube.nowSize != sizeCheck)
        {
            // X 텍스트 출력
            OutputText(false);

            // TODO : 바구니에서 고기 튀어 오르게 하기.
            other.gameObject.SetActive(true);
            iceCube.SpitOutToPot(target, speed);
        }
    }

    /// <summary>
    /// 고기를 넣었을 때 텍스트를 출력하는 메서드
    /// </summary>
    public void OutputText(bool _isAnswer)
    {
        if(_isAnswer)
        {
            ox_text.text = "0";
            ox_text.color = Color.yellow;
            StartCoroutine(ResetText());
        }
        else 
        {
            ox_text.text = "X";
            ox_text.color = Color.red;
            StartCoroutine(ResetText());
        }
    }

    /// <summary>
    /// 텍스트를 초기화하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator ResetText()
    {
        yield return new WaitForSecondsRealtime(1f);

        ox_text.text = "";
    }

}
