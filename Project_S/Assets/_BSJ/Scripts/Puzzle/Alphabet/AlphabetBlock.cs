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
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
    }
}
