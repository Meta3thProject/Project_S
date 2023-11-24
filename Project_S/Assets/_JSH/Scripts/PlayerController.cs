using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 직렬화로 할당
    public QuestManager questM;

    // TEST: 이동과 상호작용만 구현
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        MoveSelf();
    }

    public void MoveSelf()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // 수평 입력
        float verticalInput = Input.GetAxis("Vertical"); // 수직 입력

        Vector3 movement = transform.forward * verticalInput + transform.right * horizontalInput; // 이동 방향 벡터 생성

        rb.AddForce(movement * 5.0f); // Rigidbody에 힘을 가해서 이동시키기
    }
}
