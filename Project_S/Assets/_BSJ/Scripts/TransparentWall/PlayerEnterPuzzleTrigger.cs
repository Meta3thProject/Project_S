using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterPuzzleTrigger : MonoBehaviour
{
    [field: SerializeField] public GameObject puzzleWall { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<CharacterController>() != null)
        {
            puzzleWall.SetActive(true);
        }
    }

    public void RemoveWall()
    {
        puzzleWall.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void ActiveWall()
    {
        puzzleWall.SetActive(true);
    }
}
