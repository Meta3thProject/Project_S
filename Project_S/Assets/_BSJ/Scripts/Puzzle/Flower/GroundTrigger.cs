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
        // 이 퍼즐의 인덱스는 2번입니다.
        puzzleClear = transform.root.GetChild(2).GetComponent<FlowerPuzzleClear>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FlowerPuzzle>() != null)
        {
            flower = other.GetComponent<FlowerPuzzle>();

            if (flower.flowerName == flowerName)
            {
                flower.transform.position = new Vector3(transform.position.x, flower.transform.position.y - 0.3f, transform.position.z);    // y값은 제일 어울리는 값을 찾아서 넣어준 것임.
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
