using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritHand : MonoBehaviour
{
    private HandController controller = default;
    private InputBridge input = default;
    private GameObject projectile = default;
    private readonly string projectileName = "Projectile";
    
    public GameObject bullet = default;

    private float maxScale = default;
    private float minScale = default;
    private float curScale = default;

    private bool isEnabled = false;
    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }
    void Init()
    {
        controller = GetComponent<HandController>();
        projectile = this.gameObject.GetChildObj(projectileName);
        input = InputBridge.Instance;
        maxScale = 0.5f;
        minScale = 0.1f;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.PreviousHeldObject != null)
        {
            return;
        }
        if (input.LeftTriggerDown && controller.PreviousHeldObject == null)
        {
            if (isEnabled == false)
            {
                controller.PreviousHeldObject = projectile;
                StartCoroutine(ChargeSpiritHand());
             
            }
        }
    }
    IEnumerator ChargeSpiritHand()
    {
        curScale = 0f;
        while (curScale < maxScale)
        {
            isEnabled = true;
            projectile.transform.SetParent(transform);
            projectile.transform.localPosition = Vector3.zero;
            projectile.SetActive(isEnabled);
            curScale += Time.deltaTime;
            projectile.transform.localScale = new Vector3(curScale, curScale, curScale);
            yield return null;
        }
        projectile.transform.localScale = new Vector3(minScale, minScale, minScale);
        projectile.GetComponent<Rigidbody>().AddForce(this.transform.forward * 5f, ForceMode.Impulse);
        projectile.transform.SetParent(null);
        isEnabled = false;
    }
}
