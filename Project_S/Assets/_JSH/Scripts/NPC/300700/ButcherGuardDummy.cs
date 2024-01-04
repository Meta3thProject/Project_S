using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 더미 NPC라서 NPCBase를 상속받지 않는다
public class ButcherGuardDummy : MonoBehaviour, INPCBehaviour
{
    // 대사 리스트
    private List<string> dialogs;

    // 출력한 대사 인덱스
    public int indexToPrint = default;

    private void Awake()
    {
        dialogs = new List<string>();

        dialogs.Add("고객님, 여기는 들어갈 수 없습니다.");
        dialogs.Add("고객님, 뒤로 돌아가시죠.");
        dialogs.Add("계속 들어오려고 하신다면 힘으로 해결하겠습니다.");

        // 전에 출력한 문자열이 다시 나오지 않도록 할 예정임
        // 0~2 중의 하나로 설정해야 함
        indexToPrint = -1;
    }

    public void PopUpDialog()
    {
        // 출력했던 문자열 인덱스 임시 저장
        int printedIndex_ = indexToPrint;
        // 중복되지 않는 난수를 넣기 위한 루프
        do
        {
            // 랜덤한 수 넣어주기
            indexToPrint = Random.Range(0, dialogs.Count);
        }
        while (indexToPrint == printedIndex_);
        // 대사 출력
        NPCManager.Instance.ActivateMain(dialogs[indexToPrint]);
    }
}
