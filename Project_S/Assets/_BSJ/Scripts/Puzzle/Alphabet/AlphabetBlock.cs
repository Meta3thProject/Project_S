using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AlphabetBlockType
{
    C, A, T, D, O
}

public class AlphabetBlock : MonoBehaviour
{
    [field: SerializeField]
    public AlphabetBlockType type { get; private set; }

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void EnterAlphabetTrigger()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        transform.rotation = Quaternion.identity;
        StartCoroutine(constraintsControl());
    }

    private IEnumerator constraintsControl()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        rb.constraints = RigidbodyConstraints.None;
    }
}
