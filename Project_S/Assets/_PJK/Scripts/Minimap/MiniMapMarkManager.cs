using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MiniMapMarkManager : MonoBehaviour
{
    
    public List<GameObject> questLocation = default;
    public GameObject questMark = default;
    private GameObject questMarker = default;
    public bool isWorldmap = false;
    public bool isZonemap = true;



    void Start()
    {

        questMarker = Instantiate(questMark, new Vector3(0, 0, 0), Quaternion.identity, transform);
        questMarker.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        questMarker.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
