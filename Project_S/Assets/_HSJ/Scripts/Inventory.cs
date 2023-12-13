using BNG;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    private InputBridge input = default;
    private GameObject KeyItemSlot = default;
    private SnapZone snapZone = default;
    private bool isPress = false;
    
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

    void Start()
    {
        
    }

    // Update is called once per frame
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

    public bool InputBButtonDown()
    {
        if(input.BButtonDown)
        {
            return true;
        }
        return false;
    }
}
