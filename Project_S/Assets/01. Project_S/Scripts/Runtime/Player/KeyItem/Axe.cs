using BNG;
using UnityEngine;

public class Axe : GrabbableEvents
{
    Grabbable grabbable;
        
    public float axeActivateSpeed = 10f;

    public AudioSource CollisionAudio;
    public bool Colliding = false;

    private bool isBladeOn = false;
    private BoxCollider bladeCollider = default;

    private GameObject obj = default;

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
        if(other.transform.CompareTag("Damageable")) 
        {
            input.VibrateController(0.2f, 0.5f, 0.1f, thisGrabber.HandSide);
            Debug.LogFormat("Effect");
            if(obj == null && obj == default)
            {
                obj = Instantiate(ResourceManager.effects["SwordBlock"], other.ClosestPointOnBounds(this.transform.position),Quaternion.identity);
            }
            else
            {
                obj.transform.position = other.ClosestPointOnBounds(this.transform.position);
                Vector3 hitNormal = this.transform.position - other.transform.position;
                obj.transform.up = hitNormal.normalized;
            }
            if(obj.activeInHierarchy)
            {
                obj.SetActive(false);
            }
            obj.SetActive(true);
        }
    }
}


