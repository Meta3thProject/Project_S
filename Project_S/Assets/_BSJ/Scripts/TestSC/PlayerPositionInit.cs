using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionInit : MonoBehaviour
{
    private void Awake()
    {
        FirebaseManager.instance.PlayerPosUpdateFromDB(this.transform);
    }
}
