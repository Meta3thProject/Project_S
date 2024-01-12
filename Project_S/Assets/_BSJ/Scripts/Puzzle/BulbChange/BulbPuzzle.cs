using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulbType
{
    bulb, brokenBulb
}

public class BulbPuzzle : MonoBehaviour
{
    // 현재 전구 타입
    [field: SerializeField] public BulbType bulbType { get; private set; }

    // 현재 전구 트리거에 닿았는지 판단
    private bool isTriggerEnter;

    // 현재 정령의 손에 맞았는지 판단
    public bool isSpiritHandHit { get; private set; }

    private Vector3 bounceDirection;        // 정령의 손에 맞았을 때 랜덤 방향으로 튀어나가게 하기위한 방향
    private float bouncePower;              // 랜덤 방향으로 튀어나가는 힘.

    Rigidbody rigidBody;

    private void Awake()
    {
        isTriggerEnter = false;
        isSpiritHandHit = false;

        rigidBody = GetComponent<Rigidbody>();

        bouncePower = Random.Range(1f, 3f);
        SetRandomDirection();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("SpiritHand"))
        {
            isSpiritHandHit = true;
            BounceBulb();
            StartCoroutine(InitSpiritHandHit());
        }
    }

    /// <summary>
    /// 전구 트리거에 닿았음을 체크하는 메서드.
    /// </summary>
    public void EnterBulbTrigger(bool isEnterTrigger_ = true)
    {
        isTriggerEnter = true;
    }

    /// <summary>
    /// 랜덤한 방향벡터를 설정하는 메서드.
    /// </summary>
    private void SetRandomDirection()
    {
        int randomXPosition = Random.Range(1, 5);
        int randomYPosition = Random.Range(1, 5);
        int randomZPosition = Random.Range(1, 5);

        bounceDirection = (new Vector3(randomXPosition, randomYPosition, randomZPosition) - transform.position).normalized;
    }

    /// <summary>
    /// 랜덤한 방향으로 튀어나가는 메서드.
    /// </summary>
    private void BounceBulb()
    {
        if(isTriggerEnter) 
        {
            rigidBody.useGravity = true;
            rigidBody.AddForce(bounceDirection * bouncePower, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// 정령의 손에 닿았음을 초기화하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator InitSpiritHandHit(float time_ = 3f)
    {
        yield return new WaitForSecondsRealtime(time_);

        isSpiritHandHit = false;
    }
}
