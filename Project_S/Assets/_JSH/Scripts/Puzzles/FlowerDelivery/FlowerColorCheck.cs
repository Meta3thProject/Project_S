using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerColorCheck : MonoBehaviour
{
    public GameObject delivered;

    public FlowerDelivery checker;

    private void OnTriggerEnter(Collider other)
    {
        // delivered가 비어있지 않으면 함수 종료 
        if (GFunc.IsValid(delivered) == true) { return; }

        if (checker.IsFlowerBusket(other.GetComponent<ItemData>().itemID) == true)
        {
            delivered = other.gameObject;

            delivered.transform.position = transform.position;
            delivered.transform.rotation = transform.rotation;

            delivered.GetComponent<Rigidbody>().velocity = Vector3.zero;
            delivered.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // delivered가 비어있으면 함수 종료 
        if (GFunc.IsValid(delivered) == false) { return; }

        if (checker.IsFlowerBusket(other.GetComponent<ItemData>().itemID) == true)
        {
            delivered = default;
        }
    }
}
