using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PerfumeType
{
    AnswerPerfume01, AnswerPerfume02, AnswerPerfume03, WrongPerfume
}

public class PerfumePuzzle : MonoBehaviour
{
    [field: SerializeField] private PerfumeType perfumeType;


}
