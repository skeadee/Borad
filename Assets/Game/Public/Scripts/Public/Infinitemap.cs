
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Infinitemap : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    public int speed;

    private BoxCollider2D box;
    private float ghLength;

    public bool length = false; // 세로 체크

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.bodyType = RigidbodyType2D.Kinematic;


        if (!length) _rb2d.velocity = new Vector2(speed, 0f);
        else _rb2d.velocity = new Vector2(0f, speed);

        box = GetComponent<BoxCollider2D>();

        if(!length)  ghLength = box.size.x; // 가로전체의 박스 가로 크기
        else ghLength = box.size.y;

    }

    private void Update()
    {
        if (!length && transform.position.x < -ghLength)
        {
            RepositionGround();
        }

        else if(length && transform.position.y < -ghLength)
        {
            RepositionGround();
        }
    }

    private void RepositionGround()
    {
        Vector2 _move;

        if (!length) _move = new Vector2(ghLength * 2, 0f); // 다음 장소로 맵을 이동해서 곱하기 2를 함
        else _move = new Vector2(0f , ghLength * 2);

        transform.position = (Vector2)transform.position + _move;
    }


}
