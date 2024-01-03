using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GSingleton<GameManager>
{
    private PlayerStat playerStat = default;
    // Start is called before the first frame update
    private void Awake()
    {
        ResourceManager.Init();
        playerStat = GFunc.GetRootObj(Define.PLAYER).GetComponent<PlayerStat>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
