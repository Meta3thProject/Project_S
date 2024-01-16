using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GSingleton<GameManager>
{
    private PlayerStat playerStat = default;
    private SoundManager soundManager = default;
    // Start is called before the first frame update
    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        ResourceManager.Init();
        soundManager = SoundManager.Instance;

        playerStat = GFunc.GetRootObj(Define.PLAYER).GetComponent<PlayerStat>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
}
