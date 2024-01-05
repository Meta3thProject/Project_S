using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class YesOrNoHighLight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    private Color normalColor;
    private Color buttonColor;
    private Color pressedColor;
    private TextMeshProUGUI text;
    Vector3 size;

    private void Awake()
    {
        button = GetComponent<Button>();
        
    }

    void Start()
    {
        normalColor = new Color(50 / 255, 50 / 255, 50 / 255, 1f);

        buttonColor = Color.white;
        pressedColor = new Color(50 / 255, 50 / 255, 50 / 255, 1f);
        text = GetComponentInChildren<TextMeshProUGUI>();

        size = button.gameObject.transform.localScale;
    }


    // 마우스가 버튼에 들어왔을 때 호출될 메서드
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("버튼이 하이라이트 상태입니다.");
        button.gameObject.transform.localScale = size * 1.2f;
        text.fontSize = 24;
    }

    // 마우스가 버튼에서 나갔을 때 호출될 메서드
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("버튼이 하이라이트 상태에서 벗어났습니다.");
        button.gameObject.transform.localScale = size;
        text.fontSize = 20;
    }

    
}
