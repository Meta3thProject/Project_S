using BNG;
using UnityEngine;

public class Axe : GrabbableEvents
{
    Grabbable grabbable;
    
    public float axeActivateSpeed = 10f;
    
    private bool isBladeOn = false;

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

        // { Effect Init
        effectPos = this.gameObject.GetChildObj("EffectOffset").transform;        
        effect = Instantiate(ResourceManager.effects["axe on"], effectPos);
        effect.transform.localPosition = Vector3.zero;
        effect.SetActive(isBladeOn);
        // } Effect Init
    }
    void Update()
    {
        if (!grabbable.BeingHeld)
        {
            isBladeOn = false;
            bladeCollider.enabled = false;
        }
        ActiveBlade();
    }

    private void ActiveBlade()
    {
        effect.SetActive(isBladeOn);
    }
    public override void OnTriggerDown()
    {
        isBladeOn = !isBladeOn;
        bladeCollider.enabled = isBladeOn;
        SoundManager.Instance.PlayAudioClip("SE_Item_axe_on", effectPos.position);
        base.OnTriggerDown();
    }
    
}


