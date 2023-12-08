using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
//using Vector3;


public class pcc : MonoBehaviour
{
    public float MoveSpeed = 3.2f;
    Vector3 Movement;
    public float jumpPower = 3f;
    //private bool isGrounded;
    public float fallMultiplier = 3.2f;
    public float lowJumpMultiplier = 1.2f;
    private Rigidbody rigid;


    void Start()
    {

        rigid = GetComponent<Rigidbody>();
        //isGrounded = false;

    }



    void Update()
    {
        Jump();
        Move();


    }


    void Move()
    {
        float X = Input.GetAxis("Horizontal");
        float Z = Input.GetAxis("Vertical");
        float Y = Input.GetAxisRaw("Mouse X");
        transform.Translate((new Vector3(X, 0, Z) * MoveSpeed) * Time.deltaTime);
        transform.Rotate(new Vector3(0f, Y, 0f));


    }

    void Rotate()
    {

    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        /*{
            if (!isGrounded)
            {
                isGrounded = true;
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }
            else
            {
                return;
            }

        }*/
    }
}
    


    /*void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;

        }
    }
}*/


