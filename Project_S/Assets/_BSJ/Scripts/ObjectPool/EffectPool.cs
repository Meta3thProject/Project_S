using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이펙트 타입 구별
public enum EffectType
{
    SortEffect01, SortEffect02, TreeHitEffect,
}

[Serializable]
public class EffectPoolInfo
{
    // 인스펙터 창에서 보여줄 정보들
    public EffectType effectType;         // 오브젝트 타입
    public int effectAmount;              // 처음 생성될 오브젝트의 갯수
    public GameObject effectPrefab;       // 이펙트 프리팹
    public GameObject effectcontainer;    // 이펙트 담을 컨테이너
    public Stack<GameObject> EffectPoolStack = new Stack<GameObject>();
}

public class EffectPool : MonoBehaviour
{
    public static EffectPool instance;

    [SerializeField]
    List<EffectPoolInfo> effectPoolInfo;

    private void Awake()
    {
        instance = this; 
    }

    private void Start()
    {
        // 시작 시 이펙트 풀에 이펙트 생성
        for(int i = 0; i < effectPoolInfo.Count; i++)
        {
            CreateEffect(effectPoolInfo[i]);
        }
    }

    /// <summary>
    /// 풀에서 이펙트를 가져오는 함수.
    /// </summary>
    /// <param name="effectType"></param>
    public GameObject GetEffect(EffectType _effectType)
    {
        // 반환할 이펙트 오브젝트
        GameObject effect = default;

        // 이펙트 타입 체크해서 해당 이펙트 풀 가져오기
        EffectPoolInfo effectPoolInfo = GetPoolByType(_effectType);

        Stack<GameObject> effectPoolStack = effectPoolInfo.EffectPoolStack;

        // 이펙트 풀 스택이 0보다 크다면 이펙트 반환
        if(effectPoolStack.Count > 0)
        {
            effect = effectPoolStack.Pop();
            return effect;
        }

        // 차있는 스택이 없다면 생성
        else
        {
            effect = Instantiate(effectPoolInfo.effectPrefab, effectPoolInfo.effectcontainer.transform);
        }

        return effect;
    }

    /// <summary>
    /// 풀에 이펙트를 반환하는 함수.
    /// </summary>
    /// <param name="_effect"></param>
    /// <param name="_effectType"></param>
    public void ReleaseEffect(GameObject _effect, EffectType _effectType)
    {
        // 어떤 이펙트 타입의 풀인지 체크 후 초기화 한 뒤 반환
        EffectPoolInfo effectPoolInfo = GetPoolByType(_effectType);
        _effect.SetActive(false);
        _effect.transform.position = effectPoolInfo.effectcontainer.transform.position;

        // 스택 반환
        Stack<GameObject> effectStack = effectPoolInfo.EffectPoolStack;
        if (effectStack.Contains(_effect) == false)
        {
            effectStack.Push(_effect);
        }
    }

    /// <summary>
    /// 풀에 이펙트를 생성하는 함수.
    /// </summary>
    /// <param name="poolInfo"></param>
    private void CreateEffect(EffectPoolInfo _poolInfo)
    {
        for(int i = 0; i < _poolInfo.effectAmount; i++)
        {
            // 이펙트 생성 후 스택에 푸쉬
            GameObject createdEffect = Instantiate(_poolInfo.effectPrefab, _poolInfo.effectcontainer.transform);
            _poolInfo.EffectPoolStack.Push(createdEffect);

            // 생성한 오브젝트 비활성화, 위치 초기화
            createdEffect.transform.position = _poolInfo.effectcontainer.transform.position;
            createdEffect.SetActive(false);
        }
    }

    /// <summary>
    /// 호출하는 이펙트 종류를 검출하여 해당 풀 반환하는 함수.
    /// </summary>
    /// <param name="effecttype"></param>
    /// <returns></returns>
    private EffectPoolInfo GetPoolByType(EffectType _effecttype)
    {
        for(int i = 0; i < effectPoolInfo.Count; i++)
        {
            if(_effecttype == effectPoolInfo[i].effectType)
            {
                return effectPoolInfo[i];
            }
        }

        return null;
    }
}
