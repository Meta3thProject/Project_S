using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireButton : MonoBehaviour
{
    // 직렬화로 할당: 4번째 firebase
    public GameObject fireBase;

    private void OnCollisionEnter(Collision collision)
    {
        // 눌리면 땅에 박힘
        transform.position -= Vector3.up * 2;

        // 눌리면 화로 리프트 업
        fireBase.transform.position += Vector3.up * 2;
    }
    private void OnTriggerEnter(Collider other)
    {
    }
}
