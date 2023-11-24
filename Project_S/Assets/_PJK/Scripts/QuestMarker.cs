using UnityEngine;
using UnityEngine.UI;

public class QuestMarker : MonoBehaviour
{
    public Text questDirectionText; // 퀘스트 방향을 나타내는 UI 텍스트
    public Text distanceText; // NPC와의 거리를 나타내는 UI 텍스트
    public Transform questTarget; // 퀘스트 대상 NPC의 Transform

    void Update()
    {
        UpdateUIPosition();
        UpdateDistance();
    }

    void UpdateUIPosition()
    {
        // 플레이어의 시야 바로 앞으로 UI 요소 이동
        Vector3 playerPosition = transform.position;
        Vector3 playerForward = transform.forward;
        float distance = 2f; // 표시할 거리 조정 가능

        Vector3 newPosition = playerPosition + playerForward * distance;
        transform.position = newPosition;
        transform.rotation = Quaternion.LookRotation(playerForward);
    }

    void UpdateDistance()
    {
        // NPC와 플레이어 사이의 거리 계산 및 UI 업데이트
        if (questTarget != null)
        {
            float distance = Vector3.Distance(transform.position, questTarget.position);
            distanceText.text = $"Distance: {distance:F2} units"; // 원하는 형식으로 거리 표시
        }
    }
}
