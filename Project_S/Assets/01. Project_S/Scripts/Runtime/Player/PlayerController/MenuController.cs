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
        if (isMenuButtonDown())
        {
            DisableMove(!isPress);
        }
    }

    public void DisableMove(bool isOn_)
    {
        isPress = isOn_;
        controller.enabled = isOn_;
        locomotionManager.enabled = isOn_;
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
