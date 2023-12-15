using BNG;
using UnityEngine;

public class QuikSlotController : MonoBehaviour
{
    InputBridge input = default;
    GameObject KeyItemSlot = default;
    SnapZone snapZone = default;
    bool isPress = false;
    
    void Awake()
    {
        Init();
    }

    void Init()
    {
        input = InputBridge.Instance;
        KeyItemSlot = this.gameObject.GetChildObj("KeyItemSlot");
        snapZone = KeyItemSlot.GetComponent<SnapZone>();
        isPress = false;
    }

    void Update()
    {
        if (InputBButtonDown())
        {   
            isPress = !isPress;
            if(snapZone.HeldItem != null)
            {
                snapZone.gameObject.SetActive(isPress);
                snapZone.enabled = isPress;
            }
        }
    }

    private bool InputBButtonDown()
    {
        if(input.BButtonDown)
        {
            return true;
        }
        return false;
    }
}
