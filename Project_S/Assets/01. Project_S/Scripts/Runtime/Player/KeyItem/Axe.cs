using BNG;
using UnityEngine;

public class Axe : GrabbableEvents
{
    Grabbable grabbable;
        
    public float axeActivateSpeed = 10f;

    public AudioSource CollisionAudio;
    
    private bool isBladeOn = false;

    private BoxCollider bladeCollider = default;

    private GameObject effect = default;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        grabbable = GetComponent<Grabbable>();
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

    }
    public override void OnTriggerDown()
    {
        isBladeOn = !isBladeOn;
        bladeCollider.enabled = isBladeOn;
        base.OnTriggerDown();
    }

    private void OnTriggerEnter(Collider other)
    {        
        //if(other.transform.CompareTag("Default")) 
        //{
        //    input.VibrateController(0.2f, 0.8f, 0.1f, thisGrabber.HandSide);
        //    if(effect == null && effect == default)
        //    {
        //        effect = Instantiate(ResourceManager.effects["SwordBlock"], other.ClosestPointOnBounds(this.transform.position),Quaternion.identity);
        //    }
        //    else
        //    {
        //        effect.transform.position = other.ClosestPointOnBounds(this.transform.position);
        //        Vector3 hitNormal = this.transform.position - other.transform.position;
        //        effect.transform.up = hitNormal.normalized;
        //    }
        //    if(effect.activeInHierarchy)
        //    {
        //        effect.SetActive(false);
        //    }
        //    effect.SetActive(true);
        //}
    }
}


