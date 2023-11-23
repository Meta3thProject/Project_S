using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectEffect : MonoBehaviour
{
    public IObjectPool<GameObject> Pool { get; set; }
}
