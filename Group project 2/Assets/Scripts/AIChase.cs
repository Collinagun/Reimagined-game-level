using UnityEngine;
using UnityEngine.SceneManagement;

public class AIChase : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float distanceBetween;

    private Vector2 defaultPosition;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        defaultPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FallDetector"))
        {
            transform.position = defaultPosition;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject); // Destroy the AI
            SceneManager.LoadScene("GameOver"); // Load the GameOver scene
        }
    }
}