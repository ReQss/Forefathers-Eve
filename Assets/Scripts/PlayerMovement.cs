using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }
    public float speed = 4f;
    private Rigidbody2D rb;
    private Vector2 input;
    // private SpriteRenderer spriteRenderer;
    private Interactable currentInteractable = null;
    private Animator animator; // Dodaj animator

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // Pobierz animator
    }

    void Update()
    {
        // ruch
        input = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;

        // flip
        // flip rigowanej postaci
        if (input.x < 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (input.x > 0)
            transform.localScale = new Vector3(-1, 1, 1);


        // ustaw animację "Running" gdy idziemy w bok lub w górę
        if (animator != null)
            animator.SetBool("Running", input.magnitude > 0);

        if (Input.GetKeyDown(KeyCode.E) )
        {
            animator.SetTrigger("Attack"); // uruchom animację "Attack"
            // currentInteractable.Interaction();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + input * speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
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
