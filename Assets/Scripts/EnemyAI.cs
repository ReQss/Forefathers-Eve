using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;
    private Transform player;
    public int attackDamage = 10;
    private bool isAttacking = false;
    public int health = 100;
    public float knockbackForce = 5f; // Add this variable

    private Rigidbody2D rb;
    public Image healthBarImage;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Dash());
    }

    void Update()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        // Debug.Log(direction);
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
        // if player is on the left flip the sprite
        if (player.position.x < transform.position.x)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (player.position.x > transform.position.x)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }

    }
    public IEnumerator Dash()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            //dash
            if (player != null)
            {
                Vector2 dashDirection = (player.position - transform.position).normalized;
                rb.AddForce(dashDirection * speed * 10f, ForceMode2D.Impulse);
                yield return new WaitForSeconds(0.5f);
                rb.linearVelocity = Vector2.zero;
            }
        }

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            health -= 10;
            if (healthBarImage != null)
            {
                healthBarImage.fillAmount = (float)health / 100f;
            }
            if (health <= 0)
            {
                Destroy(gameObject);
            }

            // Knockback logic
            _ = KnockBack();
            Destroy(other.gameObject);
        }
    }
    public async Task KnockBack()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            Vector2 knockbackDir = (transform.position - player.position).normalized;
            rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
            await Task.Delay(500); // Wait for 0.5 seconds
            rb.linearVelocity = Vector2.zero; // Stop the enemy's movement
        }   
    }
}
