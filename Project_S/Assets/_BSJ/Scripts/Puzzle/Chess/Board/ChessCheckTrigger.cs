using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessCheckTrigger : MonoBehaviour
{
    [SerializeField] private ChessPieceName chessPieceName;

    private ChessPuzzleClear puzzleClear;

    private void Awake()
    {
        puzzleClear = transform.parent.GetComponent<ChessPuzzleClear>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ChessPiece>() != null)
        {
            ChessPiece chesspiece = other.GetComponent<ChessPiece>();

            if(chesspiece.pieceName == chessPieceName) 
            {
                chesspiece.EnterChessTrigger();
                puzzleClear.IncreaseClearCheck((int)chessPieceName);
                chesspiece.transform.position = new Vector3(transform.position.x, chesspiece.transform.position.y, transform.position.z);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<ChessPiece>() != null)
        {
            ChessPiece chesspiece = other.GetComponent<ChessPiece>();

            if (chesspiece.pieceName == chessPieceName)
            {
                chesspiece.StayChessTrigger();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ChessPiece>() != null)
        {
            ChessPiece chesspiece = other.GetComponent<ChessPiece>();

            if (chesspiece.pieceName == chessPieceName)
            {
                chesspiece.ExitChessTrigger();
                puzzleClear.DecreaseClearCheck((int)chessPieceName);
            }
        }
    }
}
