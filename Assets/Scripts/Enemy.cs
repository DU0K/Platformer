using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public Transform[] points;

    private Rigidbody2D RB;
    private SpriteRenderer SR;
    private int i;
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
        transform.position = points[0].position;
    }

    void Update()
    {
        RB.transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, points[i].position) < 0.01f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }

        SR.flipX = points[i].transform.position.x > transform.position.x;
    }
}
