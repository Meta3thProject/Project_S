using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorChecker : MonoBehaviour
{
    [SerializeField]
    private Door[] doors = default;
    int touchNum = default;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        doors = GetComponentsInChildren<Door>();
    }

    public string CheckTouchDoor()
    {
        for(int i = 0; i < doors.Length; i++)
        {
            if (doors[i].isFirstTouch == true)
            {
                touchNum++;
            }
        }
        
        if(touchNum <= 8)
        {
            return Define.MBTI_S;
        }
        else
        {
            return Define.MBTI_N;
        }
    }
}
