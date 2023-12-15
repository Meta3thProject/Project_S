using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlowerName
{
    OrangeFlower, PinkFlower
}

public class FlowerPuzzle : MonoBehaviour
{
    [field: SerializeField]
    public FlowerName flowerName { get; private set; }

    private ParticleSystem particle;
    private Rigidbody rb;

    private void Awake()
    {
        particle = transform.GetChild(0).GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody>();
    }

    public void EnterGroundTrigger()
    {
        PlayFlowerParticle();
        rb.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    public void ExitGroundTrigger()
    {
        StopFlowerParticle();
    }

    private void PlayFlowerParticle()
    {
        particle.Play();
    }

    private void StopFlowerParticle()
    {
        particle.Stop();
    }
}

