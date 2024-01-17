using BNG;
using UnityEngine;

public class QuikSlotController : MonoBehaviour
{
    private InputBridge input = default;
    private GameObject KeyItemSlot = default;
    private SnapZone snapZone = default;
    private GameObject torch = default;
    
    private bool isPress = false;
    
    void Awake()
    {
        Init();
        if (snapZone.HeldItem == null ||
            PuzzleManager.instance.puzzles[14] ||
            !snapZone.HeldItem.GetComponent<Torch>())
        {
            torch.SetActive(true);
            torch.GetComponent<ReturnToSnapZone>().enabled = true;
            snapZone.HeldItem = torch.GetComponent<Grabbable>();
        }       // if : 횃불 제작 후 재접속시 생성
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
