using BNG;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    InputBridge input = default;
    CharacterController controller = default;
    LocomotionManager locomotionManager = default;
    void Awake()
    {
        Init();
    }

    void Init()
    {
        // LEGACY : 
        // MenuButton 으로 옮겨갈 기능
        //input = InputBridge.Instance;
        controller = this.GetComponent<CharacterController>();
        locomotionManager = this.GetComponent<LocomotionManager>();
    }

    void Update()
    {
        // LEGACY : 
        // MenuButton 으로 옮겨갈 기능
        //if (isMenuButtonDown())
        //{
        //    DisableMove(!isPress);
        //}
    }

    public void DisableMove(bool isOn_)
    {
        controller.enabled = isOn_;
        locomotionManager.enabled = isOn_;
    }
}
