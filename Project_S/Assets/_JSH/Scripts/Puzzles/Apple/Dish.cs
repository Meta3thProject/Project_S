using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    public AppleCheck checker;
    public List<GameObject> onDish;

    public int myIdx;

    public bool isAppleOn;

    private void Awake()
    {
        onDish = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 리스트에 추가
        onDish.Add(other.gameObject);

        if (other.name == "Apple")
        {
            isAppleOn = true;
            checker.CheckMatchNumber();
            checker.CheckInOrder(myIdx);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 리스트에서 제거
        onDish.Remove(other.gameObject);

        if (other.name == "Apple")
        {
            isAppleOn = false;
            // 순서먼저 체크
            checker.CheckInOrder(myIdx);
            checker.CheckMatchNumber();
        }
    }
}
