using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    public Image cooldownImage;

    void Start()
    {
        SetHandActive(true);
        handActive = true;
    }
    void Update()
    {
        if(PlayerMovement.Instance.isMovementLocked || PlayerMovement.Instance.isMovementLocked2|| PlayerMovement.Instance.isDead) return;
        // Przełączanie stanu przy prawym przycisku myszy
        // if (Input.GetMouseButtonDown(1))
        // {
        //     handActive = !handActive;

        //     // Włącz/wyłącz widoczność sprite'ów
        //     SetHandActive(handActive);
        // }

        // Jeśli ręka aktywna, podążaj za myszką
        if (handActive)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0f; 
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
            handTarget.position = new Vector3(worldPos.x, worldPos.y, handTarget.position.z);
            if(Input.GetMouseButtonDown(0))
            {
                if (!isCasting)
                {
                    StartCoroutine(CastFireball());
                }
            }
        }
    }
    public void SetHandActive(bool isActive)
    {
        Debug.Log("sss");
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
        cooldownImage.fillAmount = 0f;
        while (cooldownImage.fillAmount < 1f)
        {
            cooldownImage.fillAmount += Time.deltaTime / 1f;
            yield return null;
        }
        isCasting = false;
    }
}
