using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    public float speed = 5f;     // Prędkość fireballa
    public float lifetime = 5f;  // Po ilu sekundach zniszczyć

    private Vector2 direction = Vector2.up; // Domyślny kierunek, żeby nie był null
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Upewniamy się, że Rigidbody nie zostanie "przepchnięty" przez inne siły
        rb.gravityScale = 0f;
        rb.linearDamping = 0f;
    }

    // Ustawienie kierunku z zewnątrz
    public void Initialize(Vector2 dir)
    {
        if (dir != Vector2.zero)
            direction = dir.normalized;
    }

    void Start()
    {
        // Jeśli kierunek nie został ustawiony z zewnątrz, fireball leci w górę
        rb.linearVelocity = direction * speed;

        // Automatycznie niszcz po lifetime sekundach
        Destroy(gameObject, lifetime);
    }
}
