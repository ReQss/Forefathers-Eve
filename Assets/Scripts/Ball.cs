using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody2D rb;
    private HandFollowCursor additionalHand;
    private Transform target;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        additionalHand = Object.FindFirstObjectByType<HandFollowCursor>();
        if (additionalHand != null)
            target = additionalHand.handTarget;
    }

    void Start()
    {
        if (target == null) return;
        // kierunek do kursora
        Vector2 direction = (target.position - transform.position).normalized;

        // ruch pocisku
        rb.linearVelocity = direction * speed;

        // opcjonalnie: obrót w stronę lotu
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        //  bool flipX = PlayerMovement.Instance.flipX;
        // if (flipX)
        // {
        //     transform.localScale = new Vector3(-1, 1, 1);
        // }
        // else
        // {
        //     transform.localScale = new Vector3(1, 1, 1);
        // }
    }
}
