using System.Threading.Tasks;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }
    public float speed = 4f;
    private Rigidbody2D rb;
    private Vector2 input;
    // private SpriteRenderer spriteRenderer;
    private Interactable currentInteractable = null;
    public Animator animator; // Dodaj animator
    public GameObject lantern;
    public GameObject rake;
    private bool isAttacking = true;
    public bool isMovementLocked = false;
    public AudioSource walkAudioSource;
    public int maxHealth = 100;
    public int currentHealth = 100;
    public HandFollowCursor additionalHand;
    public bool flipX = false;
    public bool isMovementLocked2 = false;
    public float knockbackForce = 5f;
    public GameObject canvas;
    public GameObject deadPanel;
    public bool isDead = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        
        maxHealth = GameManager.Instance.health;
        rb = GetComponent<Rigidbody2D>();
        // spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); 
        if(GameManager.Instance.questVariables.isEverythingAchieved == false)
        {
            currentHealth = maxHealth/2;
        }
    }

    public void StopMovement()
    {
        input = Vector2.zero;
        if (rb != null)
            rb.linearVelocity = Vector2.zero;
    }
    void Update()
    {
        if(isMovementLocked || isMovementLocked2 || isDead){ 
            animator.SetBool("Running", false);
            
            return;
        }
         if (Input.GetKeyDown(KeyCode.E) && animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Pray")
        {
            if (currentInteractable != null )
            {
                animator.SetTrigger("Interact");
                
                StopMovement();
                currentInteractable.Interaction();
            }
            else
            {
                animator.SetTrigger("Attack");
            }
        }
        // ruch
        input = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;

        // flip
        // flip rigowanej postaci
        ChangePlayerDirection();
        // PlayerDirection();

        if (animator != null && isMovementLocked == false && isMovementLocked2 == false)
            animator.SetBool("Running", input.magnitude > 0);

        // Footsteps sound
        if (walkAudioSource != null)
        {
            if (input.magnitude > 0)
            {
                if (!walkAudioSource.isPlaying)
                    walkAudioSource.Play();
            }
            else
            {
                if (walkAudioSource.isPlaying)
                    walkAudioSource.Stop();
            }
        }

       
    }
    public void LateUpdate()
    {
        
        PlayerDirection();
    }
    public void ChangePlayerDirection()
    {
        if (additionalHand != null && additionalHand.handActive)
        {
            if (additionalHand.handTarget.position.x < transform.position.x){
                flipX = false;
                
            Debug.Log("x");
            }
            else{
                flipX = true;
                
             Debug.Log("y");
            }
        }
        else
        {
            if (input.x < 0){
                flipX = false;
            }
            else if (input.x > 0){
                flipX = true;
            }
        }
    }
    public void PlayerDirection()
    {
        // if(additionalHand.handActive)return ;
        if(flipX){
            transform.localScale = new Vector3(-1, 1, 1);
            if(canvas != null)
                canvas.transform.localScale = new Vector3(-1, 1, 1);
        } else {
            transform.localScale = new Vector3(1, 1, 1);
            if(canvas != null)
                canvas.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void SetLanternState(bool state)
    {
        if(lantern != null)
        lantern.SetActive(state);
    }
    public void SetRakeState(bool state)
    {
        if(rake != null)
        rake.SetActive(state);
    }

    void FixedUpdate()
    {
        if (isMovementLocked || isMovementLocked2) return;

        rb.MovePosition(rb.position + input * speed * Time.fixedDeltaTime);
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            currentInteractable = other.GetComponent<Interactable>();
            UIHandler.Instance.ShowPlayerTip(currentInteractable.interactionTip);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Enemy"))
    {
        // obrażenia
        _ = GetDamage(other);

    }
}
    private bool lockDamage = false;

    public async Task GetDamage(Collider2D other)
    {
        if (lockDamage) return;
        lockDamage = true;

        isMovementLocked = true;
        // obrażenia
        int damage = 10;
        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);

        UIHandler.Instance.UpdateHealthBar();
        if (currentHealth <= 0)
        {
            // dead
            // Debug.Log("Player Dead");
            isMovementLocked = true;
            lockDamage = true;
            isDead = true;
            deadPanel.SetActive(true);
            deadPanel.GetComponent<Animator>().SetTrigger("trigger");
            animator.SetTrigger("Dead");
            return;
        }
        // knockback

        _ = KnockBack(other);
        // czas knockbacku
        await Task.Delay(200);

        rb.linearVelocity = Vector2.zero;
        isMovementLocked = false;
        Debug.Log("Player Health: " + currentHealth);
        // i-frames
        await Task.Delay(800);
        lockDamage = false;
    }

    public async Task KnockBack(Collider2D other)
    {
        if (rb != null)
        {
            Vector2 knockbackDir = (transform.position - other.transform.position).normalized;
            rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
            await Task.Delay(500); // Wait for 0.5 seconds
            rb.linearVelocity = Vector2.zero; // Stop the enemy's movement
        }   
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            if (currentInteractable != null)
                UIHandler.Instance.HidePlayerTip();

            currentInteractable = null;
        }
    }
}
