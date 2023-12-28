using BNG;
using System.Collections;
using UnityEngine;

public class InventoryTeleport : MonoBehaviour
{
    // TODO : 
    // 인벤토리 위치가 확정되면 스크립트 내부에서 서치해서 가져올 것
    public GameObject invenPos = default;

    private ScreenFader screenFader = default;
    private InputBridge input = default;
    
    private Vector3 curPos = default;
    private CharacterController controller = default;

    private bool isEnterInven = default;
    private bool isInput = default;

    private const float reEnterTime = 1f;
    private WaitForSeconds waitTime = default;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        input = InputBridge.Instance;
        controller = GetComponent<CharacterController>();
        screenFader = this.gameObject.GetChildObj("CenterEyeAnchor").GetComponent<ScreenFader>();

        isEnterInven = false;
        isInput = false;

        waitTime = new WaitForSeconds(reEnterTime);
    }

    // Update is called once per frame
    void Update()
    {
        TeleportToInven();
    }

    private bool CheckInput()
    {
        if (input.YButton)
        {
            isInput = true;
            return true;
        }
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    isInput = true;
        //    return true;
        //}
        return false;
    }

    private void TeleportToInven()
    {
        if (isInput == false && CheckInput())
        {
            screenFader.DoFadeIn();
            if (isEnterInven == false)
            {
                curPos = controller.transform.position;
                isEnterInven = true;
                StartCoroutine(WaitNextEnter(invenPos.transform.position));  
            }
            else
            {
                isEnterInven = false;
                StartCoroutine(WaitNextEnter(curPos));
            }

        }
    }

    private void MovePosition(Vector3 pos)
    {
        controller.enabled = false;
        controller.transform.position = pos;
        controller.enabled = true;         
    }

    private IEnumerator WaitNextEnter(Vector3 pos)
    {
        yield return waitTime;
        MovePosition(pos);
        screenFader.DoFadeOut();
        isInput = false;

    }
}
