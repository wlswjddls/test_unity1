using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    public string backGroundObjectName = "background";
    public Vector2 newBackgroundSize;

    public int screenWidth;
    public int screenHeight;
    public bool isFullScreen=false;

     

    void Start()
    {
        screenWidth=800;
        screenHeight=1500;
        newBackgroundSize= new Vector2(800f,1500f);

        GameObject backgroundGameObject = GameObject.Find(backGroundObjectName);
        if(backgroundGameObject != null)
        {
            Image backgroundImage = backgroundGameObject.GetComponent<Image>();
            if(backgroundImage != null)
            {
                RectTransform backgroundRectTransform = backgroundImage.GetComponent<RectTransform>();
                backgroundRectTransform.sizeDelta = newBackgroundSize;

                Color32 customColor32 = new Color32(0, 0, 0, 255);
                backgroundImage.color = customColor32;

                backgroundImage.transform.SetAsFirstSibling();
            }
        }
        Screen.SetResolution(screenWidth, screenHeight, isFullScreen);
        Debug.Log($"✅ 화면 해상도가 {screenWidth}x{screenHeight}로 설정되었습니다. 전체 화면 모드: {isFullScreen}");

    


    }
    
}
