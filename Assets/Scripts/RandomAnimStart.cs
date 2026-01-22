using UnityEngine;

public class RandomAnimStart : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Animator anim;
    public string animName = "Idle";

    void Awake()
    {
        anim = GetComponent<Animator>();

        // losowy moment animacji
        anim.Play(animName, 0, Random.value);
    }
}
