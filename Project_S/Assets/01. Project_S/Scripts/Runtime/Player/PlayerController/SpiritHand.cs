using BNG;
using System.Collections;
using UnityEngine;

public class SpiritHand : MonoBehaviour
{
    public HandControl handControl = default;

    private InputBridge input = default;
    private VRUISystem uiSystem = default;

    private Grabber grabber = default;
    private GameObject projectilePointer = default;
    private GameObject spiritProjectile = default;
    private GameObject chargeEffect = default;

    private LineRenderer lineRenderer = default;
    private Vector3 defaultPosition = new Vector3(0f,0f,10f);
    
    private float chargeTime = default;
    private float maxChargeTime = 1f;
        
    private bool isHandEnabled = false;
    private bool isCharged = false;
    private bool isShooting = false;

    // Start is called before the first frame update
 
    void Start()
    {
        Init();
    }
    void Init()
    {
        input = InputBridge.Instance;
        uiSystem = VRUISystem.Instance;

        grabber = GetComponentInChildren<Grabber>();        
        projectilePointer = this.gameObject.GetChildObj("ProjectilePointer");
        lineRenderer = projectilePointer.GetComponent<LineRenderer>();

        spiritProjectile = Instantiate(ResourceManager.objects["fairy's hand_ projectile Variant"], projectilePointer.transform);
        chargeEffect = Instantiate(ResourceManager.objects["fairy's hand_ charging Variant"], projectilePointer.transform);

        lineRenderer.enabled = isHandEnabled;
        spiritProjectile.SetActive(isHandEnabled);
        chargeEffect.SetActive(isHandEnabled);
    }

    void Update()
    {
        if (uiSystem.EventData.pointerCurrentRaycast.gameObject != null &&
            uiSystem.EventData.pointerCurrentRaycast.gameObject.layer == LayerMask.NameToLayer("UI"))
        {
            return;
        }       // if : UI를 인식하면 정령 손을 사용하지 못하도록 return;

        if (grabber.HeldGrabbable != null)
        {            
            return;
        } // if : 빈 손이 아니라면 return 

        isHandEnabled = CheckTriggerDown();

        if (!isShooting)
        {
            ActiveSpiritHand();
        }

    }
    private void ActiveSpiritHand()
    {
        
        if (isHandEnabled)
        {
            grabber.ForceRelease = true;
            grabber.HideHandGraphics();
            
            ActivePointer();            
            ChargeTimer();
            
        }
        else if (!isHandEnabled)
        {
            chargeTime = 0f;
            grabber.ForceRelease = false;
            grabber.ResetHandGraphics();

            ActivePointer();
            ShootProjectile();
        }
    }       // ActiveSpiritHand()

    private Vector3 GetRayDistance()
    {                
        Vector3 tempPos = defaultPosition;
        RaycastHit hit = new RaycastHit();

        if(Physics.Raycast(projectilePointer.transform.position,projectilePointer.transform.forward,
            out hit,10f, LayerMask.GetMask("Default")))
        {            
            tempPos = new Vector3(0f,0f,hit.distance);     
        }
        
        return tempPos;
    }       // GetRayDistance()
    private void ActivePointer()
    {
        if(isHandEnabled)
        {
            lineRenderer.SetPosition(1,GetRayDistance());
        }
        lineRenderer.enabled = isHandEnabled;
        chargeEffect.SetActive(isHandEnabled);

    }       // ActivePointer()

    private void ChargeTimer()
    {
        chargeTime += Time.deltaTime;
        if (maxChargeTime <= chargeTime)
        {
            isCharged = true;
        }
    }       // ChargeTimer()
    private void ShootProjectile()
    {
        if (isCharged)
        {
            SoundManager.Instance.PlaySfxClip("SE_Player_fairyhand_shoot", this.transform.position);

            spiritProjectile.SetActive(true);
            spiritProjectile.transform.SetParent(null);
            spiritProjectile.GetComponent<Rigidbody>().AddForce(projectilePointer.transform.forward * 5f, ForceMode.Impulse);
            isShooting = true;
            isCharged = false;
            StartCoroutine(WaitForShoot());
        }
    }       // ShootProjectile()

    private IEnumerator WaitForShoot()
    {
        float timer = 0f;
        while(timer < 2f)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        SetProjectile();
        isShooting = false;
    }       // WaitForShoot()

    // ! Projectile Reset 
    private void SetProjectile()
    {
        spiritProjectile.SetActive(false);
        spiritProjectile.transform.SetParent(projectilePointer.transform);
        spiritProjectile.transform.localPosition = Vector3.zero;
        spiritProjectile.transform.localRotation = Quaternion.identity;  
        spiritProjectile.GetComponent<Rigidbody>().velocity = Vector3.zero;

    }       // SetProjectile()

    // ! HandControl 
    private bool CheckTriggerDown()
    {
       

        if (GetHandController(handControl) == HandControl.LeftTrigger)
        {
            return input.LeftTrigger > 0.2f;
        }
        else if (GetHandController(handControl) == HandControl.RightTrigger)
        {
            return input.RightTrigger > 0.2f;
        }

        return false;
    }       // CheckTriggerDown()

    private HandControl GetHandController(HandControl handControl_)
    {
        HandControl trigger = handControl_;
  
        return trigger;
    }       // GetHandController()
}
