using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : GrabbableEvents
{
    Grabbable grabbable;

    // Enable this when toggled on
    public Transform BladeTransform;
    public Transform RaycastTransform;
    public LayerMask LaserCollision;
    public ParticleSystem CollisionParticle;

    public bool axeEnabled = false;

    bool axeSwitchOn = false;

    public float LaserLength = 1f;
    public float axeActivateSpeed = 10f;

    public AudioSource CollisionAudio;
    public bool Colliding = false;

    // Start is called before the first frame update
    void Start()
    {
        grabbable = GetComponent<Grabbable>();

        //if (CollisionParticle != null)
        //{
        //    CollisionParticle.Stop();
        //}
    }

    void Update()
    {

        // Toggle Saber
        if (grabbable.BeingHeld)
        {
            axeSwitchOn = !axeSwitchOn;
        }

        // Sheath / Unsheath
        if (axeEnabled || axeSwitchOn)
        {
            //BladeTransform.localScale = Vector3.Lerp(BladeTransform.localScale, Vector3.one, Time.deltaTime * axeActivateSpeed);
        }
        else
        {
        //    BladeTransform.localScale = Vector3.Lerp(BladeTransform.localScale, new Vector3(1, 0, 1), Time.deltaTime * axeActivateSpeed);
        }

        //BladeTransform.gameObject.SetActive(BladeTransform.localScale.y >= 0.01);

        checkCollision();

        //// Raise pitch on collision
        //if (Colliding)
        //{
        //    CollisionAudio.pitch = 2f;
        //}
        //else
        //{
        //    CollisionAudio.pitch = 1f;
        //}
    }

    public override void OnTrigger(float triggerValue)
    {

        axeEnabled = triggerValue > 0.2f;

        base.OnTrigger(triggerValue);
    }

    void checkCollision()
    {

        Colliding = false;

        if (axeEnabled == false && !axeSwitchOn)
        {
            Debug.Log("!");
            
            //CollisionParticle.Pause();
            return;
        }

        //RaycastHit hit;
        //Physics.Raycast(RaycastTransform.position, RaycastTransform.up, out hit, LaserLength, LaserCollision, QueryTriggerInteraction.Ignore);

        //if (hit.collider != null)
        //{
        //    if (CollisionParticle != null)
        //    {

        //        float distance = Vector3.Distance(hit.point, RaycastTransform.transform.position);
        //        float percentage = distance / LaserLength;
        //        BladeTransform.localScale = new Vector3(BladeTransform.localScale.x, percentage, BladeTransform.localScale.z);

        //        // Allow collision particle to play
        //        CollisionParticle.transform.parent.position = hit.point;
        //        CollisionParticle.transform.parent.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        //        if (!CollisionParticle.isPlaying)
        //        {
        //            CollisionParticle.Play();
        //        }

        //        // Haptics
        //        input.VibrateController(0.2f, 0.1f, 0.1f, thisGrabber.HandSide);

        //        Colliding = true;
        //    }
        //}
        //else
        //{
        //    if (CollisionParticle != null)
        //    {
        //        CollisionParticle.Pause();
        //    }
        //}
    }

    void OnDrawGizmosSelected()
    {
        if (RaycastTransform != null)
        {
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(RaycastTransform.position, RaycastTransform.position + RaycastTransform.up * LaserLength);
        }
    }
}


