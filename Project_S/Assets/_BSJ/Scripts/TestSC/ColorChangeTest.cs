using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeTest : MonoBehaviour, IColorChange
{
    [SerializeField]
    private Material material;

    [SerializeField]
    private Color color;

    [SerializeField]
    private Gradient gradient;

    private void Start()
    {
        // TEST : DOColor
        color = material.color;

        // { TEST : Make Gradient 
        gradient = new Gradient();

        gradient.colorKeys = new GradientColorKey[]
        {
            new GradientColorKey(Color.red, 0.0f),
            new GradientColorKey(Color.yellow, 0.5f),
            new GradientColorKey(Color.green, 1.0f)
        };

        gradient.alphaKeys = new GradientAlphaKey[]
        {
            new GradientAlphaKey(1.0f, 0.0f),
            new GradientAlphaKey(0.5f, 0.5f),
            new GradientAlphaKey(0.0f, 1.0f)
        };
        // } TEST : Make Gradient

    }

    public void ChangeColor()
    {
        // TEST : DOColor
        //material.DOColor(Color.red, 1);

        // TEST : DOGradientColor
        material.DOGradientColor(gradient, 1f);
    }

    public void ReturnColor()
    {
        material.color = color;
    }
}
