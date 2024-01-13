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
        if (isacting == false)
        {
            if (thisGrabber != null && thisGrabber.HeldGrabbable != null)
            {
                if (Input.GetKeyDown(KeyCode.J) || thisGrabber.HeldGrabbable.tag.Equals("MiniMapLeft"))
                {

                    MapOpen();
                    isacting = true;
                    MapScale.instance.isMapOpen = isacting;


                }
            }
        }
        else if (isacting == true)
        {
            if (thisGrabber != null && thisGrabber.HeldGrabbable == null)
            {

                if (Input.GetKeyDown(KeyCode.K) || thisGrabber.HeldGrabbable == null)
                {

                    MapClose();
                    isacting = false;
                    MapScale.instance.isMapOpen = isacting;

                }
            }
        }
    }
    public void MapOpen()
    {

        Debug.Log("잡힘");
        isacting = true;

        canvas.GetComponent<Canvas>().enabled = true;

        //if (thisGrabber.HandSide == ControllerHand.Left)
        //{
        //    Debug.Log("왼손으로 잡음");
        //    OpenMap();
        //    isLeftOpen = true;
        //    isLeftClose = false;
        //    isRightOpen = false;
        //    isRightClose = false;
        //}
        //else 
        if (thisGrabber.HandSide == ControllerHand.Right)
        {
            Debug.Log("오른손으로 잡음");

            OpenLeftMap();
            isLeftOpen = false;
            isLeftClose = false;
            isRightOpen = true;
            isRightClose = false;
        }




    }
    public void MapClose()
    {

        //if (isLeftOpen == true)
        //{
        //    CloseMap();

        //    isLeftOpen = false;
        //    isLeftClose = true;
        //    isRightOpen = false;
        //    isRightClose = false;



        //    if (right < -50f)
        //    {
        //        canvas.GetComponent<Canvas>().enabled = false;

        //    }
        //    if (canvas.GetComponent<Canvas>().enabled == false)
        //    {

        //        isacting = false;
        //    }
        //}
        //else 
        if (isRightOpen == true)
        {
            CloseLeftMap();

            isLeftOpen = false;
            isLeftClose = false;
            isRightOpen = false;
            isRightClose = true;



            if (isLeftClose || isRightClose)
            {
                canvas.GetComponent<Canvas>().enabled = false;

            }
            if (canvas.GetComponent<Canvas>().enabled == false)
            {

                isacting = false;
            }
        }

    }


    //void OpenMap()
    //{
    //    Debug.Log("왼손으로 지도피기시작");

    //    m.SetFloat(direction, -1);
    //    StartCoroutine(OpenM());
    //    Debug.Log("왼손으로 지도피기시작2");


    //}
    //IEnumerator OpenM()
    //{
    //    while (testFloat < 1f)
    //    {

    //        testFloat += speed * 0.1f;
    //        m.SetFloat(id, testFloat);
    //        left = 0f;
    //        right = Mathf.Lerp(1090f, -50f, testFloat - 0.05f);
    //        newPadding = new Vector4(left, bottom, right, top);
    //        imagemask.padding = newPadding;
    //        //Debug.Log(testFloat);
    //        yield return new WaitForSeconds(0.01f);
    //    }
    //    isacting = false;

    //}

    void OpenLeftMap()
    {
        Debug.Log("오른손으로 지도피기시작");
        //gameObject.transform.position = new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y, gameObject.transform.position.z);
        m.SetFloat(direction, 1);

        StartCoroutine(OpenLeftM());
        Debug.Log("오른손으로 지도피기종료");



    }
    IEnumerator OpenLeftM()
    {
        Debug.Log("코루틴시작");
        //gameObject.transform.position = new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y, gameObject.transform.position.z);

        Debug.LogFormat("시작 testFloat:{0}",testFloat);
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
        Debug.Log("오른손으로 지도피기코루틴종료");

    }





    //void CloseMap()
    //{
    //    //gameObject.transform.position = new Vector3(0f, 2f, 0f);
    //    m.SetFloat(direction, -1);
    //    StartCoroutine(CloseM());
    //    //gameObject.transform.position = new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y, gameObject.transform.position.z);
    //}
    //IEnumerator CloseM()
    //{
    //    while (testFloat > 0f)
    //    {
    //        testFloat -= speed * 0.1f;
    //        m.SetFloat(id, testFloat);
    //        left = 0f;
    //        right = Mathf.Lerp(1090f, -50f, testFloat - 0.05f);
    //        newPadding = new Vector4(left, bottom, right, top);
    //        imagemask.padding = newPadding;
    //        yield return new WaitForSeconds(0.01f);
    //    }
    //    yield return null;
    //    isacting = false;
    //}

    void CloseLeftMap()
    {
        //  gameObject.transform.position = new Vector3(2.95f, 2f, 0f);
        m.SetFloat(direction, 1);
        StartCoroutine(CloseLeftM());
        Debug.Log(number);
    }
    IEnumerator CloseLeftM()
    {
        while (testFloat < 1f)
        {
            testFloat += speed * 0.1f;
            m.SetFloat(id, testFloat);
            right = 0;
            left = Mathf.Lerp(1090f, -50f, testFloat - 0.05f);
            newPadding = new Vector4(left, bottom, right, top);
            imagemask.padding = newPadding;
            Debug.Log(testFloat);
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
        isacting = false;
    }
}