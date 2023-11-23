using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    public GameObject playerHand;       // 플레이어 손 게임 오브젝트

    [SerializeField]
    private GameObject handObject;      // 현재 잡고 있는 게임 오브젝트
    private Rigidbody handObjRigid;     // 플레이어 손의 들려있는 오브젝트의 Rigidbody
    private Rigidbody hitObjRigid;      // 충돌한 있는 게임 오브젝트의 Rigidbody

    [SerializeField]
    private bool isGrab = false;

    // 아이템 박스에서 아이템을 꺼내오기 위한 캐싱
    private ItemBox itemBox;

    private void Update()
    {
        if(isGrab && Input.GetKeyDown(KeyCode.X))
        {
            isGrab = false;
            handObjRigid = handObject.transform.GetChild(0).GetComponent<Rigidbody>();

            handObjRigid.useGravity = true;
            handObjRigid.constraints = RigidbodyConstraints.None;

            handObject.transform.parent = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // 아이템이면 플레이어 손의 자식오브젝트로 변경한다.
        if (other.transform.CompareTag("Item"))
        {
            if (isGrab == false && Input.GetKey(KeyCode.Z))
            {
                isGrab = true;
                handObject = other.gameObject.transform.parent.gameObject;
                hitObjRigid = other.GetComponent<Rigidbody>();

                handObject.transform.parent = playerHand.transform;
                handObject.transform.localPosition = Vector3.zero;
                hitObjRigid.useGravity = false;
                hitObjRigid.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            }
        }

        // 아이템 박스라면
        if (other.transform.CompareTag("ItemBox"))
        {
            if(isGrab == false && Input.GetKey(KeyCode.Z))
            {
                // 아이템 박스 컴포넌트를 불러옵니다.
                itemBox = other.GetComponent<ItemBox>();
                if (itemBox.itemCount <= 0) { Debug.Log("아이템의 갯수가 부족합니다."); return; }

                // 아이템을 생성한 뒤,
                GameObject newItem = itemBox.MakeNewItem(itemBox.itemName);

                // 그랩할 수 있게 설정합니다.
                isGrab = true;
                handObject = newItem.gameObject;
                hitObjRigid = newItem.GetComponent<Rigidbody>();

                handObject.transform.parent = playerHand.transform;
                handObject.transform.localPosition = Vector3.zero;
                hitObjRigid.useGravity = false;
                hitObjRigid.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            }
        }
    }
}
