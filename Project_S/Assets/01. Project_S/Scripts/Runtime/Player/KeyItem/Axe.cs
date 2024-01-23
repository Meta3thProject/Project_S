using BNG;
using UnityEngine;

public class Axe : GrabbableEvents
{
    Grabbable grabbable;

    public float axeActivateSpeed = 10f;

    public bool IsBladeOn {  get; private set; } 

    private BoxCollider bladeCollider = default;

    private GameObject effect = default;
    private Transform effectPos = default;

    // Start is called before the first frame update
    void Start()
    {
        Init();    
    }

    void Init()
    {
        grabbable = GetComponent<Grabbable>();
        bladeCollider = this.GetComponent<BoxCollider>();
        IsBladeOn = false;

        // { Effect Init
        effectPos = this.gameObject.GetChildObj("EffectOffset").transform;        
        effect = Instantiate(ResourceManager.effects["axe on"], effectPos);
        effect.transform.localPosition = Vector3.zero;
        effect.SetActive(IsBladeOn);
        // } Effect Init
    }
    void Update()
    {
        if (!grabbable.BeingHeld)
        {
            IsBladeOn = false;
            bladeCollider.enabled = false;
        }
        ActiveBlade();
    }

    private void ActiveBlade()
    {
        effect.SetActive(IsBladeOn);
    }

    public void DoShake()
    {
        input.VibrateController(0.3f, 0.2f, 0.1f, thisGrabber.HandSide);
    }

    public override void OnTriggerDown()
    {
        IsBladeOn = !IsBladeOn;
        bladeCollider.enabled = IsBladeOn;
        if(IsBladeOn == true)
        {
            SoundManager.Instance.PlaySfxClip("SE_Item_axe_on", effectPos.position, 0.1f);
        }
        base.OnTriggerDown();
    }
    
}


