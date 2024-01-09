using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;

public class WorkBanch : MonoBehaviour
{
    // 아이템ID를 체크해서 조합

    public List<GameObject> onWorkBanch;

    private Dictionary<string, int> idStringToID;

    private StringBuilder stringBuilder;

    private void Awake()
    {
        idStringToID = new Dictionary<string, int>();

        idStringToID.Add("100002100004", 100005);   // 목재 + 기름 = 횃불
        idStringToID.Add("101000101001101002", 101003);   // 사과 + 바나나 + 바구니 = 과일 바구니
        idStringToID.Add("100003104002104005", 104006);   // 돌 + 검은 꽃 + 옷감 = 검은 옷감
        idStringToID.Add("100003104003104005", 104007);   // 돌 + 노란 꽃 + 옷감 = 노란 옷감
        idStringToID.Add("105000105001", 105003);          // 탄산수 + 활기 오렌지 가루 = 활기 주스
        idStringToID.Add("105000105002", 105004);          // 탄산수 + 고요 레몬 가루 = 고요 주스
        idStringToID.Add("105007105008", 105009);          // 빨간 향수 + 노란 향수 = 주황 향수

        stringBuilder = new StringBuilder();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabbable"))
        {
            // 감지한 것을 리스트에 추가
            onWorkBanch.Add(other.gameObject);
        }
        // 아이템 조합 시도
        CombineItems();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabbable"))
        {
            // 감지한 것을 리스트에서 제거
            onWorkBanch.Remove(other.gameObject);
        }
        // 아이템 조합 시도
        CombineItems();
    }

    public void CombineItems()
    {
        // 요소 정렬
        onWorkBanch.Sort();

        for (int i = 0; i < onWorkBanch.Count; i++)
        {
            stringBuilder.Append(onWorkBanch[i].GetComponent<ItemData>().itemID.ToString());
        }

        foreach (string str in idStringToID.Keys)
        {
            if (str == stringBuilder.ToString())
            {
                // 조합대 위의 모든 아이템 삭제
                for (int i = onWorkBanch.Count - 1; i >= 0; i--)
                {
                    GameObject temp_ = onWorkBanch[i];
                    onWorkBanch.Remove(temp_);
                    Destroy(temp_);
                }

                // 조합대 컬라이더 중앙에서 조합된 아이템 생성
                ItemDataManager.instance.InstanceItem(idStringToID[str], GetComponent<Collider>().bounds.center);
                break;
            }
        }

        // 비운다
        stringBuilder.Clear();
    }
}

[CustomEditor(typeof(WorkBanch))]
public class CombineItemButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        WorkBanch workBanch = (WorkBanch)target;

        if (GUILayout.Button("Combine Items"))
        {
            workBanch.CombineItems();
        }
    }
}
