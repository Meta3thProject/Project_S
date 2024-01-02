using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessCheckTrigger : MonoBehaviour
{
    [SerializeField] private ChessPieceName chessPieceName;

    private ChessPuzzleClear puzzleClear;
    private ChessPiece chesspiece;

    private void Awake()
    {
        // 이 퍼즐의 인덱스는 3번입니다.
        puzzleClear = transform.root.GetChild(3).GetComponent<ChessPuzzleClear>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ChessPiece>() != null)
        {
            chesspiece = other.GetComponent<ChessPiece>();

            if(chesspiece.pieceName == chessPieceName) 
            {
                chesspiece.transform.position = new Vector3(transform.position.x, chesspiece.transform.position.y - 0.1f, transform.position.z);    // y값은 제일 어울리는 값을 찾아서 넣어준 것임.
                chesspiece.EnterChessTrigger();
                puzzleClear.IncreaseClearCheck((int)chessPieceName);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ChessPiece>() != null)
        {
            chesspiece = other.GetComponent<ChessPiece>();

            if(chesspiece.pieceName == chessPieceName)
            {
                chesspiece.ExitChessTrigger();
                puzzleClear.DecreaseClearCheck((int)chessPieceName);
            }
        }
    }
}
