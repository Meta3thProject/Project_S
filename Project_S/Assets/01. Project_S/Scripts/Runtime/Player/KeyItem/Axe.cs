using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : GrabbableEvents
{
    Grabbable grabbable;


    private bool isBladeOn = false;
    private BoxCollider bladeCollider = default;
    private GameObject bladeFX = default;
    

    public float LaserLength = 1f;
    public float axeActivateSpeed = 10f;

    public AudioSource CollisionAudio;
    public bool Colliding = false;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        grabbable = GetComponent<Grabbable>();
        bladeFX = this.gameObject.GetChildObj("BladeFX");
        bladeCollider = this.GetComponent<BoxCollider>();

    }
    void Update()
    {
        if (!grabbable.BeingHeld)
        {
            isBladeOn = false;
            bladeCollider.enabled = false;
        }

        if (isBladeOn)
        {
            ActiveBlade();
        }
        else
        {
            ActiveBlade();
        }
    }

    private void ActiveBlade()
    {
        bladeFX.SetActive(isBladeOn);
    }
    public override void OnTriggerDown()
    {
        isBladeOn = !isBladeOn;
        bladeCollider.enabled = isBladeOn;
        base.OnTriggerDown();
    }

    //void checkCollision()
    //{

    //    Colliding = false;

    //    if (axeEnabled == false && !axeSwitchOn)
    //    {

    //        //CollisionParticle.Pause();
    //        return;
    //    }

    //}
}


