using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrigger : MonoBehaviour
{
    [SerializeField] private FlowerName flowerName;

    private FlowerPuzzleClear puzzleClear;

    private void Awake()
    {
        puzzleClear = transform.parent.GetComponent<FlowerPuzzleClear>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FlowerPuzzle>() != null)
        {
            FlowerPuzzle flower = other.GetComponent<FlowerPuzzle>();

            if (flower.flowerName == flowerName)
            {
                flower.EnterGroundTrigger();
                puzzleClear.IncreaseClearCheck((int)flowerName);
                flower.transform.position = new Vector3(transform.position.x, flower.transform.position.y, transform.position.z);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<ChessPiece>() != null)
        {
            FlowerPuzzle flower = other.GetComponent<FlowerPuzzle>();

            if (flower.flowerName == flowerName)
            {
                flower.StayGroundTrigger();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ChessPiece>() != null)
        {
            FlowerPuzzle flower = other.GetComponent<FlowerPuzzle>();

            if (flower.flowerName == flowerName)
            {
                flower.ExitGroundTrigger();
                puzzleClear.DecreaseClearCheck((int)flowerName);
            }
        }
    }
}
