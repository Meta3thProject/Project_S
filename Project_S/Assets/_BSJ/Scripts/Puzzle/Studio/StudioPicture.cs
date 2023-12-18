using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PictureType
{
    first, second, third, fourth,
    fifth, sixth, seventh, eighth
}

public class StudioPicture : MonoBehaviour
{
    // 그림 타입
    [field: SerializeField] public PictureType pictureType {  get; private set; }

    private Rigidbody rb;

    private Vector3 enterTriggerSize;
    private Vector3 exitTriggerSize;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // 사이즈는 프레임 사이즈에 맞추었음. (1.45f, 1.45f, 1)
        enterTriggerSize = new Vector3(1.45f, 1.45f, 1);
        exitTriggerSize = transform.localScale;
    }

    /// <summary>
    /// 사진관 프레임의 트리거에 닿았을 때 작동하는 메소드.
    /// </summary>
    public void EnterFrameTrigger()
    {
        ControlFasten(true);
        transform.localScale = enterTriggerSize;
        transform.rotation = Quaternion.identity;   // 원 위치로 변경 후 180도 회전
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + 180f, transform.rotation.z);
        rb.velocity = Vector3.zero;
    }

    /// <summary>
    /// 사진관 프레임의 트리거에서 나왔을 때 작동하는 메소드.
    /// </summary>
    public void ExitFrameTrigger() 
    {
        transform.localScale = exitTriggerSize;
        ControlFasten(false);
    }

    private void ControlFasten(bool _control)
    {
        if(_control)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            StartCoroutine(constraintsControl());
        }

        else
        {
            rb.useGravity = true;
        }
    }

    IEnumerator constraintsControl()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        rb.constraints = RigidbodyConstraints.None;
    }
}
