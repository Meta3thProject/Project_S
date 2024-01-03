using System.Collections;
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
        EndingAfterTalk.endingAfterTalk.MBTIColor(out Color EndingColor);
        playerCamera.backgroundColor = EndingColor;
        StartCoroutine(changecolor());
        playerCamera.cullingMask = 1 << LayerMask.NameToLayer("LastCamera");

        EndingAfterTalk.endingAfterTalk.endingafter();
    }


    IEnumerator changecolor()
    {
        Color currentColor = playerCamera.backgroundColor;
        Color targetColor = Color.white;

        for (int i = 0; i < 50; i++)
        {
            currentColor = Color.Lerp(currentColor, targetColor, 0.1f); // 1/100씩 변경하도록 보간

            playerCamera.backgroundColor = currentColor;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
