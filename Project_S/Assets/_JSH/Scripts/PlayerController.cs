using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // TEST: 이동과 상호작용만 구현
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        DetectNPC();
    }

    private void FixedUpdate()
    {
        // MoveSelf();
    }

    public void MoveSelf()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // 수평 입력
        float verticalInput = Input.GetAxis("Vertical"); // 수직 입력

        Vector3 movement = transform.forward * verticalInput + transform.right * horizontalInput; // 이동 방향 벡터 생성

        rb.AddForce(movement * 5.0f); // Rigidbody에 힘을 가해서 이동시키기
    }

    [HideInInspector]
    public float viewAngle = 90f; // 시야각 설정
    [HideInInspector]
    public float viewRadius = 2f; // 시야 반경 설정

    public void DetectNPC()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, 1 << LayerMask.NameToLayer("Water"));

        if (targetsInViewRadius.Length <= 0)
        {
            NPCManager.Instance.PopDown();
            return;
        }

        Transform target = default;

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform targetTemp = targetsInViewRadius[i].transform;

            target = targetTemp;

            Vector3 dirToTemp = (targetTemp.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToTemp) < viewAngle / 2)
            {

                if (target == default)
                {
                    target = targetTemp;
                }
                else
                {
                    float dstToTemp = Vector3.Distance(transform.position, targetTemp.position);
                    float dstToTarget = Vector3.Distance(transform.position, target.position);

                    if (dstToTarget > dstToTemp)
                    {
                        target = targetTemp;
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            dirToTarget.y = 0;

            NPCManager.Instance.PopUp(dirToTarget);
            target.GetComponent<INPCBehaviour>().PopUpDialog();
        }
        else { return; }
    }
}
