using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrigger : MonoBehaviour
{
    [SerializeField] private FlowerName flowerName;

    private FlowerPuzzleClear puzzleClear;
    private FlowerPuzzle flower;

    private void Awake()
    {
        puzzleClear = transform.parent.GetComponent<FlowerPuzzleClear>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FlowerPuzzle>() != null)
        {
            flower = other.GetComponent<FlowerPuzzle>();

            if (flower.flowerName == flowerName)
            {
                flower.transform.position = new Vector3(transform.position.x, flower.transform.position.y, transform.position.z);
                flower.EnterGroundTrigger();
                puzzleClear.IncreaseClearCheck((int)flowerName);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ChessPiece>() != null)
        {
            flower = other.GetComponent<FlowerPuzzle>();

            if (flower.flowerName == flowerName)
            {
                flower.ExitGroundTrigger();
                puzzleClear.DecreaseClearCheck((int)flowerName);
            }
        }
    }
}
