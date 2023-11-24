using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MiniMapScript : MonoBehaviour
{
    public Camera miniMapCamera; // 미니맵을 위한 카메라
    public Transform player; // 플레이어 위치
    public Transform[] questLocations; // 퀘스트 위치 배열
    public Transform[] enemyLocations; // 적 위치 배열

    public GameObject playerIcon; // 플레이어 아이콘
    public GameObject questIcon; // 퀘스트 아이콘
    public GameObject enemyIcon; // 적 아이콘

    void Update()
    {
        // 플레이어 아이콘 위치 업데이트
        Vector3 playerPosition = miniMapCamera.WorldToViewportPoint(player.position);
        playerIcon.transform.position = new Vector3(playerPosition.x, playerPosition.y, 0);

        // 퀘스트 아이콘 위치 업데이트
        foreach (Transform quest in questLocations)
        {
            Vector3 questPos = miniMapCamera.WorldToViewportPoint(quest.position);
            questIcon.transform.position = new Vector3(questPos.x, questPos.y, 0);
        }

        // 적 아이콘 위치 업데이트
        foreach (Transform enemy in enemyLocations)
        {
            Vector3 enemyPos = miniMapCamera.WorldToViewportPoint(enemy.position);
            enemyIcon.transform.position = new Vector3(enemyPos.x, enemyPos.y, 0);
        }
    }
}