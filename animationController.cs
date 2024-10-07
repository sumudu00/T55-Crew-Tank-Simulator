using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationController : MonoBehaviour
{
    private Animator animator;
    private bool onHitting = false;
    public enemyPath enemyPath;
    private Transform carTransform;
    public string animationName = "Standing Arguing";

    void Start()
    {
        animator = this.GetComponent<Animator>();
        // Find the car object by tag
        GameObject carObject = GameObject.FindGameObjectWithTag("Vehicle");
        if (carObject != null)
        {
            carTransform = carObject.transform;
        }
        else
        {
            Debug.LogError("no game object found");
        }
    }

    void Update()
    {
        //    PlayHitAnimation();
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {
            // Make the player face the car
            FaceCar();
        }
    }

    //public void PlayHitAnimation()
    //{
    //    if (onHitting)
    //    {
    //        animator.SetBool("isHitting", true);



    //    }

    //    else
    //    {
    //        animator.SetBool("isHitting", false);

    //    }
    //}
    void FaceCar()
    {
        if (carTransform != null)
        {
            // Get direction to the car
            Vector3 directionToCar = carTransform.position - transform.position;
            directionToCar.y = 0; // Keep it on the same horizontal plane

            // Create the rotation to face the car
            Quaternion rotation = Quaternion.LookRotation(directionToCar);
            transform.rotation = rotation;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Vehicle"))
        {
            animator.SetBool("isHitting", true);
            onHitting = true;
            enemyPath.canWalking = false;
        }
     //animator.SetBool("isHitting", false);
    }
}
