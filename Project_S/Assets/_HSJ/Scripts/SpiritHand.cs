using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritHand : MonoBehaviour
{
    private InputBridge input = default;
    private Grabber grabber = default;
    private GameObject projectilePointer = default;
    private GameObject spiritProjectile = default;

    private VRUISystem uiSystem;

    // 타입을 나누기 위해 열어둠
    public HandControl handControl = default;
    
    
    private float chargeTime = default;
    private float maxChargeTime = 1f;

    private bool isHandEnabled = false;
    private bool isCharged = false;
    
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
        spiritProjectile = Instantiate(ResourceManager.objects["Projectile"], projectilePointer.transform);
        projectilePointer.SetActive(isHandEnabled);
    }


    // Update is called once per frame
    void Update()
    {
        if (grabber.HeldGrabbable != null )
        {
            return;
        } // if : 빈 손이 아니라면 return 

        isHandEnabled = CheckTriggerDown();

        ActiveSpiritHand();

    }

    private void ActiveSpiritHand()
    {
        if (isHandEnabled)
        {

            grabber.ForceRelease = true;
            grabber.HideHandGraphics();

            ActivePointer();
            ChargeTimer();
            // TODO : 정령의 손 충전 중 그랩 가능 한것 방지할 것 

        }
        else if (!isHandEnabled)
        {
            chargeTime = 0f;
            grabber.ForceRelease = false;
            grabber.ResetHandGraphics();

            ActivePointer();
            ShootProjectile();
        }
    }

    private void ActivePointer()
    {
        projectilePointer.SetActive(isHandEnabled);

    }
    // TEST : 임시 
    private void ChargeTimer()
    {
        chargeTime += Time.deltaTime;
        if (maxChargeTime <= chargeTime)
        {
            isCharged = true;
        }


    }
    private void ShootProjectile()
    {
        if (isCharged)
        {
            spiritProjectile.transform.SetParent(null);
            spiritProjectile.GetComponent<Rigidbody>().AddForce(projectilePointer.transform.forward * 5f, ForceMode.Impulse);
            isCharged = false;
            StartCoroutine(WaitForShoot());
        }
    }

    private IEnumerator WaitForShoot()
    {
        float timer = 0f;
        while(timer < 2f)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        SetProjectile();
    }

    private void SetProjectile()
    {
        spiritProjectile.transform.SetParent(projectilePointer.transform);
        spiritProjectile.transform.localPosition = Vector3.zero;
        spiritProjectile.GetComponent<Rigidbody>().velocity = Vector3.zero;

    }

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
        HandControl Trigger = handControl_;
  
        return Trigger;
    }       // GetHandController()
}
