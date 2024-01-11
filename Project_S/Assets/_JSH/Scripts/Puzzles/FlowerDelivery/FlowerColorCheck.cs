using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerColorCheck : MonoBehaviour
{
    public GameObject delivered;

    public FlowerDelivery checker;

    private void OnTriggerEnter(Collider other)
    {
        if (checker.IsFlowerBusket(other.GetComponent<ItemData>().itemID) == true)
        {
            delivered = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (checker.IsFlowerBusket(other.GetComponent<ItemData>().itemID) == true)
        {
            delivered = default;
        }
    }
}
