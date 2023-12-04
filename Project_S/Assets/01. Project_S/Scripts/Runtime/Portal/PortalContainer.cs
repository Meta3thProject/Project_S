using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalContainer : MonoBehaviour
{
    private GameObject[] portals = default;

    private Vector3[] portalPos = default;

    [SerializeField] 
    private int arraySize;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        // { Portal 생성함       

        // TODO : 추후 더 나은 방법으로 수정 할 것
        if (!portals.IsValid())
        {
            arraySize = 4;
        }       // if : arraySize가 비어있을 경우 초기값인 4로 고정 

        //portals = new GameObject[arraySize];
 
        //for (int i = 0; i < portals.Length; i++)
        //{
        //    //portals[i] = ResourceManager.Instance.
        //        GetResource(RDefine.PORTAL_OBJ, Vector3.zero, Quaternion.identity);
        //    portals[i].GetComponent<Portal>().GetEnum(i);
        //    portals[i].GetComponent<Portal>().enabled = false;
        //    portals[i].transform.SetParent(this.gameObject.transform);
        //}       // loop : 포탈 게임 오브젝트를 생성하고, 포탈 타입을 설정해준다. 
        //// } Portal 생성함
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
