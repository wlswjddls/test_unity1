using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed;
    private RectTransform canvasRectTransform; // 캔버스의 RectTransform 참조

    void Start()
    {
        canvasRectTransform = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    

    void Update()
    {
        // 매 프레임마다 y축(위쪽)으로 이동
        // UI 오브젝트이므로 transform.position을 사용합니다.
        transform.position += Vector3.up * speed * Time.deltaTime;

        CheckIfOffScreen();
    }
    
    void CheckIfOffScreen()
    {
        if (canvasRectTransform == null) return; // 캔버스 참조가 없으면 함수 종료

        // UI 캔버스는 화면 중앙이 (0,0)입니다.
        // 화면의 절반 너비와 높이를 계산합니다.
        float screenWidth = canvasRectTransform.rect.width / 2;
        float screenHeight = canvasRectTransform.rect.height / 2;

        // 현재 총알의 위치를 가져옵니다.
        Vector3 bulletPosition = transform.localPosition;
        
        // 총알이 화면 경계를 벗어났는지 확인합니다.
        // 예를 들어, 상단 경계(y > screenHeight)를 벗어났을 때 삭제합니다.
        // 만약 다른 방향으로도 움직인다면 모든 경계(좌, 우, 하단)를 확인해야 합니다.
        if (bulletPosition.y > screenHeight)
        {
            // 총알 오브젝트를 파괴합니다.
            Destroy(gameObject);
        }
    }
}