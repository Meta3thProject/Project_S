using BNG;
using EPOOutline;
using UnityEngine;


// 스크립트 이름 변경해야함 NPC Dialog 관련
public class PlayerController : MonoBehaviour
{

    private InputBridge input = default;

    private Transform leftHand = default;
    private Transform rightHand = default;
    
    private RaycastHit leftHit = default;
    private RaycastHit rightHit = default;

    private Collider prevRayHitLeft = default;
    private Collider prevRayHitRight = default;

    private Collider curRayHitLeft = default;
    private Collider curRayHitRight = default;

    private readonly float rayDistance = 5f;
    private void Awake()
    {        
        Init();
    }

    private void Update()
    {
        InteractNpc();
    }

    private void Init()
    {
        input = InputBridge.Instance;
        leftHand = gameObject.GetChildObj("LeftController").transform;
        rightHand = gameObject.GetChildObj("RightController").transform;

        leftHit = new RaycastHit();
        rightHit = new RaycastHit();
    }       // Init()

    // TODO : Refactoring needed
    private void InteractNpc()
    {                

        if (Physics.Raycast(leftHand.transform.position, leftHand.transform.forward, out leftHit, rayDistance, LayerMask.GetMask("NPC")))
        {
            GetHitNpc(leftHit.collider, ref prevRayHitLeft, ref curRayHitLeft);
            if (input.XButtonDown)
            {            
                GetNpcDialog(curRayHitLeft);

            }
        }       // if : 좌측 컨트롤러 Ray처리
        else
        {
            if(prevRayHitLeft != null)
            {
                ActiveOutLine(prevRayHitLeft, false);
            }
        }

        if (Physics.Raycast(rightHand.transform.position, rightHand.transform.forward, out rightHit, rayDistance, LayerMask.GetMask("NPC")))
        {
            GetHitNpc(rightHit.collider,ref prevRayHitRight, ref curRayHitRight);
            if (input.AButtonDown)
            {
                GetNpcDialog(curRayHitRight);
            }
        }       // if : 우측 컨트롤러 Ray처리
        else
        {
            if (prevRayHitRight != null)
            {
                ActiveOutLine(prevRayHitRight, false);
            }
        }       
    }       // CheckNpc()       

    private void GetNpcDialog(Collider curHitCollider_)
    {
        NPCManager.Instance.interacted = NPCManager.Instance.npcs[NPCManager.Instance.npcs.IndexOf(curHitCollider_.GetComponent<NPCBase>())];

        Vector3 dirToTarget = (curRayHitRight.transform.position - transform.position).normalized;
        dirToTarget.y = 0;

        NPCManager.Instance.PopUp(dirToTarget);
        curHitCollider_.GetComponent<INPCBehaviour>().PopUpDialog();
    }       // GetNpcDialog()
        
    private void GetHitNpc(Collider hitCollider_,ref Collider prevRayhit_, ref Collider curRayhit_)
    {
         curRayhit_ = hitCollider_;
        ActiveOutLine(curRayhit_, true);

        if(curRayhit_ != prevRayhit_ && prevRayhit_ != null)
        {
            ActiveOutLine(prevRayhit_, false);
        }
        prevRayhit_ = hitCollider_;

    }       // GetHitNpc()

    private void ActiveOutLine(Collider hitCollider_, bool isOn)
    {
        if(hitCollider_.TryGetComponent<Outlinable>(out Outlinable outlinable))
        {
            outlinable.enabled = isOn;
        }
    }       // ActiveOutLine()

}
