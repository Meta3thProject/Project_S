using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
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
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        transform.rotation = Quaternion.identity;
        PlayParticle();
        StartCoroutine(ConstraintsControl());
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

    private IEnumerator ConstraintsControl()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        rb.constraints = RigidbodyConstraints.None;
    }
}
