using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class InventoryTeleport : MonoBehaviour
{
    InputBridge input = default;

    public GameObject invenPos = default;
    
    private Vector3 curPos = default;
    private CharacterController controller = default;

    private bool isEnterInven = default;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        input = InputBridge.Instance;
        controller = GetComponent<CharacterController>();
        isEnterInven = false;
    }

    // Update is called once per frame
    void Update()
    {
        TeleportToInven();
    }

    private bool CheckInput()
    {
        if(input.YButton)
        {
            return true;
        }

        return false;
    }

    private void TeleportToInven()
    {
        if (CheckInput())
        {
            if(!isEnterInven)
            {
                curPos = controller.transform.position;
                MovePosition(invenPos.transform.position);
                isEnterInven = true;
            }
            else
            {
                MovePosition(curPos);
                isEnterInven = false;
            }

        }
    }

    private void MovePosition(Vector3 pos)
    {
        controller.enabled = false;
        controller.transform.position = pos;
        controller.enabled = true;
    }
}
