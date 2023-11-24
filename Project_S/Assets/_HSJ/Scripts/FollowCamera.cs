using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem.EnhancedTouch;


// SJ_ 여긴 캬루가 접수한다.
public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    float x;
    float z;
    private float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        this.transform.position = this.transform.position + new Vector3(x,0,z);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            this.transform.position += Vector3.up;
        }
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            this.transform.position -= Vector3.up;
        }

    }
}
