using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject player;
    public float MouseX = 0;
    public float MouseY = 0;
    // public float distance = 3;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Cam();
        /*
            Vector3 FixedPos =
            new Vector3(
                target.transform.position.x = offsetX,
                target.transform.position.y = offsetY,
                target.transform.position.z = offsetZ);
        transform.position = Vector3.Lerp(transform.position);
        */

        MouseX += Input.GetAxis("Mouse X");
        MouseY -= Input.GetAxis("Mouse Y");
        transform.rotation = Quaternion.Euler(MouseY, MouseX, 0);
        // Vector3 reverseDistance = new Vector3(0.0f, 0.0f, distance);

       //  transform.position = player.transform.position - transform.rotation * reverseDistance;
                
    }


}