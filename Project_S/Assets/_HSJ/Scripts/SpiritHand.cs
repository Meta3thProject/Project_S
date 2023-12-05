using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritHand : MonoBehaviour
{
    private InputBridge input = default;
    private Grabber grabber = default;
    private Vector3 linePointer = default;

    // 일단 타입을 나누기 위해 열어둠
    public HandControl handControl = default;
    
    
    private float chargeTime = default;
    private float maxChargeTime = 2f;

    private bool isTriggerDown = false;
    private bool isCharged = false;
    // Start is called before the first frame update
 
    void Start()
    {
        Init();

    }
    void Init()
    {
        grabber = GetComponentInChildren<Grabber>();
        input = InputBridge.Instance;
    }


    // Update is called once per frame
    void Update()
    {
        if(grabber.HeldGrabbable != null)
        {
            return;
        } // if : 빈 손이 아니라면 return 

        
            ActiveSpiritHand();
        

    }
    
    private void ActiveSpiritHand()
    {
        if (CheckTriggerDown())
        {

            grabber.ForceRelease = true;
            grabber.HideHandGraphics();

            // TODO : 정령의 손 충전 중 그랩 가능 한것 방지할 것 
            

        }
        else if (!CheckTriggerDown())
        {

            grabber.ForceRelease = false;

            grabber.ResetHandGraphics();


            //projectile.GetComponent<Rigidbody>().AddForce(this.transform.forward * 5f, ForceMode.Impulse);
            //chargeTime = 0f;
        }
    }

    public bool CheckTriggerDown()
    {

        if(GetHandController(handControl) == HandControl.LeftTrigger)
        {
            if (input.LeftTriggerDown)
            {
                return true;
            }
        }
        else if(GetHandController(handControl) == HandControl.RightTrigger)
        {
            if(input.RightTriggerDown)
            {
                return true;
            }
        }          
                     
        return false;
    }       // CheckTriggerDown()

    private HandControl GetHandController(HandControl handControl_)
    {
        HandControl Trigger = handControl_;
  
        return Trigger;
    }       // GetHandController()
}
