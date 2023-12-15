using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CombineButtonClick : MonoBehaviour
{
    [SerializeField] private bool isCombine;

    public InventoryTable inventoryTable;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            isCombine = true;

            inventoryTable.CombineItems();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            isCombine = false;
        }
    }
}
