using System.Collections;
using UnityEngine;

public class SpiriHandProjectile : MonoBehaviour
{
    private GameObject effect = default;
    private const float time = 1f;
    private WaitForSeconds waitTime = new WaitForSeconds(time);     
    private void Awake()
    {
        effect = Instantiate(ResourceManager.objects["fairy's hand_ attack Variant"],this.transform);
        effect.SetActive(false);        
    }
    // ISSUE : 표적을 빗나가게 맞췄을 떄 탄환이 지속시간 동안 떠있다는 문제가 있음
    IEnumerator OnEffect()
    {
        effect.SetActive(true);
        yield return waitTime;
        effect.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        // TEST : test를 위한 default 
        // TODO : 어떤 Layer 에 대응할 것인지 할당할 것
        if (collision.gameObject.layer == LayerMask.NameToLayer("Default"))
        {   
            effect.transform.position = collision.contacts[0].point;
            effect.transform.up = collision.contacts[0].normal;
            StartCoroutine(OnEffect()); 
        }
    }

}
