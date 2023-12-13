using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : MonoBehaviour, IMeltable
{
    // 시간 차지 여부
    private bool isCharging = default;
    // 녹기에 충분한 시간
    private const float CAN_MELT_TIME = 3.0f;
    // 차지된 시간
    private float chargedTime = default;

    private void Awake()
    {
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
                Melting(0);
            }
        }
        // 시간 차지 중이 아니라면 통과
        else { /* Do Nothing */ }
    }

    public void Melting(int grade_)
    {
        // 녹아 사라짐
        gameObject.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        // "Torch"가 아니라면 함수 종료
        if (!other.gameObject.name.Equals("Torch")) { return; }
        // "Torch"라면 시간 차지
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
