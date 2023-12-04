using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GSingleton<GameManager>
{
    private PlayerStat playerStat = default;
    // Start is called before the first frame update
    void Start()
    {
        playerStat = GFunc.GetRootObj(Define.PLAYER).GetComponent<PlayerStat>();
        playerStat.AddPoint(Define.MBTI_T, 1f);
        playerStat.AddPoint(Define.MBTI_P, 1f);
        playerStat.AddPoint(Define.MBTI_T, 1f);

        ResourceManager.Init();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
