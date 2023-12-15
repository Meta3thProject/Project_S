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

    private ParticleSystem particle;
    private Rigidbody rb;

    private void Awake()
    {
        particle = transform.GetChild(0).GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody>();
    }

    public void EnterChessTrigger()
    {
        PlayParticle();
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
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
        particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }
}
