using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniMapStars : MonoBehaviour
{
    private TMP_Text starsNumber = default;
    private int Star = default;
    
    // Start is called before the first frame update
    void Start()
    {
        starsNumber = GetComponent<TMP_Text>();

       
        
    }

    // Update is called once per frame
    void Update()
    {
        starsNumber.text = StarManager.starManager.getStarCount.ToString()+"/99";
    }
}
