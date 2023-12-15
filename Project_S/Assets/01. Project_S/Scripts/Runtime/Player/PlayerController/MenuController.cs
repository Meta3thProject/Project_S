using BNG;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    InputBridge input = default;
    CharacterController controller = default;
    LocomotionManager locomotionManager = default;
    bool isPress = default;
    void Awake()
    {
        Init();
    }

    void Init()
    {
        input = InputBridge.Instance;
        controller = this.GetComponent<CharacterController>();
        locomotionManager = this.GetComponent<LocomotionManager>();
        isPress = false;
    }

    void Update()
    {
     if(isMenuButtonDown())
        {
            isPress = !isPress;
            controller.enabled = !isPress;
            locomotionManager.enabled = !isPress;
        }
    }

    bool isMenuButtonDown()
    {
        if (input.BackButtonDown)
        {
            return true;
        }
        return false;
    }
}
