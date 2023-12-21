using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapController : MonoBehaviour
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

    private Vector4 newPadding = default;
    private float left = 0f;
    private float bottom = 0f;
    private float top = 0f;
    private float right = 1090f;

    private bool isacting = false;
    private void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
    }
    void Start()
    {
        m = GetComponent<Renderer>().material;
        minimap = transform.GetChild(0).GetComponent<GameObject>();
        imagemask = transform.GetChild(0).GetChild(0).GetComponent<RectMask2D>();
        newPadding = new Vector4(left, bottom, right, top);
        m.SetFloat(id, number);
        imagemask.padding = newPadding;
    }

    void Update()
    {
        if (isacting == false)
        {


            if (Input.GetKeyDown(KeyCode.J))
            {
                isacting = true;
                canvas.GetComponent<Canvas>().enabled = true;

                OpenMap();
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                isacting = true;
                CloseMap();
                if (right < -50f)
                {
                    canvas.GetComponent<Canvas>().enabled = false;

                }

            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                isacting = true;
                canvas.GetComponent<Canvas>().enabled = true;
                CloseLeftMap();
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                isacting = true;
                OpenLeftMap();
                canvas.GetComponent<Canvas>().enabled = false;

            }
        }
    }

    void OpenMap()
    {

        //gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+2f, gameObject.transform.position.z);
        m.SetFloat(direction, -1);
        StartCoroutine(OpenM());
        Debug.Log(number);


    }
    IEnumerator OpenM()
    {
        while (testFloat < 1f)
        {

            testFloat += speed * 0.1f;
            m.SetFloat(id, testFloat);
            left = 0f;
            right = Mathf.Lerp(1090f, -50f, testFloat - 0.05f);
            newPadding = new Vector4(left, bottom, right, top);
            imagemask.padding = newPadding;
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
        isacting = false;

    }

    void OpenLeftMap()
    {
        //gameObject.transform.position = new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y, gameObject.transform.position.z);
        m.SetFloat(direction, 1);

        StartCoroutine(OpenLeftM());
        Debug.Log(number);


    }
    IEnumerator OpenLeftM()
    {
        while (testFloat > 0f)
        {
            testFloat -= speed * 0.1f;
            m.SetFloat(id, testFloat);
            right = 0f;
            left = Mathf.Lerp(1090f, -50f, testFloat - 0.05f);
            newPadding = new Vector4(left, bottom, right, top);
            imagemask.padding = newPadding;
            Debug.Log(testFloat);
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
        isacting = false;
    }





    void CloseMap()
    {
        //gameObject.transform.position = new Vector3(0f, 2f, 0f);
        m.SetFloat(direction, -1);
        StartCoroutine(CloseM());
        Debug.Log(number);
        //gameObject.transform.position = new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y, gameObject.transform.position.z);
    }
    IEnumerator CloseM()
    {
        while (testFloat > 0f)
        {
            testFloat -= speed * 0.1f;
            m.SetFloat(id, testFloat);
            left = 0f;
            right = Mathf.Lerp(1090f, -50f, testFloat - 0.05f);
            newPadding = new Vector4(left, bottom, right, top);
            imagemask.padding = newPadding;
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
        isacting = false;
    }

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