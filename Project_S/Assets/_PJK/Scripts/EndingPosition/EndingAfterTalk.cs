using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public partial class EndingAfterTalk : MonoBehaviour
{
    public static EndingAfterTalk endingAfterTalk;
    





    private void Awake()
    {

        endingAfterTalk = this;
        stars = new List<GameObject>();
    }
    void Start()
    {
        targetPosition = endingstarposition.transform.position;
        PlayerCamera = Camera.main;
        //playerstat.AddPoint(Define.MBTI_N, 10f);
        //playerstat.AddPoint(Define.MBTI_I, 10f);
    }

    public void StartEndingtalk()
    {


        startEnding();

    }



    public void startEnding()
    {
        GameManager.Instance.PlayerStat.GetMBTIStat();
        StartCoroutine(SkytoDark());
        GoddessAnimator.SetBool("EndTalk", true);

        Player = Player.gameObject;
        bool isend = false;
        if (StarManager.starManager.getStarCount >= 22 && isend == false)
        {
            starnumber = StarManager.starManager.getStarCount;

            float radius = 10.0f;
            float angleIncrement = (2 * Mathf.PI) / StarManager.starManager.getStarCount;


            //star.transform.localScale = Vector3.one * 0.01f;
            for (int i = 0; i < StarManager.starManager.getStarCount+1; i++)
            {
                float angle = i * angleIncrement;
                float x = gameObject.transform.position.x + radius * Mathf.Sin(angle);
                float y = gameObject.transform.position.y + radius * Mathf.Cos(angle);
                float z = gameObject.transform.position.z ;

                float targetx = endingstarposition.transform.position.x + radius * Mathf.Sin(angle);
                float targety = endingstarposition.transform.position.y + radius * Mathf.Cos(angle);
                float targetz = endingstarposition.transform.position.z;

                int ranx = Random.Range(-25, 25);
                int rany = Random.Range(3, 25);
                int ranz = Random.Range(-25, 25);
                stars.Add(Instantiate(star, Player.transform.position, Quaternion.identity));
                StarAppearSequence =
                DOTween.Sequence().SetAutoKill(false)
                // DOTween을 사용하여 오브젝트를 목표 위치로 이동시킵니다.
                .Append(stars[i].transform.DOMove(new Vector3(x+ranx, y+rany, z+ ranz), 1f))
                .Append(stars[i].transform.DOSpiral(1f, Vector3.forward, SpiralMode.Expand, 0.1f, 1, 1, false))
                .Join(stars[i].transform.DOMove(new Vector3(targetx, targety, targetz), 1f)
                .SetDelay(2f + i * 0.5f) // 딜레이 설정
                .SetEase(Ease.OutQuad))  // 이징(Ease) 설정
                .Append(stars[i].transform.DOScale(new Vector3(5f, 5f, 5f), 10))
                .OnComplete(() => StartCoroutine(MoveToSphere()));
            }
            isend = true;


        }

    }


    IEnumerator MoveToSphere()
    {
        for (int i = 0; i < stars.Count; i++)
        {
            Vector3 SphereSky = new Vector3(Sphere.transform.position.x, Sphere.transform.position.y+3f, Sphere.transform.position.z);
            Vector3 spherePosition = Sphere.transform.position;

            StarAppearSequence =
            DOTween.Sequence().SetAutoKill(false)
            //.Append(stars[i].transform.DOMove(SphereSky, 1f))
            .Append(stars[i].transform.DOMove(spherePosition, 1f))
            .Join(stars[i].transform.DOScale(new Vector3(1f, 1f, 1f), 1f));


            yield return new WaitForSeconds(0.5f); // 0.5초 대기
        }
    }

    IEnumerator SkytoDark()
    {
        Debug.Log("5SkytoDark들어옴?");

        float zerotoone = 1f;
        Sequence skySequence = DOTween.Sequence().SetAutoKill(false);
        for (int i = 0; i < 100; i++)
        {
            zerotoone -= 0.01f;
            Vector2 time = new Vector2(zerotoone, 1);

            skySequence = DOTween.Sequence().SetAutoKill(false);
            skySequence.Append(Sky_Mat.DOTiling(time, 3f));
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void endingafter()
    {
        Sky_Mat.DOTiling(new Vector2(1, 1), 0.5f);
    }



}
