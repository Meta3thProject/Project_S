using BNG;
using UnityEngine;

public class QuikSlotController : MonoBehaviour
{
    private InputBridge input = default;
    private GameObject KeyItemSlot = default;
    private SnapZone snapZone = default;
    private GameObject torch = default;
    
    private bool isPress = false;
    private void Start()
    {
        if (PuzzleManager.instance.puzzles[14])
        {
            GFunc.CreateObj<Torch>("Torch").GetComponent<ReturnToSnapZone>().enabled = true;
        }       // if : 횃불 제작 후 재접속시 생성

        Init();
 
    }
    void Init()
    {
        input = InputBridge.Instance;
        KeyItemSlot = this.gameObject.GetChildObj("KeyItemSlot");
        torch = this.gameObject.GetChildObj("Torch");
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
