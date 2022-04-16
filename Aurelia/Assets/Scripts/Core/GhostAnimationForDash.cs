using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimationForDash : MonoBehaviour
{

    public float ghostDelay;
    private float ghostDelaySeconds;
    public GameObject ghost;
    public bool makeGhost = false;

    // Start is called before the first frame update
    void Start()
    {
        ghostDelaySeconds = ghostDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if(makeGhost == true)
        {
            if (ghostDelaySeconds > 0)
            {
                ghostDelaySeconds -= Time.deltaTime;
            }
            else
            {
                GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
                currentGhost.transform.localScale = this.transform.localScale;
                Sprite currentSprite = currentGhost.GetComponent<Sprite>();
                ghostDelaySeconds = ghostDelay;
                Destroy(currentGhost, 1f);
            }
        }
    }
}
