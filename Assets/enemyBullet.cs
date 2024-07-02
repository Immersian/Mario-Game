using UnityEditor;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force;
    public GameObject SnailAlive;
    public GameObject Shell;

    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        // Calculate the direction vector from this GameObject to the player, normalized to a unit vector
        Vector3 direction = (player.transform.position - transform.position).normalized;
        // Set the velocity of the Rigidbody2D to move in the direction of the player, scaled by the force value
        rb.velocity = direction * force;
        // Calculate the rotation angle in degrees based on the direction vector
        float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Apply the rotation to this GameObject, rotating it to face the player
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Bullet hit the player!");

            // Get the Player component from the collided object and call TakeDamage
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null && !player.IsInvulnerable) // Check if player is not invulnerable
            {
                player.TakeDamage();
            }

            Destroy(gameObject);
        }
    }
    public void TakeDamage()
    {
        // Disable the SnailAlive GameObject
        SnailAlive.SetActive(false);

        // Enable the Shell GameObject at the Snail's position
        Shell.transform.position = SnailAlive.transform.position;
        Shell.SetActive(true);
    }
}


