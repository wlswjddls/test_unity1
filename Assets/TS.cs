using UnityEngine;
using UnityEngine.UI;

public class TS : MonoBehaviour
{
    //적
    public Sprite[] enemySprite;

    //적 위치
    public float enemyPositionX;
    public float enemyPositionY;
    //적 부모
    public Transform enemyParent;

    //기본값
    private RectTransform mainCanvasRectTransform;
    
    //적 위치값
    public GameObject createdenemy;

    //캔버스 값
    public RectTransform canvasRectTransform;

    void Start()
    {
        Canvas mainCanvas = FindObjectOfType<Canvas>();

        if(enemyParent==null)
        {
            enemyParent= mainCanvas.transform; 
        }
        enemyPositionX=0;
        enemyPositionY=500;

        createdenemy=CreateEnemy(enemyPositionX,enemyPositionY, enemyParent);
    }


    public GameObject CreateEnemy(float positionX, float positionY, Transform parent)
    {
        GameObject enemyContainer = new GameObject("enemyContainer");
        RectTransform containerRect = enemyContainer.AddComponent<RectTransform>();
        enemyContainer.transform.SetParent(parent, false);
        containerRect.anchoredPosition = new Vector2(positionX, positionY);
        containerRect.sizeDelta = new Vector2(100, 100);
        containerRect.transform.localScale = new Vector3(1, -1, 1);

        containerRect.anchorMin = new Vector2(0.5f, 0.5f);
        containerRect.anchorMax = new Vector2(0.5f, 0.5f);
        containerRect.pivot = new Vector2(0.5f, 0.5f);
        enemyContainer.transform.SetAsLastSibling();
        // 우주선 스프라이트가 쪼개져 있어서 그걸 다 담는 부모 스프라이트

        float size=40f;
        Vector2 targetLocalPosition = new Vector2(positionX, positionY);
        Vector2[] partPositions = new Vector2[] { new Vector2(-20, -15), new Vector2(20, -15), new Vector2(0, 0) };

        for (int i = 0; i < enemySprite.Length; i++)
        {

            GameObject partObject =new GameObject("enemyPart"+i);
            partObject.transform.SetParent(enemyContainer.transform, false);

            Image partObjectImage = partObject.AddComponent<Image>();

            partObjectImage.sprite = enemySprite[i];
            partObjectImage.raycastTarget = false;

            RectTransform rectTransform = partObjectImage.GetComponent<RectTransform>();

            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            if(i==2) rectTransform.sizeDelta = new Vector2(size, size+25);
            else rectTransform.sizeDelta = new Vector2(size+15, size-10);

            rectTransform.anchoredPosition = partPositions[i];
        }
        return enemyContainer;
    }
}
