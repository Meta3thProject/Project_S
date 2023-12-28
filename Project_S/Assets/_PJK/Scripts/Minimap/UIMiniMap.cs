using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMiniMap : MonoBehaviour
{
    [SerializeField]
    private Camera minimapCamera;
    [SerializeField]
    private TextMeshProUGUI textmapName;

    private void Awake()
    {
        //textmapName = SceneManager.GetActivateScene().name;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
