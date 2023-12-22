using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestStone : MonoBehaviour
{
    public TMP_Text questnumber = default;
    public TMP_Text questname = default;
    public TMP_Text questdetail = default;
    private int number = default;
    private int min = 0;
    private int max = 10;
    private Dictionary<int, string> questNumber = default;
    private Dictionary<int, string> questName = default;
    private Dictionary<int, string> questDetail = default;

    void Start()
    {
        questNumber = new Dictionary<int, string>()
        {
            {0,"첫번째 여행"},
            {1,"두번째 여행"},
            {2,"세번째 여행"}
        };

        questName = new Dictionary<int, string>()
        {
            {0,"빛을 잃어버린마을"},
            {1,"얼음 마을"},
            {2,"색깔을 찾는 여행"}
        };

        questDetail = new Dictionary<int, string>()
        {
            {0,"마을에 빛을 찾아주자"+min+"/"+max},
            {1,"얼어붙은 마을 사람들의 마음을 녹여주자"+min+"/"+max},
            {2,"잃어버린 색깔을 찾아보자"+min+"/"+max}
        };


        questnumber.text = questNumber[number];
        questname.text = questName[number];
        questdetail.text = questDetail[number];
    }

    public void Questclear(int quest)
    {
        questnumber.text = questNumber[number];
        questname.text = questName[number];
        questdetail.text = questDetail[number];
        number += quest;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Questclear(1);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            min += 1;

            questnumber.text = questNumber[number];
            questname.text = questName[number];
            questdetail.text = questDetail[number];

            if (min > max)
            {
                min = 0;
                Questclear(1);

            }
        }
        questNumber = new Dictionary<int, string>()
        {
            {0,"첫번째 여행"},
            {1,"두번째 여행"},
            {2,"세번째 여행"}
        };

        questName = new Dictionary<int, string>()
        {
            {0,"빛을 잃어버린마을"},
            {1,"얼음 마을"},
            {2,"색깔을 찾는 여행"}
        };
        questDetail = new Dictionary<int, string>()
        {
            {0,"마을에 빛을 찾아주자"+min+"/"+max},
            {1,"얼어붙은 마을 사람들의 마음을 녹여주자"+min+"/"+max},
            {2,"잃어버린 색깔을 찾아보자"+min+"/"+max}
        };
    }
}
