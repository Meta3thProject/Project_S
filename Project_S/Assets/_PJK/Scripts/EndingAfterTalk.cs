using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //star = gameObject;
        targetPosition = endingstarposition.transform.position;
        PlayerCamera = Camera.main;
    }

    public void StartEndingtalk()
    {
        star.gameObject.SetActive(true);

        startEnding();

    }



    public void startEnding()
    {

        StartCoroutine(SkytoDark());
        Player = Player.gameObject;
        bool isend = false;
        if (StarManager.starManager.getStarCount >= 22 && isend == false)
        {
            starnumber = StarManager.starManager.getStarCount;

            float radius = 10.0f;
            float angleIncrement = (2 * Mathf.PI) / StarManager.starManager.getStarCount;


            star.transform.localScale = Vector3.one * 0.01f;
            for (int i = 0; i < StarManager.starManager.getStarCount; i++)
            {
                float angle = i * angleIncrement;
                float x = gameObject.transform.position.x + radius * Mathf.Cos(angle);
                float y = gameObject.transform.position.y;
                float z = gameObject.transform.position.z + radius * Mathf.Sin(angle);
                stars.Add(Instantiate(star, Player.transform.position, Quaternion.identity));

                StarAppearSequence =
                DOTween.Sequence().SetAutoKill(false)
                // DOTween을 사용하여 오브젝트를 목표 위치로 이동시킵니다.
                .Append(stars[i].transform.DOSpiral(1f, Vector3.forward, SpiralMode.Expand, 0.1f, 1, 1, false))
                .Join(stars[i].transform.DOMove(new Vector3(x, y, z), 1f)
                .SetDelay(2f + i * 0.5f) // 딜레이 설정
                .SetEase(Ease.OutQuad))  // 이징(Ease) 설정
                .Append(stars[i].transform.DOScale(new Vector3(5f, 5f, 5f), 10));


                StartCoroutine(MoveToSphere());
                isend = true;
            }


        }

    }


        IEnumerator MoveToSphere()
        {
            yield return new WaitForSeconds(15f);

            for (int i = 0; i < stars.Count; i++)
            {
                Vector3 spherePosition = Sphere.transform.position;

                StarAppearSequence =
                DOTween.Sequence().SetAutoKill(false)
                .Append(stars[i].transform.DOMove(spherePosition, 1f))
                .Join(stars[i].transform.DOScale(new Vector3(1f, 1f, 1f), 1f));


                yield return new WaitForSeconds(0.5f); // 0.5초 대기
            }
        }

        IEnumerator SkytoDark()
        {
            float zerotoone = 1f;
            for (int i = 0; i < 100; i++)
            {
                zerotoone -= 0.01f;
                Vector2 time = new Vector2(zerotoone, 1);

                StarAppearSequence =
               DOTween.Sequence().SetAutoKill(false)
               .Append(Sky_Mat.DOTiling(time, 3f));
                yield return new WaitForSeconds(0.05f);
            }
        }
    
    public void endingafter()
    {
        Sky_Mat.DOTiling(new Vector2(1,1), 0.5f);
    }



}
