using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class SpiritHand : MonoBehaviour
{
    private InputBridge input = default;
    private Grabber grabber = default;
    private GameObject projectilePointer = default;
    private GameObject spiritProjectile = default;

    private LineRenderer lineRenderer = default;
    private Vector3 defaultPosition = new Vector3(0f,0f,10f);

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

        grabber = GetComponentInChildren<Grabber>();
        
        projectilePointer = this.gameObject.GetChildObj("ProjectilePointer");
        lineRenderer = projectilePointer.GetComponent<LineRenderer>();

        spiritProjectile = Instantiate(ResourceManager.objects["Projectile"], projectilePointer.transform);
        projectilePointer.SetActive(isHandEnabled);
    }


    // Update is called once per frame
    //void Update()
    //{
    //    if (grabber.HeldGrabbable != null )
    //    {
    //        return;
    //    } // if : 빈 손이 아니라면 return 

    //    isHandEnabled = CheckTriggerDown();

    //    ActiveSpiritHand();
              
    //}

    // Test : 가장 늦은 업데이트 실행으로 잡고 있는 오브젝트가 없을 떄만 실행할 수 있도록 테스트 
    void LateUpdate()
    {
        if (grabber.HeldGrabbable != null)
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
        projectilePointer.SetActive(isHandEnabled);

    }       // ActivePointer()
    // TEST : 임시 
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
            spiritProjectile.transform.SetParent(null);
            spiritProjectile.GetComponent<Rigidbody>().AddForce(projectilePointer.transform.forward * 5f, ForceMode.Impulse);
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
    }       // WaitForShoot()

    private void SetProjectile()
    {
        spiritProjectile.transform.SetParent(projectilePointer.transform);
        spiritProjectile.transform.localPosition = Vector3.zero;
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
        HandControl Trigger = handControl_;
  
        return Trigger;
    }       // GetHandController()
}
