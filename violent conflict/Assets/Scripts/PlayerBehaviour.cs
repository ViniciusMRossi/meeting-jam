using System;
using System.Collections;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public AudioClip StepSound;
    public AudioSource StepAudioSource;
    public AudioSource VoiceAudioSource;
    public float speed = 1.0f;

    public bool isAlive = true;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        if (horizontal == 0 && vertical == 0)
        {
            return;
        }
        Vector3 direction = new Vector3(horizontal, vertical, 0);
        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
        {
            if (horizontal > 0)
            {
                animator.SetTrigger("walkRight");
            }
            else
            {
                animator.SetTrigger("walkLeft");
            }
        }
        else
        {
            if (vertical > 0)
            {
                animator.SetTrigger("walkUp");
            }
            else
            {
                animator.SetTrigger("walkDown");
            }
        }

        transform.position += direction * speed;
        StepAudioSource.PlayOneShot(StepSound);
    }

    public void Die()
    {
        isAlive = false;
        animator.SetTrigger("die");
    }
}