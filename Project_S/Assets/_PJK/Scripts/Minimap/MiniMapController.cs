using BNG;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapController : GrabbableEvents
{
    private int id = Shader.PropertyToID("Vector1_98d33b1d219b486e97f4a6d459a007a3");
    private int direction = Shader.PropertyToID("Vector1_d6c62a52b7fe47f88987fd02c26c39fe");

    private Material m = default;
    private float number = 0f;
    private float testFloat = 0f;
    public float speed = 0.1f;
    private GameObject minimap = default;
    private RectMask2D imagemask = default;
    private Canvas canvas = default;
    private bool isRightOpen = false;
    private bool isRightClose = false;
    private bool isLeftOpen = false;
    private bool isLeftClose = false;

    private Vector4 newPadding = default;
    private float left = 0f;
    private float bottom = 0f;
    private float top = 0f;
    private float right = 1090f;

    private bool isacting = false;
    protected override void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
        base.Awake();
    }
    void Start()
    {
        m = GetComponent<Renderer>().material;
        minimap = transform.GetChild(0).gameObject;
        imagemask = transform.GetChild(0).GetChild(0).GetComponent<RectMask2D>();
        newPadding = new Vector4(left, bottom, right, top);
        m.SetFloat(id, number);
        imagemask.padding = newPadding;
    }

    void Update()
    {
        if (!isacting)
        {
            if (thisGrabber != null && thisGrabber.HeldGrabbable != null)
            {
                if ((Input.GetKeyDown(KeyCode.J) || thisGrabber.HeldGrabbable.tag.Equals("MiniMapLeft")) && isacting == false)
                {
                    isacting = true;
                    StartCoroutine(MapOpenCoroutine());
                    //MapOpen();
                    MapScale.instance.isMapOpen = true;

                    //isacting = false;
                    //Debug.Log("맵열기종료");
                }
            }
            else if(thisGrabber != null && thisGrabber.HeldGrabbable == null)
            {
                if (Input.GetKeyDown(KeyCode.K) || thisGrabber.HeldGrabbable == null)
                {
                    StartCoroutine(MapCloseCoroutine());
                    //isacting = false;
                    MapScale.instance.isMapOpen = false;

                }
            }
        }
    }
    IEnumerator MapOpenCoroutine()
    {
        MapOpen(); // MapOpen 함수 호출

        // 여기서 MapOpen 함수의 코루틴이 끝날 때까지 기다립니다.
        yield return StartCoroutine(OpenLeftM());

        isacting = false;
    }

    IEnumerator MapCloseCoroutine()
    {
        MapClose(); // MapOpen 함수 호출

        // 여기서 MapOpen 함수의 코루틴이 끝날 때까지 기다립니다.
        yield return StartCoroutine(CloseLeftM());

        isacting = false;
    }
    public void MapOpen()
    {


        isacting = true;

        canvas.GetComponent<Canvas>().enabled = true;

        if (thisGrabber.HandSide == ControllerHand.Right)
        {

            OpenLeftMap();
            isLeftOpen = false;
            isLeftClose = false;
            isRightOpen = true;
            isRightClose = false;
        }




    }
    public void MapClose()
    {
        isacting = false;

        canvas.GetComponent<Canvas>().enabled = false;

        if (isRightOpen == true)
        {
            CloseLeftMap();

            isLeftOpen = false;
            isLeftClose = false;
            isRightOpen = false;
            isRightClose = true;
        }

    }

    void OpenLeftMap()
    {

        m.SetFloat(direction, 1);

        StartCoroutine(OpenLeftM());



    }
    IEnumerator OpenLeftM()
    {

        while (testFloat < 1f)
        {
            testFloat += speed * 0.1f;
            m.SetFloat(id, testFloat);
            right = 0;
            left = Mathf.Lerp(1090f, -50f, testFloat - 0.05f);
            newPadding = new Vector4(left, bottom, right, top);
            imagemask.padding = newPadding;
            //Debug.Log(testFloat);
            yield return new WaitForSeconds(0.01f);
        }

        isacting = false;

    }

    void CloseLeftMap()
    {
        m.SetFloat(direction, 1);
        StartCoroutine(CloseLeftM());
    }
    IEnumerator CloseLeftM()
    {
        while (testFloat > 0f)
        {
            testFloat -= speed * 0.1f;
            m.SetFloat(id, testFloat);
            right = Mathf.Lerp(1090f, -50f, testFloat - 0.05f);
            left = 0;
            newPadding = new Vector4(left, bottom, right, top);
            imagemask.padding = newPadding;
            yield return new WaitForSeconds(0.01f);
        }
        isacting = false;
    }
}