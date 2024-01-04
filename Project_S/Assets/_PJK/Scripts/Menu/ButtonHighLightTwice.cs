using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHighLightTwice : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button otherButton;
    private Button button;
    private Color normalColor;
    private Color highlightedColor;
    private Color pressedColor;
    private TextMeshProUGUI text;
    public TextMeshProUGUI othertext;

    private void Awake()
    {
        button = GetComponent<Button>();
    }


    void Start()
    {

        normalColor = new Color(50f / 255f, 50f / 255f, 50f / 255f, 1f);
        highlightedColor = Color.yellow;
        pressedColor = Color.white;
        text = GetComponentInChildren<TextMeshProUGUI>();
        othertext = otherButton.GetComponentInChildren<TextMeshProUGUI>();
    }





    // 마우스가 버튼에 들어왔을 때 호출될 메서드
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("버튼이 하이라이트 상태입니다.");
        text.color = highlightedColor;
        text.fontSize = 12;
        othertext.color = highlightedColor;
        othertext.fontSize = 12;


    }

    public void buttonclick()
    {
        // 버튼의 눌린 상태 색상 설정
        button.image.color = pressedColor;
        text.color = pressedColor;
        text.fontSize = 12;
        if (otherButton.image.color != pressedColor)
        {
            otherButton.image.color = pressedColor;
            othertext.color = pressedColor;
            othertext.fontSize = 12;
            Debug.Log("눌림");
        }
    }

    public void buttonup()
    {
        // 마우스가 버튼 밖에서 떼어졌을 때는 기본 색상으로 복원
        if (!RectTransformUtility.RectangleContainsScreenPoint(button.GetComponent<RectTransform>(), Input.mousePosition))
        {
            Debug.Log("눌렀다가 바깥에서 땜");
            button.image.color = normalColor;
            text.color = normalColor;
            text.fontSize = 10;
            if (otherButton.image.color != normalColor)
            {
                otherButton.image.color = normalColor;
                othertext.color = normalColor;
                othertext.fontSize = 10;
            }
        }
        // 마우스가 버튼 안에서 떼어졌을 때는 하이라이트 색상으로 복원
        else
        {
            Debug.Log("눌렀다가 안에서 땜");

            button.image.color = highlightedColor;
            text.color = pressedColor;
            text.fontSize = 12;
            if (otherButton.image.color != highlightedColor)

            {
                otherButton.image.color = highlightedColor;
                othertext.color = pressedColor;
                othertext.fontSize = 12;
            }
        }
    }
    // 마우스가 버튼에서 나갔을 때 호출될 메서드
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("버튼이 하이라이트 상태에서 벗어났습니다.");
        text.color = normalColor;
        text.fontSize = 10;
        othertext.color = normalColor;
        othertext.fontSize = 10;
    }
    // 버튼이 눌렸을 때 호출될 메서드
    public void OnPointerDown(PointerEventData eventData)
    {
        // 버튼의 눌린 상태 색상 설정
        button.image.color = pressedColor;
        text.color = pressedColor;
        text.fontSize = 12;
        if (otherButton.image.color != pressedColor)
        {
            otherButton.image.color = pressedColor;
            othertext.color = pressedColor;
            othertext.fontSize = 12;
        Debug.Log("눌림");
        }
    }

    // 버튼에서 손가락이 떼어졌을 때 호출될 메서드
    public void OnPointerUp(PointerEventData eventData)
    {
        

        // 마우스가 버튼 밖에서 떼어졌을 때는 기본 색상으로 복원
        if (!RectTransformUtility.RectangleContainsScreenPoint(button.GetComponent<RectTransform>(), Input.mousePosition))
        {
            Debug.Log("눌렀다가 바깥에서 땜");
            button.image.color = normalColor;
            text.color = normalColor;
            text.fontSize = 10;
            if (otherButton.image.color != normalColor)
            {
                otherButton.image.color = normalColor;
                othertext.color = normalColor;
                othertext.fontSize = 10;
            }
        }
        // 마우스가 버튼 안에서 떼어졌을 때는 하이라이트 색상으로 복원
        else
        {
            Debug.Log("눌렀다가 안에서 땜");

            button.image.color = highlightedColor;
            text.color = pressedColor;
            text.fontSize = 12;
            if (otherButton.image.color != highlightedColor)

            {
                otherButton.image.color = highlightedColor;
                othertext.color = pressedColor;
                othertext.fontSize = 12;
            }
        }


    }
}
