using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    bool isend = false;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn") && isend == false)
        {
            Debug.Log("테스트트리거성공");
            EndingAfterTalk.endingAfterTalk.StartEndingtalk();
            isend = true;
        }
    }
}
