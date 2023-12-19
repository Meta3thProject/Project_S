using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShieldType
{
    Painting01, Painting02, Painting03, Painting04, Painting05, Painting06, Painting07
}

public class ShieldPuzzle : MonoBehaviour
{
    [field: SerializeField] public ShieldType shieldType;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 방패 문양의 트리거에 닿았을 때 작동하는 메소드.
    /// </summary>
    public void EnterShieldShapeTrigger(Quaternion _Eulur, Vector3 _targetPosition)
    {
        //! 트리거에 딱 달라붙게 하기 위한 값들
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        ControlGravity(true);
        transform.rotation = _Eulur;
        transform.position = _targetPosition;
    }

    /// <summary>
    /// 방패 문양의 트리거에서 나왔을 때 작동하는 메소드.
    /// </summary>
    public void ExitShieldShapeTrigger()
    {
        ControlGravity(false);
    }

    private void ControlGravity(bool _control)
    {
        if (_control)
        {
            rb.useGravity = false;
            
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
