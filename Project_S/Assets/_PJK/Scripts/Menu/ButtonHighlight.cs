using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    private Color normalColor;
    private Color highlightedColor;
    private Color pressedColor;
    private TextMeshProUGUI text;

    private void Awake()
    {
        button = GetComponent<Button>();
        
    }

    void Start()
    {
        normalColor = Color.white;
        highlightedColor = Color.white;
        pressedColor = Color.blue;
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void click()
    {
        Debug.Log("클릭");
    }
    public void clickup()
    {
        Debug.Log("손땜");
    }

    // 마우스가 버튼에 들어왔을 때 호출될 메서드
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("버튼이 하이라이트 상태입니다.");
        text.color = highlightedColor;
        text.fontSize = 12;
    }

    // 마우스가 버튼에서 나갔을 때 호출될 메서드
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("버튼이 하이라이트 상태에서 벗어났습니다.");
        text.color = normalColor;
        text.fontSize = 10;
    }
    // 버튼이 눌렸을 때 호출될 메서드
    public void OnPointerDown(PointerEventData eventData)
    {
        // 버튼의 눌린 상태 색상 설정
        button.image.color = pressedColor;
        Debug.Log("버튼이 프레스 상태입니다.");
        text.color = pressedColor;
        text.fontSize = 12;
    }

    // 버튼에서 손가락이 떼어졌을 때 호출될 메서드
    public void OnPointerUp(PointerEventData eventData)
    {
        // 마우스가 버튼 밖에서 떼어졌을 때는 기본 색상으로 복원
        if (!RectTransformUtility.RectangleContainsScreenPoint(button.GetComponent<RectTransform>(), Input.mousePosition))
        {
            button.image.color = normalColor;
            Debug.Log("버튼이 기본 상태입니다.");
            text.color = normalColor;
            text.fontSize = 10;
        }
        // 마우스가 버튼 안에서 떼어졌을 때는 하이라이트 색상으로 복원
        else
        {
            button.image.color = highlightedColor;
            Debug.Log("버튼이 프레스 상태입니다.");
            text.color = pressedColor;
            text.fontSize = 12;
        }
    }
}
