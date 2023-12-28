using System.Collections;
using UnityEngine;

public class LastDoorOpen : MonoBehaviour
{
    [SerializeField]
    private GameObject rightdoor;
    [SerializeField]
    private GameObject leftdoor;
    private float rotationSpeed = 15.0f;
    private float currentAngle = 0.0f;
    void Start()
    {
        leftdoor = gameObject.transform.GetChild(0).gameObject;
        rightdoor = gameObject.transform.GetChild(1).gameObject;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") /*&& StarManager.starManager.getStarCount > 22*/)
        {
            Debug.Log("문이랑 플레이어 부딪침");
            StartCoroutine(OpenLastDoor());
        }
    }

    IEnumerator OpenLastDoor()
    {
        float targetAngle = 90.0f;
        while (currentAngle < targetAngle)
        {
            float rotationAmount = rotationSpeed * Time.deltaTime;
            currentAngle += rotationAmount;
            if (currentAngle > targetAngle)
                rotationAmount -= currentAngle - targetAngle;
            leftdoor.transform.Rotate(0, -rotationAmount, 0);
            rightdoor.transform.Rotate(0, rotationAmount, 0);
            yield return null;

        }

    }
}
