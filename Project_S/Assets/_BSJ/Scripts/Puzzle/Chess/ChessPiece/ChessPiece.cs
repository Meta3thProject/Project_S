using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChessPieceName
{
    Knight, Rook, Pawn
}

public class ChessPiece : MonoBehaviour
{
    [field: SerializeField]
    public ChessPieceName pieceName { get; private set; }

    public ParticleSystem particle { get; private set; }

    private void Awake()
    {
        particle = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    public void EnterChessTrigger()
    {
        PlayParticle();
        transform.rotation = Quaternion.identity;
    }

    public void StayChessTrigger()
    {
        transform.rotation = Quaternion.identity;
    }

    public void ExitChessTrigger()
    {
        StopParticle();
    }

    private void PlayParticle()
    {
        particle.Play();
    }

    private void StopParticle()
    {
        particle.Stop();
    }
}
