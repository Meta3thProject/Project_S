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

    private bool isOptionclose = true;


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

        isOptionclose = false;
        Debug.Log("옵션창꺼져있음");
        Menu.gameObject.SetActive(true);
        firstMenu.gameObject.SetActive(true);
        option.gameObject.SetActive(false);
    }

    public void MenuClose()
    {

        Debug.Log("옵션창켜져있음");
        isOptionclose = true;

        Menu.gameObject.SetActive(false);
        firstMenu.gameObject.SetActive(false);
        option.gameObject.SetActive(false);
    }


    public void StillPlay()
    {
        // BSJ _ 240105
        return;

        //Debug.Log("계속진행버튼클릭");

        //isOptionclose = true;
        //firstMenu.gameObject.SetActive(false);

        //option.gameObject.SetActive(false);

        //Menu.gameObject.SetActive(false);
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
