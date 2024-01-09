using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPuzzle : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void EnterCatTrigger()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        StartCoroutine(ConstraintsControl());
    }

    private IEnumerator ConstraintsControl()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        rb.constraints = RigidbodyConstraints.None;
    }
}
