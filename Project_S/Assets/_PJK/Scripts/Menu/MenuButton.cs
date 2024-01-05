using BNG;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public GameObject Menu;
    public GameObject firstMenu;
    public GameObject option;
    public GameObject secondOption;
    public GameObject logOut;
    public GameObject gameExit;

    public UnityEngine.UI.Button stillplaybutton;
    public UnityEngine.UI.Button optionbutton;
    public UnityEngine.UI.Button logoutbutton;
    public UnityEngine.UI.Button gameoverbutton;
    private Text stillplaytext;
    private Text optiontext;
    private Text logouttext;
    private Text gameovertext;
    private Text stillplaytext2;
    private Text optiontext2;
    private Text logouttext2;
    private Text gameovertext2;
    private bool isOptionclose;



    private void Start()
    {
        stillplaytext = stillplaybutton.GetComponentInChildren<Text>();
        stillplaytext2 = stillplaybutton.GetComponentInChildren<Text>();
        optiontext = optionbutton.GetComponentInChildren<Text>();
        optiontext2 = optionbutton.GetComponentInChildren<Text>();
        logouttext = logoutbutton.GetComponentInChildren<Text>();
        logouttext2 = logoutbutton.GetComponentInChildren<Text>();
        gameovertext = gameoverbutton.GetComponentInChildren<Text>();
        gameovertext2 = gameoverbutton.GetComponentInChildren<Text>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) || InputBridge.Instance.BackButtonDown)
        {
            if (isOptionclose == true)
            {
                MenuOpen();
            }
            else if (isOptionclose == false)
            {
                MenuClose();
            }
        }
    }

    public void MenuOpen()
    {

        Debug.Log("옵션창꺼져있음");
        Menu.gameObject.SetActive(true);
        firstMenu.gameObject.SetActive(true);
        option.gameObject.SetActive(false);
        isOptionclose = false;
    }

    public void MenuClose()
    {

        Debug.Log("옵션창켜져있음");

        Menu.gameObject.SetActive(false);
        firstMenu.gameObject.SetActive(false);
        option.gameObject.SetActive(false);
        isOptionclose = true;
    }


    public void StillPlay()
    {
        Debug.Log("계속진행버튼클릭");

        firstMenu.gameObject.SetActive(false);

        option.gameObject.SetActive(false);

        Menu.gameObject.SetActive(false);
        isOptionclose = true;
    }
    public void Option()
    {
        Debug.Log("설정버튼클릭");

        firstMenu.gameObject.SetActive(false);

        option.gameObject.SetActive(true);

        secondOption.gameObject.SetActive(true);
        logOut.gameObject.SetActive(false);
        gameExit.gameObject.SetActive(false);
       

    }




    public void LogOut()
    {
        Debug.Log("로그아웃버튼클릭");

        firstMenu.gameObject.SetActive(false);

        option.gameObject.SetActive(true);

        secondOption.gameObject.SetActive(false);
        logOut.gameObject.SetActive(true);
        gameExit.gameObject.SetActive(false);

       

    }
    public void GameExit()
    {
        Debug.Log("게임종료버튼클릭");

        firstMenu.gameObject.SetActive(false);

        option.gameObject.SetActive(true);

        secondOption.gameObject.SetActive(false);
        logOut.gameObject.SetActive(false);
        gameExit.gameObject.SetActive(true);
       

    }



}
