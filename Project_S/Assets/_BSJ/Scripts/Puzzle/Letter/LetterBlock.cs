using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LetterBlockType
{
    C, A, T, D, O
}

public class LetterBlock : MonoBehaviour
{
    [field: SerializeField]
    public LetterBlockType type { get; private set; }

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 해당 트리거에에 딱 맞게 들어가게 하는 메서드.
    /// </summary>
    public void EnterLetterTrigger()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        transform.rotation = Quaternion.identity;
        StartCoroutine(constraintsControl());
    }

    /// <summary>
    /// 0.5초 뒤에 constraints를 끄는 코루틴.
    /// </summary>
    /// <returns></returns>
    private IEnumerator constraintsControl()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        rb.constraints = RigidbodyConstraints.None;
    }
}
