using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphabetTrigger : MonoBehaviour
{
    [SerializeField] private AlphabetBlockType alphabetType;

    private AlphabetPuzzleClear alphabetClear;
    private AlphabetBlock alphabetBlock;

    private void Awake()
    {
        alphabetClear = transform.parent.GetComponent<AlphabetPuzzleClear>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AlphabetBlock>() != null)
        {
            alphabetBlock = other.GetComponent<AlphabetBlock>();
            alphabetBlock.transform.position = new Vector3(transform.position.x, alphabetBlock.transform.position.y, transform.position.z);

            if (alphabetBlock.type == alphabetType)
            {
                alphabetBlock.EnterAlphabetTrigger();
                alphabetClear.IncreaseClearCheck((int)alphabetType);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<AlphabetBlock>() != null)
        {
            alphabetBlock = other.GetComponent<AlphabetBlock>();

            if (alphabetBlock.type == alphabetType)
            {
                alphabetClear.DecreaseClearCheck((int)alphabetType);
            }
        }
    }
}
