using TMPro;
using UnityEngine;

public class MiniMapText : MonoBehaviour
{

    public GameObject Player;
    public TextMeshProUGUI allStar;
    public TextMeshProUGUI worldMapallStar2;
    public TextMeshProUGUI zoneStarScoreText;
    public TextMeshProUGUI[] WorldMapzoneStarScoreTexts = new TextMeshProUGUI[5];
    public TextMeshProUGUI zoneName;
    private int zoneStar;
    private int zoneHaveStar;
    private string zonename;
    private int clear1;
    private int clear2;
    private int clear3;
    private int clear4;
    private int clear5;
    private bool isnumber = false;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

        if (MapScale.instance.isMapOpen == true)
        {
            if (isnumber == false)
            {
                OpenMap();
                isnumber = true;
            }

            if (MapScale.instance.isZoneMap == true)
            {


                if (MapScale.instance.iszone1 == true)
                {
                    zonename = "정령의 섬";
                    zoneHaveStar = clear1;
                    zoneStar = 1;
                }
                else if (MapScale.instance.iszone2 == true)
                {
                    zonename = "중앙섬";
                    zoneHaveStar = clear2;
                    zoneStar = 13;
                }
                else if (MapScale.instance.iszone3 == true)
                {
                    zonename = "도구섬";
                    zoneHaveStar = clear3;
                    zoneStar = 4;
                }
                else if (MapScale.instance.iszone4 == true)
                {
                    zonename = "외부섬";
                    zoneHaveStar = clear4;
                    zoneStar = 2;
                }
                else if (MapScale.instance.iszone5 == true)
                {
                    zonename = "대형 정육점";
                    zoneHaveStar = clear5;
                    zoneStar = 3;
                }

                allStar.text = $"{StarManager.starManager.getStarCount}";
                zoneStarScoreText.text = $"Zone클리어률 {zoneHaveStar}/{zoneStar}";
                zoneName.text = $"{zonename}";
            }
            else if (MapScale.instance.isWorldMap == true)
            {
                WorldMapzoneStarScoreTexts[0].text = $"{clear1}/1";
                WorldMapzoneStarScoreTexts[1].text = $"{clear2}/13";
                WorldMapzoneStarScoreTexts[2].text = $"{clear3}/4";
                WorldMapzoneStarScoreTexts[3].text = $"{clear4}/2";
                WorldMapzoneStarScoreTexts[4].text = $"{clear5}/3";
                worldMapallStar2.text = $"{StarManager.starManager.getStarCount}";

            }



        }
        else if (MapScale.instance.isMapOpen == false)
        {
            isnumber = false;
        }
    }


    public void OpenMap()
    {
        clear1 = 0;
        clear2 = 0;
        clear3 = 0;
        clear4 = 0;
        clear5 = 0;
        for (int i = 0; i < PuzzleManager.instance.puzzles.Length; i++)
        {
            if (PuzzleManager.instance.puzzles[i] == true)
            {
                switch (i)
                {
                    case 14:


                        clear1++;
                        break;


                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 5:
                    case 9:
                    case 10:
                    case 18:
                    case 19:
                    case 20:
                    case 21:
                    case 22:


                        clear2++;
                        break;


                    case 6:
                    case 7:
                    case 8:
                    case 11:

                        clear3++;
                        break;

                    case 4:
                    case 17:

                        clear4++;
                        break;

                    case 12:
                    case 15:
                    case 16:

                        clear5++;
                        break;


                    default:
                        break;

                }
            }
        }

    }
}