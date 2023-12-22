using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPuzzleTrigger : MonoBehaviour
{
    [SerializeField] private ShieldType shieldTriggerType;

    [Header("방패가 트리거에 닿았을 때 회전을 위한 값")]
    [SerializeField] private float rotationValueX;
    [SerializeField] private float rotationValueY;
    [SerializeField] private float rotationValueZ;

    private ShieldPuzzleClear shieldPuzzleClear;
    private ShieldPuzzle shield;

    private bool isInterative = false;
    private Quaternion _Eulur;

    private Transform target;

    private void Awake()
    {
        shieldPuzzleClear = transform.parent.GetComponent<ShieldPuzzleClear>();
        _Eulur = Quaternion.Euler(rotationValueX, rotationValueY, rotationValueZ);
        target = transform.GetChild(0).transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isInterative) { return; }

        if (other.GetComponent<ShieldPuzzle>() != null)
        {
            isInterative = true;
            shield = other.GetComponent<ShieldPuzzle>();

            if (shield.shieldType == shieldTriggerType)
            {
                shield.EnterShieldShapeTrigger(_Eulur, target.transform.position);
                shieldPuzzleClear.IncreaseClearCheck((int)shieldTriggerType);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ShieldPuzzle>() != null)
        {
            isInterative = false;
            shield = other.GetComponent<ShieldPuzzle>();

            shield.ExitShieldShapeTrigger();

            if (shield.shieldType == shieldTriggerType)
            {
                shieldPuzzleClear.DecreaseClearCheck((int)shieldTriggerType);
            }
        }
    }
}
