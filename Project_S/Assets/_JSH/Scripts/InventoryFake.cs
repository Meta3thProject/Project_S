using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 테스트용 가짜 인벤토리

public class InventoryFake : MonoBehaviour
{
    public static InventoryFake Instance;

    public Dictionary<int, int> fakeItems;

    private void Awake()
    {
        // { 싱글톤
        if (null == Instance)
        {
            Instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        // } 싱글톤

        fakeItems = new Dictionary<int, int>();

        fakeItems.Add(100002, 1);
        fakeItems.Add(100004, 1);
        fakeItems.Add(101003, 1);

        fakeItems.Add(103011, 1);
        fakeItems.Add(103012, 1);
        fakeItems.Add(103013, 1);
        fakeItems.Add(103014, 1);
        fakeItems.Add(103015, 1);
        fakeItems.Add(103016, 1);

        fakeItems.Add(105003, 1);
        fakeItems.Add(105004, 1);

        fakeItems.Add(106000, 1);
        fakeItems.Add(106001, 1);
        fakeItems.Add(106002, 1);
        fakeItems.Add(106003, 1);

        fakeItems.Add(7777777, 1);
    }
}
