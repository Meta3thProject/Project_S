using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 테스트용 가짜 인벤토리

public class InventoryFake : GSingleton<InventoryFake>
{
    public Dictionary<int, int> fakeItems;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);

        fakeItems = new Dictionary<int, int>();

        fakeItems.Add(100002, 1);
        fakeItems.Add(100004, 1);
        fakeItems.Add(100005, 1);
        fakeItems.Add(100003, 1);

        fakeItems.Add(105069, 1);
        fakeItems.Add(105070, 1);
        fakeItems.Add(105107, 1);
        fakeItems.Add(105108, 1);
    }
}
