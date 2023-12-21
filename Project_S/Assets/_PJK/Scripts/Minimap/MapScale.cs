using UnityEngine;
using UnityEngine.UI;

public class MapScale : MonoBehaviour
{
    public RenderTexture minimapMaxCameraTexture = default;
    public RenderTexture minimapPlayerCameraTexture = default;
    public RawImage minimapImage = default;
    MiniMapMarkManager miniMapMarkManager = default;

    void Start()
    {
        miniMapMarkManager = GetComponent<MiniMapMarkManager>();
        minimapImage.texture = minimapPlayerCameraTexture;
    }


    public void changetoplayer()
    {
        minimapImage.texture = minimapPlayerCameraTexture;
        miniMapMarkManager.isPlayermap = true;
        miniMapMarkManager.isWorldmap = false;

    }

    public void changetoMaxMap()
    {
        minimapImage.texture = minimapMaxCameraTexture;
        miniMapMarkManager.isWorldmap = true;
        miniMapMarkManager.isPlayermap = false;

    }
}
