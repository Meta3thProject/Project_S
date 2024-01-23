using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isFirstTouch { get; private set; }

    private void Awake()
    {
        isFirstTouch = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isFirstTouch == false)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                isFirstTouch = true;                
            }
        }
        
    }
}
