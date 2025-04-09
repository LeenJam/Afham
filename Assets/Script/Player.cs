using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator; 
    private StarterAssetsInputs starterAssetsInputs;
    private float timeShot = -1f;
    public const int ANIMATION_LAYER_SHOOT = 1;
    public Ball ballAttachedToPlayer;
    public Ball BallAttachedToPlayer { get => ballAttachedToPlayer; set => ballAttachedToPlayer = value;}
    public Rigidbody rigidbody;
    
    // Start is called before the first frame update
    void Start()
    { 
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();

        
      
    
    }

    // Update is called once per frame
    void Update()
    {
      
     if (starterAssetsInputs.shoot)
     {
         starterAssetsInputs.shoot = false;
         timeShot = Time.time;
         animator.Play("Shoot", ANIMATION_LAYER_SHOOT, 0f);
         animator.SetLayerWeight(ANIMATION_LAYER_SHOOT, 1f);
     }

      if (timeShot>0)
      {
        //shoot ball
        Ball ballAttachedToPlayer = GetComponent<Ball>();

        if (ballAttachedToPlayer != null && Time.time - timeShot > 0.2)
        {
            ballAttachedToPlayer.StickToPlayer = false;
            Rigidbody rigidbody = ballAttachedToPlayer.transform.gameObject.GetComponent<Rigidbody>();
            rigidbody.AddForce(transform.forward * 300f , ForceMode.Impulse);
            ballAttachedToPlayer = null;

        }
        
        //finish kicking animation
        if (Time.time - timeShot> 0.5)
        {
            timeShot = -1f;
        }
        
      }
      else 
      {
        animator.SetLayerWeight(ANIMATION_LAYER_SHOOT , Mathf.Lerp(animator.GetLayerWeight(ANIMATION_LAYER_SHOOT), 0f, Time.deltaTime * 10f));
      }

    }

     
    

    
}
