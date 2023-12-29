using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upon3treeTrigger : MonoBehaviour
{
    // 탁자 위에 올라온 오브젝트를 담아두는 리스트
    [SerializeField] List<GameObject> ItemsOnTable = new List<GameObject>();

    // 퍼즐 클리어 스크립트
    Upon3treePuzzleClear upon3treePuzzleClear;

    private void Awake()
    {
        // 이 퍼즐의 인덱스 번호는 0번입니다.
        upon3treePuzzleClear = transform.root.GetChild(0).GetComponent<Upon3treePuzzleClear>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 나무가 트리거에 닿으면 리스트에 추가
        if (other.CompareTag("Wood"))
        {
            ItemsOnTable.Add(other.gameObject);

            // 나무가 3개 이상이면 클리어
            if (ItemsOnTable.Count >= 3)
            {
                upon3treePuzzleClear.PuzzleClear();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 나무가 트리거에 닿으면 리스트에 추가
        if (other.CompareTag("Wood"))
        {
            ItemsOnTable.Remove(other.gameObject);
        }
    }
}
