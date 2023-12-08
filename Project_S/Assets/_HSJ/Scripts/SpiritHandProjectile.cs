using BNG;
using UnityEngine;


public class SpiritHandProjectile : GrabbableEvents
{
    private Vector3 tempScale = default;
    private Rigidbody rb = default;
    private Transform parent = default;
    // Start is called before the first frame update
    void Start()
    {
        parent = this.transform.parent;        
        rb = this.GetComponent<Rigidbody>();
        Init();
    }

   
    void Init()
    { 
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public override void OnTriggerDown()
    {
        //isBladeOn = !isBladeOn;
        //bladeCollider.enabled = isBladeOn;
        base.OnTriggerDown();
    }

    public override void OnTrigger(float triggerValue)
    {

        base.OnTrigger(triggerValue);
    }
}
