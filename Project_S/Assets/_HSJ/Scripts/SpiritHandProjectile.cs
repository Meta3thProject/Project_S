using BNG;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using Unity.VisualScripting;
using UnityEngine;


public class SpiritHandProjectile : GrabbableEvents
{
    private Vector3 tempScale = default;
    private Rigidbody rb = default;
    private Transform parent = default;
    // Start is called before the first frame update
    void Start()
    {
        parent = this.transform.parent;        
        rb = this.GetComponent<Rigidbody>();
        Init();
    }

    private void OnEnable()
    {
        this.transform.SetParent(null);
        Invoke("DisableSelf",3f);
    }

    private void OnDisable()
    {
        this.transform.localScale = tempScale;
        this.transform.parent = parent;
        rb.velocity = Vector3.zero;
        this.transform.localPosition = Vector3.zero;

    }
    void Init()
    { 
        tempScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    void DisableSelf()
    {
        this.gameObject.SetActive(false);
    }
}
