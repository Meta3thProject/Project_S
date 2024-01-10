using UnityEngine;
using UnityEngine.UI;

public class MapScale : MonoBehaviour
{
    public GameObject Player;
    public RenderTexture minimapCameraTexture = default;
    public GameObject minimapCamera = default;
    private Camera cam = default;
    public GameObject worldpos;
    public GameObject zone1pos;
    public GameObject zone2pos;
    public GameObject zone3pos;
    public GameObject zone4pos;
    public GameObject zone5pos;

    public RawImage minimapImage = default;
    private bool iszone1 = true;
    private bool iszone2 = false;
    private bool iszone3 = false;
    private bool iszone4 = false;
    private bool iszone5 = false;

    private bool isZoneMap = false;
    private bool isWorldMap = false;

    private bool isMapOpen = false;
    private Vector3 pos = default;
    private Rect defaultmap = new Rect(0.2f, 0.2f, 0.6f, 0.6f);
    private Rect zone4Rect = new Rect(0.2f, 0.2f, 0.6f, 0.6f);
    private Rect zone5Rect = new Rect(0.2f, 0.2f, 0.6f, 0.6f);



    MiniMapMarkManager miniMapMarkManager = default;

    void Start()
    {
        cam = minimapCamera.GetComponent<Camera>();
        miniMapMarkManager = GetComponent<MiniMapMarkManager>();
        //minimapImage.texture = minimapMaxCameraTexture;
    }




    private void Update()
    {
        



        if (isMapOpen == false)
        {
            return;
        }
        else if (isMapOpen == true)
        {
            pos = NPCManager.Instance.player.transform.position;

        }

    }

    public void CameraControll()
    {

        if (isZoneMap == true)
        {
            //zone1map
            if (50f < pos.x && pos.x < 50f && 50f < pos.z && pos.z < 50f)
            {
                iszone1 = true;
                iszone2 = false;
                iszone3 = false;
                iszone4 = false;
                iszone5 = false;
                cam.orthographicSize = 34f;
                minimapCamera.transform.position = zone1pos.transform.position;
            }
            //zone2map
            else if (pos.x < 50f && pos.z < 50f && 50f < pos.x && 50f < pos.z)
            {
                iszone1 = false;
                iszone2 = true;
                iszone3 = false;
                iszone4 = false;
                iszone5 = false;
                cam.orthographicSize = 76f;
                minimapCamera.transform.position = zone2pos.transform.position;
            }
            //zone3map
            else if (pos.x < 50f && pos.z < 50f && 50f < pos.x && 50f < pos.z)
            {
                iszone1 = false;
                iszone2 = false;
                iszone3 = true;
                iszone4 = false;
                iszone5 = false;
                cam.orthographicSize = 27.5f;
                minimapCamera.transform.position = zone3pos.transform.position;

            }
            //zone4map
            else if (pos.x < 50f && pos.z < 50f && 50f < pos.x && 50f < pos.z)
            {
                iszone1 = false;
                iszone2 = false;
                iszone3 = false;
                iszone4 = true;
                iszone5 = false;
                cam.orthographicSize = 76f;
                minimapCamera.transform.position = zone2pos.transform.position;
                cam.rect = zone4Rect;
            }

            //zone5map
            else if (pos.x < 50f && pos.z < 50f && 50f < pos.x && 50f < pos.z)
            {
                iszone1 = false;
                iszone2 = false;
                iszone3 = false;
                iszone4 = false;
                iszone5 = true;
                cam.orthographicSize = 76f;
                minimapCamera.transform.position = zone2pos.transform.position;
            }


        }
        //worldmap
        else if (isWorldMap == true)
        {

        }
    }


    public void Zone()
    {
        if (iszone1 == true)
        {
            minimapCamera.transform.position = zone1pos.transform.position;

        }
        else if (iszone2 == true)
        {
            minimapCamera.transform.position = zone2pos.transform.position;

        }
        
        else if (iszone3 == true)
        {
            minimapCamera.transform.position = zone3pos.transform.position;

        }
        else if (iszone4 == true)
        {
            minimapCamera.transform.position = zone4pos.transform.position;

        }
        
        else if (iszone5 == true)
        {
            minimapCamera.transform.position = zone5pos.transform.position;

        }
       
    }


    public void changetoWorldMap()
    {
        miniMapMarkManager.isWorldmap = true;
        miniMapMarkManager.isZonemap = false;
        //minimapImage.texture = minimapZone1CameraTexture;


    }

    public void changetoZoneMap()
    {
        miniMapMarkManager.isZonemap = true;
        miniMapMarkManager.isWorldmap = false;
        //minimapImage.texture = minimapZone1CameraTexture;
    }
}
