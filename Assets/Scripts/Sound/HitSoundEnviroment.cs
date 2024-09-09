using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSoundEnviroment : MonoBehaviour
{
    [SerializeField]
    private AudioClip HitSound;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var ballController = collision.gameObject.GetComponent<BallController>();
        if (ballController) 
        {
            if(Random.Range(1, 101) < 25) 
            {
                ballController.ModifyBallBounce(5f);
            }
            SoundManager.Instance.PlaySound(HitSound);
        }
    }
}
