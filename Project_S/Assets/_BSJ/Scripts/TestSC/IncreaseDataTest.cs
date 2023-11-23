using ObserverPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDataTest : MonoBehaviour
{
    [SerializeField]
    ObserverTestUI data;

    public void Shoot()
    {
        data.UpdateData(3, 5);
    }
}
