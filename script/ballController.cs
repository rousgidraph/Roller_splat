using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 15;
    private bool isTraveling;
    private Vector3 travelDirection;
    private Vector3 nextCollisionPosition;

    public int minSwipeRegognition = 500;
    private Vector2 swipePosLastFrame;
    private Vector2 swipePosCurrentFrame;
    private Vector2 currentSwipe;
    public ParticleSystem dust;
    private Color solveColor;
   


    private AudioSource player;


    private void Start()
    {
        solveColor = Random.ColorHSV(0.5f, 1f);
        GetComponent<MeshRenderer>().material.color = solveColor;     
        
        
        //settings.playOnAwake = false;

    }

    private void FixedUpdate() {
        var main = dust.main;
        var colorModule = dust.colorOverLifetime;
        

        colorModule.color = Color.red;
        if (isTraveling) { 
          rb.velocity = speed * travelDirection;
            dust.Play();
        }

        Collider[] hitcolliders = Physics.OverlapSphere(transform.position - (Vector3.up / 2), 0.05f);
        int i = 0;
        while (i < hitcolliders.Length) {
            ground_piece ground = hitcolliders[i].transform.GetComponent<ground_piece>();
            if (ground && !ground.is_coloured) {
                ground.ChangeColour(solveColor);
            }
            i++;  
        }

        if (nextCollisionPosition != Vector3.zero){
            if (Vector3.Distance(transform.position, nextCollisionPosition) < 1) {
                isTraveling = false;
                travelDirection = Vector3.zero;
                nextCollisionPosition = Vector3.zero;
            
            }
        }

        if (isTraveling)
        {

            return;
        }
        else {
            dust.Play();
        }
        


        if (Input.GetMouseButton(0)) {
            swipePosCurrentFrame = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            if (swipePosLastFrame != Vector2.zero) {
                currentSwipe = swipePosCurrentFrame - swipePosLastFrame;

                if (currentSwipe.sqrMagnitude < minSwipeRegognition)
                {
                    return;
                }

                currentSwipe.Normalize();

                //up/dpwn 
                if (currentSwipe.x > -0.5 && currentSwipe.x < 0.5) {
                 

                    setDestination(currentSwipe.y > 0 ? Vector3.forward : Vector3.back);
                }

                //left / right
                if (currentSwipe.y > -0.5 && currentSwipe.y < 0.5)
                {
                    

                    setDestination(currentSwipe.x > 0 ? Vector3.right : Vector3.left);
                }

            }

            swipePosLastFrame = swipePosCurrentFrame;
        }

        if (Input.GetMouseButtonUp(0)) {
            swipePosLastFrame = Vector2.zero;
            currentSwipe = Vector2.zero;
        }

    }

    private void setDestination(Vector3 direction) {

        travelDirection = direction;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, 100f)) {
            nextCollisionPosition = hit.point;
        }

        isTraveling = true;


    }

}
