using UnityEngine;

public class BindObjectPosition : MonoBehaviour
{
    public GameObject targetObject;
    public Vector3 offset = Vector3.zero;
    public bool hasBorders = false;

    // Granice kamery
    public float minX, maxX, minY, maxY;

    void LateUpdate()
    {
        if (targetObject != null)
        {
            // Wyliczamy docelową pozycję
            float targetX = targetObject.transform.position.x + offset.x;
            float targetY = targetObject.transform.position.y + offset.y;
            float targetZ = transform.position.z + offset.z; // Z zostaje bez zmian
            if(!hasBorders){
                // Przypisujemy pozycję do kamery bez ograniczeń
                transform.position = new Vector3(targetX, targetY, targetZ);
                return;
            }
            // Ograniczamy pozycję do granic mapy
            float clampedX = Mathf.Clamp(targetX, minX, maxX);
            float clampedY = Mathf.Clamp(targetY, minY, maxY);

            // Przypisujemy ograniczoną pozycję do kamery
            transform.position = new Vector3(clampedX, clampedY, targetZ);
        }
    }
}
