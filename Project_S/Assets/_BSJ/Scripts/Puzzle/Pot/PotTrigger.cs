using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotTrigger : MonoBehaviour
{
    PotPuzzleClear potPuzzleClear;
    PotPuzzle potPuzzle;

    private void Awake()
    {
        potPuzzleClear = transform.parent.GetComponent<PotPuzzleClear>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PotPuzzle>() != null)
        {
            potPuzzle = other.GetComponent<PotPuzzle>();

            if(potPuzzle.potType == PotType.AnswerPot)
            {
                potPuzzle.EnterTrigger(transform.position);
                potPuzzleClear.CheckClear();
            }
        }
    }
}
