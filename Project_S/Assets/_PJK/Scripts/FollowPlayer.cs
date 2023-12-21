using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform Player = default;
    void Start()
    {
        Player = GetComponentInParent<Transform>();
    }


    void Update()
    {
        Vector3 newPosition = new Vector3(Player.position.x, Player.position.y);
        transform.position = newPosition;
    }
}
