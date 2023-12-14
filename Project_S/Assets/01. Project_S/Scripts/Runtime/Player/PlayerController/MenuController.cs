using BNG;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    InputBridge input = default;
    CharacterController controller = default;
    PlayerTeleport teleport = default;
    bool isPress = default;
    void Awake()
    {
        Init();
    }

    void Init()
    {
        input = InputBridge.Instance;
        controller = this.GetComponent<CharacterController>();
        teleport = this.GetComponent<PlayerTeleport>();
        isPress = false;
    }

    void Update()
    {
     if(isMenuButtonDown())
        {
            isPress = !isPress;
            controller.enabled = !isPress;
            teleport.enabled = false;
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
