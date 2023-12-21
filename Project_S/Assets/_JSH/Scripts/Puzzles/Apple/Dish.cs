using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    public List<GameObject> onDish;

    private void Awake()
    {
        onDish = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 리스트에 추가
        onDish.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        // 리스트에서 제거
        onDish.Remove(other.gameObject);
    }
}
