using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialBasket : MonoBehaviour
{
    // 정령의 손에 닿았을 때 튀어나갈 방향
    private Vector3 bounceDirection01;
    private Vector3 bounceDirection02;
    private Vector3 bounceDirection03;

    // 튀어나갈 힘
    private float bouncePower = 10;

    private Rigidbody rigidBody;
    private Grabbable grabbable;

    private int randomNumber;

    private bool isInteratable;

    private void Awake()
    {
        bounceDirection01 = transform.parent.GetChild(4).GetChild(0).transform.position - this.transform.position;
        bounceDirection02 = transform.parent.GetChild(4).GetChild(1).transform.position - this.transform.position;
        bounceDirection03 = transform.parent.GetChild(4).GetChild(2).transform.position - this.transform.position;

        rigidBody = GetComponent<Rigidbody>();
        grabbable = GetComponent<Grabbable>();
        EnableGrabbable();

        randomNumber = Random.Range(0, 3);

        isInteratable = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isInteratable == false) { return; }

        if (collision.collider.CompareTag("SpiritHand"))
        {
            if (randomNumber == 0)
            {
                isInteratable = false;
                EnableGrabbable(_isEnable: true);
                rigidBody.AddForce(bounceDirection01 * bouncePower, ForceMode.Impulse);
            }

            else if(randomNumber == 1) 
            {
                isInteratable = false;
                EnableGrabbable(_isEnable: true);
                rigidBody.AddForce(bounceDirection02 * bouncePower, ForceMode.Impulse);
            }

            else
            {
                isInteratable = false;
                EnableGrabbable(_isEnable: true);
                rigidBody.AddForce(bounceDirection03 * bouncePower, ForceMode.Impulse);
            }
        }
    }

    /// <summary>
    /// grabbable 컴포넌트를 비활성/활성 하는 메서드.
    /// </summary>
    /// <param name="_isEnable">활성화시킬지 안시킬지 체크하는 bool</param>
    public void EnableGrabbable(bool _isEnable = false)
    {
        grabbable.enabled = _isEnable;
    }

}
