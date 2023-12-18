using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeEduPuzzleClear : MonoBehaviour
{
    [SerializeField] List<GameObject> ItemsOnTable = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Wood"))
        {
            ItemsOnTable.Add(other.gameObject);

            if(ItemsOnTable.Count >= 3)
            {
                StartCoroutine(StarManager.starManager.CallStar());
            }
        }
    }
}
