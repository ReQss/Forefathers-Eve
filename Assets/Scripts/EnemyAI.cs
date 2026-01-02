using System.Threading.Tasks;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;
    private Transform player;
    public int attackDamage = 10;
    private bool isAttacking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }
    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            _ = AttackPlayer();
        }
    }
    public async Task AttackPlayer()
    {
        if (player == null) return;
        if(isAttacking) return;
        
            Debug.Log("XD");
        isAttacking = true;

        PlayerMovement.Instance.currentHealth -= attackDamage;
        UIHandler.Instance.UpdateHealthBar();
        await Task.Delay(1000);
        isAttacking = false;
    }
}
