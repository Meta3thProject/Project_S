using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectReturnPool : MonoBehaviour
{
    // 이펙트 타입
    [SerializeField] EffectType effectType;

    // 자동 반환 시간
    [SerializeField] float returnTime;

    // 생성된 시점부터의 시간
    [SerializeField] float startTime;

    private void Update()
    {
        startTime += Time.deltaTime;

        if(startTime > returnTime)
        {
            EffectPool.instance.ReleaseEffect(gameObject, effectType);
        }
    }

    // 반환될 때 시간 초기화
    private void OnDisable()
    {
        startTime = 0;
    }
}
