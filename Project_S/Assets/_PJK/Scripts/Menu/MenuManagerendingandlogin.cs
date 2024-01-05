using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;

public class MenuManagerendingandlogin : MonoBehaviour
{
    public void gotologinscene()
    {
       
        FirebaseManager.instance.LogOut(true);
    }

    public void gameover()
    {
        FirebaseManager.instance.LogOut(false);
    }
}
