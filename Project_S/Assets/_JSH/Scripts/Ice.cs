using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour, IMeltable
{
    // 녹아내림 단계
    public int grade = default;
    // 녹을 수 있는지 여부
    private bool isMelt = default;
    // 시간 차지 여부
    private bool isCharging = default;
    // 녹은 후 휴식 시간
    private const float REST_TIME = 2.0f;
    // 녹기에 충분한 시간
    private const float CAN_MELT_TIME = 3.0f;
    // 차지된 시간
    private float chargedTime = default;

    private void Awake()
    {
        // 0으로 시작
        grade = 0;
        // 녹을 수 있는 상태로 시작
        isMelt = true;

        isCharging = false;
        chargedTime = 0;
    }

    private void Update()
    {
        // 시간 차지 중이라면
        if (isCharging)
        {
            // 시간 차지
            chargedTime += Time.deltaTime;
            // 충전된 시간이 충분하면
            if (chargedTime >= CAN_MELT_TIME)
            {
                // 충전된 시간 리셋
                chargedTime = 0;
                // 시간 차지 중지
                isCharging = false;
                Melting(grade);
            }
        }
        // 시간 차지 중이 아니라면 통과
        else { /* Do Nothing */ }
    }

    // 녹을 수 있게 됨
    private void CanMelt()
    {
        // 녹을 수 있게 됨
        isMelt = true;
    }

    // 녹을 수 없게 됨
    private void CantMelt()
    {
        // 녹을 수 없게 됨
        isMelt = false;

        // 지정된 시간이 지난 뒤 녹을 수 있게 변경
        Invoke("CanMelt", REST_TIME);
    }

    public void Melting(int grade_)
    {
        // 녹아내림 단계 증가
        grade += 1;

        // 녹아내림 단계가 3이면
        if (grade == 3)
        {
            // 녹아 사라짐
            gameObject.SetActive(false);
            // 함수 종료
            return;
        }
        // 녹아내림 단계가 3이 아니면 통과
        else { /* Do Nothing */ }

        // 스케일 축소
        transform.localScale *= 0.8f;
        transform.position = new Vector3(transform.position.x, transform.localScale.y / 2, transform.position.z);

        // 녹지 않게 됨
        CantMelt();
    }

    //                                           //
    // 지금은 이름으로 체크하지만 레이어로 바꾸고 싶다 //
    //                                           //

    private void OnTriggerEnter(Collider other)
    {
        // 녹을 수 없거나 "Torch"가 아니라면 함수 종료
        if (!isMelt || !other.gameObject.name.Equals("Torch")) { return; }
        // 녹을 수 있으면 시간 차지
        else { isCharging = true; }
    }

    private void OnTriggerExit(Collider other)
    {
        // 시간 차지 중이라면
        if (isCharging)
        {
            // 시간 차지 중지
            isCharging = false;

            // 차지된 시간 리셋
            chargedTime = 0;
        }
    }
}
