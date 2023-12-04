using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private LayerMask targetLayer = default;
    private enum PortalType
    {
        NONE = -1, LOBBY, STAGE1, STAGE2, STAGE3
    }
    [SerializeField]
    private PortalType type = default;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        targetLayer = LayerMask.NameToLayer(Define.LAYER_PLAYER);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetEnum(int _num)
    {
        this.type = (PortalType)_num;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == targetLayer)
        {
            if(type == PortalType.NONE)
            {
                Debug.LogWarning("포탈 타입이 지정되지 않았습니다.");
                return;
            }       // if : portalType이 지정되지 않았을 경우 로그를 띄우고 씬 로드를 막음

            SceneLoadManager.Instance.LoadScene(type.EnumToInt());            
        }
    }
}
