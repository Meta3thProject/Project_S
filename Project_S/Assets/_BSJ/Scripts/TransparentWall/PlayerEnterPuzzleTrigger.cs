using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterPuzzleTrigger : MonoBehaviour
{
    [field: SerializeField] public GameObject puzzleWall { get; private set; }

    // 퍼즐이 이미 클리어 되었는지 체크. 이미 클리어 되었다면 투명벽이 생기지 않습니다.
    public bool isPuzzleClear = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isPuzzleClear) { return; }

        if (other.GetComponent<CharacterController>() != null)
        {
            puzzleWall.SetActive(true);
        }
    }

    public void RemoveWall()
    {
        isPuzzleClear = true;
        puzzleWall.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void ActiveWall()
    {
        if(isPuzzleClear) { return; }

        puzzleWall.SetActive(true);
    }
}
