using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public float deactivateTimer = 4f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DeactivateGameObject), deactivateTimer);
    }

    // Update is called once per frame
    void Update()
    {
        BulletMove();
    }

    void BulletMove()
    {
        Vector3 temp = transform.position;
        temp.x += speed * Time.deltaTime;
        transform.position = temp;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Asteroid") || other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    void DeactivateGameObject()
    {
        Destroy(gameObject);
    }
}
