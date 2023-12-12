using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public enum AlphabetBlockType
{
    C, A, T, D, O
}

public class AlphabetBlock : MonoBehaviour
{
    [field: SerializeField]
    public AlphabetBlockType type { get; private set; }

    public void EnterAlphabetTrigger()
    {
        transform.rotation = Quaternion.identity;
    }
}
