using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StarUp : MonoBehaviour
{
    private SphereCollider SC = default;
    private SpriteRenderer StarColor = default;
    private bool isClear = false;
    void Start()
    {
        SC = GetComponent<SphereCollider>();
        StarColor = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player") && isClear == false)
    //    {
    //        Debug.Log("트리거접촉함");
    //        StartCoroutine(StarManager.starManager.CallStar());
    //        StarColor.sprite = ResourceManager.sprites["Star"];
    //        isClear = true;
    //    }
    //}
}
