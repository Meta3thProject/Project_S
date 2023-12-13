using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpirithand : MonoBehaviour
{
    private bool isLeftSpiritHand = true;
    private bool isRightSpiritHand = true;

    public InputActionReference LeftHandSpritAction;
    public InputActionReference RightHandSpritAction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(KeyLeftSpirithand())
        {
            Instantiate(ResourceManager.objects["Projectile"]);                                      
        }
        

    }

    public virtual bool KeyLeftSpirithand()
    {
        if (isLeftSpiritHand && InputBridge.Instance.LeftTriggerDown)
        {
            return true;
        }

        if (LeftHandSpritAction != null)
        {
            return LeftHandSpritAction.action.ReadValue<float>() > 0.1f;
        }

        return false;
    }

    public virtual bool KeyRigthSpirithand()
    {
        //if(isLeftSpiritHand && InputBridge.Instance.LeftTriggerDown)
        //{
        //    return true;
        //}

        if (RightHandSpritAction != null)
        {
            return RightHandSpritAction.action.ReadValue<bool>();
        }

        return false;
    }
}
