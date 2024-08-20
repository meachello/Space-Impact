using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed = 4f;
    public float rotationSpeed = 1f;
    private float _angleToRotate;
    public float deactivateAsteroidTimer = 21f;
    
    public Animator animator;
    
    public float spawnTimer = 20f;
    private float _currentSpawnTimer;
    private bool _canSpawn;
    
    [SerializeField] private GameObject AsteroidPrefab;
    [SerializeField] private Transform asteroidPoint;
    private static readonly int Destroyed = Animator.StringToHash("Destroyed");

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DeactivateGameObject), deactivateAsteroidTimer);
        AsteroidPrefab.GetComponent<Collider2D>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        AsteroidSpawn();
        transform.Rotate(new Vector3(0, 0, 30) * Time.deltaTime);
        AsteroidMove();
    }

    void AsteroidMove()
    {
        Vector3 temp = transform.position;
        temp.x -= speed * Time.deltaTime;
        transform.position = temp;
    }

    void AsteroidSpawn()
    {
        _currentSpawnTimer += Time.deltaTime;
        if (_currentSpawnTimer > spawnTimer)
        {
            _canSpawn = true;
        }
        
        if (_canSpawn)
        {
            _canSpawn = false;
            _currentSpawnTimer = 0f;
            Instantiate(AsteroidPrefab, asteroidPoint.position, Quaternion.identity);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, 0.55f);
            animator.SetBool(Destroyed, true);
            AsteroidPrefab.GetComponent<Collider2D>().enabled = false;
            if (ScoreManager.Score < 1302)
            {
                Instantiate(AsteroidPrefab, asteroidPoint.position, Quaternion.identity);
            }
        }
    }

    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }
}
