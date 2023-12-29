using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IceCubeType
{
    Chicken, Meat, Pork
}

public class IceCube : MonoBehaviour
{
    [field: SerializeField] public IceCubeType iceCubeType { get; private set; }

    [SerializeField] private List<MeshRenderer> _meshRenderers;

    private ButcherShop01Clear butcherShop01Clear;
    private ParticleSystem _particleSystem;
    private Rigidbody _rigidBody;

    private void Awake()
    {
        // 이 퍼즐의 인덱스 번호는 12번입니다.
        butcherShop01Clear = transform.root.GetChild(12).GetComponent<ButcherShop01Clear>();

        // 이펙트 캐싱
        _particleSystem = transform.GetChild(0).GetComponent<ParticleSystem>();

        // 리지드바드 캐싱
        _rigidBody = GetComponent<Rigidbody>();
        Set_Constraints();

        // 메쉬 렌더러 캐싱
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if(transform.GetChild(i).GetComponent<MeshRenderer>())
            {
                _meshRenderers.Add(transform.GetChild(i).GetComponent<MeshRenderer>());
            }
        }
    }

    private void OnEnable()
    {
        // 메쉬렌더러 보이게 하기.
        foreach(MeshRenderer renderer in _meshRenderers)
        {
            renderer.enabled = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 정령의 손으로 터치했을 때,
        if(collision.collider.CompareTag("SpiritHand"))
        {
            // 고기의 총 갯수 --
            butcherShop01Clear.iceCubeCount--;

            // 메쉬렌더러 안보이게 하기.
            foreach (MeshRenderer renderer in _meshRenderers)
            {
                renderer.enabled = false;
            }

            _particleSystem.Play();
            StartCoroutine(InActiveIceCube(0.5f));
        }
    }

    /// <summary>
    /// 몇 초 뒤에 게임오브젝트를 비활성화하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator InActiveIceCube(float _waitTime)
    {
        yield return new WaitForSecondsRealtime(_waitTime);
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Constraints 비활성화
    /// </summary>
    public void Clear_Constraints()
    {
        _rigidBody.constraints = RigidbodyConstraints.None;
    }

    /// <summary>
    /// Constraints FreezeAll
    /// </summary>
    public void Set_Constraints()
    {
        _rigidBody.constraints = RigidbodyConstraints.FreezeAll;
    }
}
