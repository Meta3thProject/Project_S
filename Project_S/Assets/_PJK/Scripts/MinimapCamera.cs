using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    private Camera minimapcamera = default;
    
    void Start()
    {
        minimapcamera = GetComponent<Camera>();
        minimapcamera.cullingMask = LayerMask.GetMask("2F","3F");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
