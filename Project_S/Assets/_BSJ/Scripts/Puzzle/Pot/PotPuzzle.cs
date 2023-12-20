using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotType
{
    AnswerPot, WrongPot
}

public class PotPuzzle : MonoBehaviour
{
    [field: SerializeField] public PotType potType { get; private set; }

    private ParticleSystem particle;
    private Rigidbody rb;

    private void Awake()
    {
        if(transform.childCount >= 1)
        {
            particle = transform.GetChild(0).GetComponent<ParticleSystem>();
        }

        rb = GetComponent<Rigidbody>();
    }

    public void EnterTrigger(Vector3 _Position)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        transform.rotation = Quaternion.identity;
        transform.position = _Position;
        PlayParticle();
        StartCoroutine(constraintsControl());
    }

    private void PlayParticle()
    {
        particle.Play();
    }

    private IEnumerator constraintsControl()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        rb.constraints = RigidbodyConstraints.None;
    }

}
