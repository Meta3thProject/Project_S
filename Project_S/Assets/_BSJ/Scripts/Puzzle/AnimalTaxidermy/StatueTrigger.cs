using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatueTrigger : MonoBehaviour
{
    [SerializeField] private AnimalType taxidermyTriggerType;

    [Header("동물 박제의 회전을 위한 값")]
    [SerializeField] private float rotationValueX;
    [SerializeField] private float rotationValueY;
    [SerializeField] private float rotationValueZ;

    private TaxidermyClear taxidermyClear;
    private Taxidermy taxidermy;

    private bool isInterative = false;
    private Quaternion _Eulur;

    private Image circle_Image;
    private Transform _TargetPositionChange;

    private void Awake()
    {
        _Eulur = Quaternion.Euler(rotationValueX, rotationValueY, rotationValueZ);
        taxidermyClear = transform.parent.parent.GetComponent<TaxidermyClear>();
        circle_Image = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        _TargetPositionChange = transform.GetChild(1).GetComponent<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isInterative) { return; }

        if (other.GetComponent<Taxidermy>() != null)
        {
            isInterative = true;
            circle_Image.gameObject.SetActive(false);
            taxidermy = other.GetComponent<Taxidermy>();

            taxidermy.EnterFrameTrigger(_Eulur, _TargetPositionChange.position);

            if (taxidermy.TaxidermyType == taxidermyTriggerType)
            {
                taxidermyClear.IncreaseClearCheck((int)taxidermyTriggerType);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Taxidermy>() != null)
        {
            isInterative = false;
            circle_Image.gameObject.SetActive(true);
            taxidermy = other.GetComponent<Taxidermy>();

            taxidermy.ExitFrameTrigger();

            if (taxidermy.TaxidermyType == taxidermyTriggerType)
            {
                taxidermyClear.DecreaseClearCheck((int)taxidermyTriggerType);
            }
        }
    }
}
