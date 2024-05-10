using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RepeatBackground : MonoBehaviour
{
    private BoxCollider2D box;
    private float ghLength;

    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
        ghLength = box.size.x; // 박스 가로 크기
    }

    private void Update()
    {
        if(transform.position.x < -ghLength)
        {
            RepositionGround();
        }
    }

    private void RepositionGround()
    {
        Vector2 _move = new Vector2(ghLength * 2, 0f); // 다음 장소로 맵을 이동해서 곱하기 2를 함
        transform.position = (Vector2)transform.position + _move;
    }
}
