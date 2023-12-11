using BNG;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProjectsVrTextInput : MonoBehaviour
{
    [SerializeField]
    TMP_InputField thisInputField;

    public ProjectsVrKeyboard AttachedKeyboard;

    void Awake()
    {
        thisInputField = GetComponent<TMP_InputField>();
    }

    public void OnInputSelect()
    {
        Debug.Log("선택함.");
        AttachedKeyboard.gameObject.SetActive(true);
        AttachedKeyboard.AttachToInputField(thisInputField);
    }

    public void OnInputDeselect()
    {
        Debug.Log("선택하지않음");
        AttachedKeyboard.gameObject.SetActive(false);
    }

    // Assign the AttachedKeyboard variable when adding the component to a GameObject for the first time
    void Reset()
    {
        var keyboard = GameObject.FindObjectOfType<ProjectsVrKeyboard>();
        if (keyboard)
        {
            AttachedKeyboard = keyboard;
            Debug.Log("Found and attached Keyboard to " + AttachedKeyboard.transform.name);
        }
    }
}
