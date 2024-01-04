using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPosition : MonoBehaviour
{
    public Camera playerCamera; // 플레이어 카메라
    public Canvas menuCanvas; // 메뉴 캔버스
    public float distanceFromCamera = 2f; // 카메라로부터의 거리

    void LateUpdate()
    {
        if (playerCamera != null && menuCanvas != null)
        {
            // 카메라의 forward 방향과 거리를 고려하여 캔버스를 배치
            Vector3 newPosition = playerCamera.transform.position + playerCamera.transform.forward * distanceFromCamera;
            menuCanvas.transform.position = newPosition;
            menuCanvas.transform.rotation = Quaternion.Euler(playerCamera.transform.rotation.x, playerCamera.transform.rotation.y,0);

        }
    }
}