using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // TEST: 이동과 상호작용만 구현
    public Canvas cc;
    private Rigidbody rb;

    public float viewAngle = 90f; // 시야각 설정
    public float viewRadius = 2f; // 시야 반경 설정

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cc.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        MoveSelf();
        Interact();
    }

    public void MoveSelf()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // 수평 입력
        float verticalInput = Input.GetAxis("Vertical"); // 수직 입력

        Vector3 movement = transform.forward * verticalInput + transform.right * horizontalInput; // 이동 방향 벡터 생성

        rb.AddForce(movement * 5.0f); // Rigidbody에 힘을 가해서 이동시키기
    }

    public void Interact()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, 1 << LayerMask.NameToLayer("Water"));
            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                Transform target = targetsInViewRadius[i].transform;
                Debug.Log(target.name);

                Vector3 dirToTarget = (target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                {
                    float dstToTarget = Vector3.Distance(transform.position, target.position);

                    // 시야 내에 있는 오브젝트에 대한 추가적인 작업 수행
                    cc.gameObject.SetActive(true);
                    cc.GetComponent<RectTransform>().position = (dirToTarget * 2) + transform.position;
                    cc.GetComponent<RectTransform>().forward = dirToTarget;
                }
            }
        }
    }
}
