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

    private bool isOptionopen = false;

    private MenuController menuController = default;

    private void Start()
    {
        Init();
    }

    
    // HSJ_ 240122
    void Init()
    {
        menuController = GFunc.GetRootObj("Player").GetComponentInChildren<MenuController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) || InputBridge.Instance.BackButtonDown)
        {
            if (isOptionopen == false)
            {
                MenuOpen();
            }
            else if (isOptionopen == true)
            {
                MenuClose();
            }
        }
    }

    public void MenuOpen()
    {
        // HSJ_ 240122
        menuController.DisableMove(false);
        
        Menu.gameObject.SetActive(true);
        firstMenu.gameObject.SetActive(true);
        option.gameObject.SetActive(false);
        isOptionopen = true;

    }

    public void MenuClose()
    {
        // HSJ_ 240122
        menuController.DisableMove(true);

        Menu.gameObject.SetActive(false);
        firstMenu.gameObject.SetActive(false);
        option.gameObject.SetActive(false);
        isOptionopen = false;

    }


    public void StillPlay()
    {
        MenuClose();
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