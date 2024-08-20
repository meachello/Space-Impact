using UnityEngine;

public class BackgrondMove : MonoBehaviour
{
    public float speed = 2f;
    public float startX;
    public float endX;
    void Update()
    {
        Move();
        if (transform.position.x < endX)
        {
            transform.position = new Vector2(startX, transform.position.y);
        }
    }
    
    void Move()
    {
        Vector3 temp = transform.position;
        temp.x -= speed * Time.deltaTime;
        transform.position = temp;
        
    }
}
