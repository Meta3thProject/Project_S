using BNG;
using UnityEngine;

public class Torch : GrabbableEvents
{
    private bool isFlameOn = false;
    private GameObject flameFX = default;
    private SphereCollider flameCollider = default;
    private GameObject handle = default;
    // Start is called before the first frame 
    void Start()
    {
        Init();
    }

    void Init()
    {
        flameFX = this.gameObject.GetChildObj("TorchFire");
        flameCollider = this.GetComponent<SphereCollider>();
        handle = this.gameObject.GetChildObj("Handle");
        flameCollider.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {        
        if(this.transform.parent != null)
        {
            flameCollider.enabled = false;
        }
        // { 토치 횃불 이펙트 끄고 키기
        if (isFlameOn)
        {                       
            ActiveFlame();
        }
        else
        {
            ActiveFlame();
        }
        // } 토치 횃불 이펙트 끄고 키기
    }    
    
    private void ActiveFlame()
    {
        flameFX.SetActive(isFlameOn);
    }

    public override void OnTriggerDown()
    {
        isFlameOn = !isFlameOn;
        flameCollider.enabled = isFlameOn;
        base.OnTriggerDown();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!flameCollider.enabled) { return; }
        if (other.gameObject.layer == LayerMask.NameToLayer("Default"))
        {              
            input.VibrateController(0.2f, 0.1f, 0.1f, thisGrabber.HandSide);
        }
    }
}
