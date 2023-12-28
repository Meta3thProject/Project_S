using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCameratoUI : MonoBehaviour
{
    public static EndingCameratoUI endingCameratoUI = default;
    private Camera playerCamera = default;
    private void Awake()
    {
        endingCameratoUI = this;
    }
    void Start()
    {
        playerCamera = GetComponent<Camera>();
        
    }


    public void gotoending()
    {
        playerCamera.clearFlags = CameraClearFlags.SolidColor;
        playerCamera.backgroundColor = Color.white;
        playerCamera.cullingMask = 1 << LayerMask.NameToLayer("LastCamera");

        EndingAfterTalk.endingAfterTalk.endingafter();
    }
}
