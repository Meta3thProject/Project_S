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
    private float zoomIn = 1;
    [SerializeField]
    private float zoomMax = 30;
    [SerializeField]
    private float zoomOneStep = 1;
    [SerializeField]
    private TextMeshProUGUI textmapName;

    private void Awake()
    {
        //textmapName = SceneManager.GetActivateScene().name;
    }
    public void ZoomIn()
    {
        minimapCamera.orthographicSize = Mathf.Max(minimapCamera.orthographicSize - zoomOneStep, zoomIn);
    }

    public void ZoomOut()
    {
        minimapCamera.orthographicSize = Mathf.Min(minimapCamera.orthographicSize - zoomOneStep, zoomIn);

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
