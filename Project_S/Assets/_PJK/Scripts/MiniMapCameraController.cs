using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraController : MonoBehaviour
{
    public GameObject Player;
    private GameObject Camera;
    private Vector3 place;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        place.x = Player.transform.position.x;
        place.z = Player.transform.position.z;
    }
}
