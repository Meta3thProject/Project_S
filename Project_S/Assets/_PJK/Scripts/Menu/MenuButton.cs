using System.Collections;
using System.Collections.Generic;
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



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            MenuOpen();
        }
    }


    public void MenuOpen()
    {
        Menu.gameObject.SetActive(true);
        firstMenu.gameObject.SetActive(true);
        option.gameObject.SetActive(false);
    }
    public void Option()
    {
        option.gameObject.SetActive(true);
        firstMenu.gameObject.SetActive(false);

    }

    public void StillPlay()
    {
        firstMenu.gameObject.SetActive(true);
        option.gameObject.SetActive(false);
        Menu.gameObject.SetActive(false);
    }


    public void LogOut()
    {
        logOut.gameObject.SetActive(true);
        secondOption.gameObject.SetActive(false);
        gameExit.gameObject.SetActive(false);

    }

    public void SecondOption()
    {
        logOut.gameObject.SetActive(false);
        secondOption.gameObject.SetActive(true);
        gameExit.gameObject.SetActive(false);
    }
    public void GameExit()
    {
        logOut.gameObject.SetActive(false);
        secondOption.gameObject.SetActive(false);
        gameExit.gameObject.SetActive(true);
    }



}
