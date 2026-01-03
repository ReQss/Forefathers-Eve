using System.Collections;
using UnityEngine;

public class HandFollowCursor : MonoBehaviour
{
    public Camera mainCamera;
    public Transform handTarget;

    public SpriteRenderer originalHandRenderer;     // SpriteRenderer oryginalnej ręki
    public SpriteRenderer originalWeaponRenderer;   // SpriteRenderer oryginalnej broni
    public SpriteRenderer handGraphicsRenderer;     // SpriteRenderer ręki podążającej za myszką
    public SpriteRenderer handWeaponRenderer;    // SpriteRenderer broni podążającej za myszką

    public bool handActive = false;
    public GameObject fireBall;
    private bool isCasting = false;
    public Transform fireballSpawnPoint;

    void Start()
    {
        SetHandActive(false);
    }
    void Update()
    {
        if(PlayerMovement.Instance.isMovementLocked) return;
        // Przełączanie stanu przy prawym przycisku myszy
        if (Input.GetMouseButtonDown(1))
        {
            handActive = !handActive;

            // Włącz/wyłącz widoczność sprite'ów
            SetHandActive(handActive);
        }

        // Jeśli ręka aktywna, podążaj za myszką
        if (handActive)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0f; // ignorujemy Z w 2D
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
            handTarget.position = new Vector3(worldPos.x, worldPos.y, handTarget.position.z);
            if (!isCasting)
            {
                StartCoroutine(CastFireball());
            }
        }
    }
    public void SetHandActive(bool isActive)
    {
        handActive = isActive;

        handGraphicsRenderer.enabled = handActive;
        handWeaponRenderer.enabled = handActive;
        originalHandRenderer.enabled = !handActive;
        originalWeaponRenderer.enabled = !handActive;
    }
    public IEnumerator  CastFireball()
    {
        if (!handActive) yield return null;
        if (isCasting) yield return null;
        
        isCasting = true;
        Instantiate(fireBall, fireballSpawnPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        isCasting = false;
    }
}
