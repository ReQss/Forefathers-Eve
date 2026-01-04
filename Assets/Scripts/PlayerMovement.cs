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
    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); 
    }

    public void StopMovement()
    {
        input = Vector2.zero;
        if (rb != null)
            rb.linearVelocity = Vector2.zero;
    }
    void Update()
    {
        if(isMovementLocked) return;
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
        PlayerDirection();

        if (animator != null)
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
    public void ChangePlayerDirection()
    {
        if (additionalHand != null && additionalHand.handActive)
        {
            if (additionalHand.handTarget.position.x < transform.position.x){
                flipX = false;
            }
            else if (additionalHand.handTarget.position.x > transform.position.x){
                flipX = true;
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
        if(flipX){
            transform.localScale = new Vector3(-1, 1, 1);
        } else {
            transform.localScale = new Vector3(1, 1, 1);
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
        if(isMovementLocked) return;
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
