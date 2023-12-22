using System.Collections.Generic;
using UnityEngine;

public class FabricCheck : MonoBehaviour
{
    public List<GameObject> fabrics;


    private void Awake()
    {
        fabrics = new List<GameObject>();
    }

    public FabricStat CheckFabric()
    {
        for (int i = 0; i < fabrics.Count; i++)
        {
            if (fabrics[i].GetComponent<FabricStat>() != null)
            {
                return fabrics[i].GetComponent<FabricStat>();
            }
        }

        return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 리스트에 추가
        fabrics.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        // 리스트에서 제거
        fabrics.Remove(other.gameObject);
    }
}
