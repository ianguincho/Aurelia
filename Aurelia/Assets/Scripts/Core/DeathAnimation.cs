using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    public Transform player;
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if(player.position.y < -1f)
        {
            DeathFromFall();
        }
    }

    public void DeathFromFall()
    {
        animator.SetTrigger("FadeOut");
    }
}
