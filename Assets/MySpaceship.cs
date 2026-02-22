using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    //총알
    public GameObject bulletPrefab;

    //총알 속도
    public float bulletSpeed = 500f;

    //우주선
    public Sprite[] spaceshipSprite;

    //우주선 위치
    public float spaceshipPositionX;
    public float spaceshipPositionY;
    //우주선 부모
    public Transform spaceshipParent;

    //기본값
    private RectTransform mainCanvasRectTransform;
    
    // 우주선 위치값
    public GameObject createdSpaceship;

    //우주선 이동속도
    private float moveSpeed;

    //캔버스 값
    public RectTransform canvasRectTransform;

    void Start()
    {
        canvasRectTransform = FindObjectOfType<Canvas>().GetComponent<RectTransform>();

        moveSpeed=400f;
        Canvas mainCanvas = FindObjectOfType<Canvas>();

        if(spaceshipParent==null)
        {
            spaceshipParent= mainCanvas.transform; 
        }
        spaceshipPositionX=0;
        spaceshipPositionY=-400;

        createdSpaceship=CreateSpaceship(spaceshipPositionX,spaceshipPositionY, spaceshipParent);
    }

    void Update()
    {
        RectTransform rectTransform = createdSpaceship.GetComponent<RectTransform>();

        // 1. spaceship 이동.
        Vector2 movement = Vector2.zero; // 초기 이동 벡터를 (0,0)으로 설정합니다.

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            movement.x = -1f;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            movement.x = 1f;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            movement.y = 1f;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            movement.y = -1f;
        }
        rectTransform.anchoredPosition += movement.normalized * moveSpeed * Time.deltaTime;
        Vector3 currentPosition = rectTransform.position;

        // x, y 좌표를 지정된 범위로 제한합니다.
        currentPosition.x = Mathf.Clamp(currentPosition.x, 0f, 800f);
        currentPosition.y = Mathf.Clamp(currentPosition.y, 0f, 1500f);

        // 수정된 위치를 다시 RectTransform에 할당합니다.
        rectTransform.position = currentPosition;

        //2. 총알 발사
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBullet(createdSpaceship.transform.position);
        }
    }

    public GameObject CreateSpaceship(float positionX, float positionY, Transform parent)
    {
        GameObject spaceshipContainer = new GameObject("SpaceshipContainer");
        RectTransform containerRect = spaceshipContainer.AddComponent<RectTransform>();
        spaceshipContainer.transform.SetParent(parent, false);
        containerRect.anchoredPosition = new Vector2(positionX, positionY);
        containerRect.sizeDelta = new Vector2(100, 100);

        containerRect.anchorMin = new Vector2(0.5f, 0.5f);
        containerRect.anchorMax = new Vector2(0.5f, 0.5f);
        containerRect.pivot = new Vector2(0.5f, 0.5f);
        spaceshipContainer.transform.SetAsLastSibling();
        // 우주선 스프라이트가 쪼개져 있어서 그걸 다 담는 부모 스프라이트

        float size=40f;
        Vector2 targetLocalPosition = new Vector2(positionX, positionY);
        Vector2[] partPositions = new Vector2[] { new Vector2(-20, -15), new Vector2(20, -15), new Vector2(0, 0) };

        for (int i = 0; i < spaceshipSprite.Length; i++)
        {
            GameObject partObject =new GameObject("MySpaceshipPart"+i);
            partObject.transform.SetParent(spaceshipContainer.transform, false);

            Image partObjectImage = partObject.AddComponent<Image>();

            partObjectImage.sprite = spaceshipSprite[i];
            partObjectImage.raycastTarget = false;

            RectTransform rectTransform = partObjectImage.GetComponent<RectTransform>();

            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            if(i==2) rectTransform.sizeDelta = new Vector2(size, size+25);
            else rectTransform.sizeDelta = new Vector2(size+15, size-10);

            rectTransform.anchoredPosition = partPositions[i];
        }
        return spaceshipContainer;
    }

    void ShootBullet(Vector3 startPosition) 
    {
        GameObject newBullet = Instantiate(bulletPrefab, canvasRectTransform);

        RectTransform bulletRect = newBullet.GetComponent<RectTransform>();
        
        bulletRect.position =startPosition;

        bullet bulletScript = newBullet.GetComponent<bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetSpeed(bulletSpeed);
        }
    }


}