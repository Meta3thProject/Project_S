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

    public ParticleSystem particle { get; private set; }

    private void Awake()
    {
        particle = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    public void EnterGroundTrigger()
    {
        PlayFlowerParticle();
        transform.rotation = Quaternion.identity;
    }

    public void StayGroundTrigger()
    {
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

