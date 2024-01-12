using BNG;
using UnityEngine;

public class Torch : GrabbableEvents
{
    Grabbable grabbable;

    private bool isFlameOn = false;
    private GameObject flameFX = default;
    private SphereCollider flameCollider = default;
    private ReturnToSnapZone returnSnapZone = default;
    private SnapZone snapZone = default;
    private bool isFirst = default;

    // Start is called before the first frame 
    void Start()
    {
        Init();
    }

    void Init()
    {
        grabbable = GetComponent<Grabbable>();
        returnSnapZone = GetComponent<ReturnToSnapZone>();
        flameCollider = this.GetComponent<SphereCollider>();
        snapZone = GFunc.GetRootObj("Player").GetChildObj("KeyItemSlot").GetComponent<SnapZone>();

        flameFX = this.gameObject.GetChildObj("TorchFire");
        flameCollider.enabled = isFlameOn;
        flameFX.SetActive(isFlameOn);
        returnSnapZone.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {       
        
    
        if(!grabbable.BeingHeld)
        {
            flameCollider.enabled = false;
            isFlameOn = false;
            return;
        }    
        else
        {
            if (grabbable.BeingHeld && isFirst == default)
            {
                isFirst = true;
                returnSnapZone.enabled = true;
                returnSnapZone.ReturnTo = snapZone;
            }

            // { 토치 횃불 이펙트 끄고 키기
            if (isFlameOn)
            {
                ActiveFlame(isFlameOn);
            }
            else
            {
                ActiveFlame(isFlameOn);
            }
            // } 토치 횃불 이펙트 끄고 키기
        }


    }

    private void ActiveFlame(bool isOn)
    {
        flameFX.SetActive(isOn);
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
