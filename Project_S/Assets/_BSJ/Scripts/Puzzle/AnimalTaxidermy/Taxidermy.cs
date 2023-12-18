using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum AnimalType
{
    Lion, Deer, WildBoar, Elephant
}

public class Taxidermy : MonoBehaviour
{
    // 그림 타입
    [field: SerializeField] public AnimalType TaxidermyType { get; private set; }

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 사진관 프레임의 트리거에 닿았을 때 작동하는 메소드.
    /// </summary>
    public void EnterFrameTrigger(Quaternion _Euler, Vector3 _position)
    {
        //! 트리거에 딱 달라붙게 하기 위한 값들
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        ControlGravity(false);
        transform.rotation = _Euler;
        transform.position = _position;

    }

    /// <summary>
    /// 사진관 프레임의 트리거에서 나왔을 때 작동하는 메소드.
    /// </summary>
    public void ExitFrameTrigger()
    {
        ControlGravity(true);
    }

    private void ControlGravity(bool _control)
    {
        if (_control)
        {
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;

            StartCoroutine(constraintsControl());
        }

        else
        {
            rb.useGravity = false;
            
        }
    }

    IEnumerator constraintsControl()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        rb.constraints = RigidbodyConstraints.None;
    }
}
