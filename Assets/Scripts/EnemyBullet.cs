using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 7f;
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
        temp.x -= speed * Time.deltaTime;
        transform.position = temp;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
        
    }

    void DeactivateGameObject()
    {
        Destroy(gameObject);
    }
}
