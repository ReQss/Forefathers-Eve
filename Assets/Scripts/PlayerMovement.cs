using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }
    public float speed = 4f;
    private Rigidbody2D rb;
    private Vector2 input;
    private SpriteRenderer spriteRenderer;
    private Interactable currentInteractable = null;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // ruch
        input = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;

        // flip
        if (input.x < 0) spriteRenderer.flipX = false;
        else if (input.x > 0) spriteRenderer.flipX = true;

        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.Interaction();
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
