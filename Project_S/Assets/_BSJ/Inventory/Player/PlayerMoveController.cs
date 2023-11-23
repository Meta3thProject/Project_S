using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;
    float jumpSpeed = 20f;
    float rotateSpped = 10f;
    float xinput;
    float yinput;
    float zinput;

    Vector3 playerVelocity;

    Rigidbody playerRigidbody;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        xinput = Input.GetAxis("Horizontal");
        zinput = Input.GetAxis("Vertical");

        float xSpeed = xinput * speed;
        float zSpeed = zinput * speed;

        playerVelocity = new Vector3(xSpeed, 0, zSpeed);
        playerRigidbody.velocity = playerVelocity;

        Jump();

        // 바라보는 방향으로 회전 후 다시 정면을 바라보는 현상을 막기 위해 설정
        if (!(xinput == 0 && zinput == 0))
        {
            // 이동과 회전을 함께 처리
            transform.position += playerVelocity * speed * Time.deltaTime;
            // 회전하는 부분. Point 1.
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(playerVelocity), Time.deltaTime * rotateSpped);
        }
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        }
    }
}
