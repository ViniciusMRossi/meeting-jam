using System;
using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
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

    public void Die()
    {
        isAlive = false;
        animator.SetTrigger("die");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(FollowPlayer(other.transform));
            StartCoroutine(PlayStepSounds());
        }
    }

    private IEnumerator PlayStepSounds()
    {
        while (true)
        {
            yield return new WaitForSeconds(.5f);
            StepAudioSource.PlayOneShot(StepSound);
        }
    }

    private IEnumerator FollowPlayer(Transform target)
    {
        while (Vector3.Distance(target.position, transform.position) > 0)
        {
            transform.LookAt(target);
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
            yield return null;
        }
    }
}