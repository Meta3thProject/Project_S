using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltTest : MonoBehaviour
{
    void Update()
    {
        if (transform.localScale.x > 3.9f)
        {
            transform.localScale *= 0.999f;
        }
    }
}
