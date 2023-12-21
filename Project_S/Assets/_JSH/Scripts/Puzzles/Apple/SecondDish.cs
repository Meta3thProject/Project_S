using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondDish : Dish
{
    public bool isSecond;

    private void Awake()
    {
        isSecond = false;
    }
}
