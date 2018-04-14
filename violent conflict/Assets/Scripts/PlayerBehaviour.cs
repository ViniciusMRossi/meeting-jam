using System.Collections;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public AudioClip StepSound;
    public AudioClip AttackSound;
    public AudioClip DeathSound;
    public AudioSource StepAudioSource;
    public AudioSource VoiceAudioSource;
    public float speed = 1.0f;

    public bool isAlive = true;

    private Animator animator;
    private bool playStep = false;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        StartCoroutine(PlayStepSounds());
    }

    private void Update()
    {
        if (!isAlive)
        {
            return;
        }
        HandleMovement();
        if (Input.GetButton("Fire1"))
        {
            animator.SetTrigger("Attack");
            VoiceAudioSource.PlayOneShot(AttackSound);
        }
    }

    private IEnumerator PlayStepSounds()
    {
        while (true)
        {
            yield return new WaitForSeconds(.2f);
            if (playStep)
            {
                StepAudioSource.PlayOneShot(StepSound);
            }
        }
    }

    private void HandleMovement()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        if (horizontal == 0 && vertical == 0)
        {
            playStep = false;
            return;
        }
        Vector3 direction = new Vector3(horizontal, vertical, 0);
        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
        {
            if (horizontal > 0)
            {
                animator.SetTrigger("WalkRight");
            }
            else
            {
                animator.SetTrigger("WalkLeft");
            }
        }
        else
        {
            if (vertical > 0)
            {
                animator.SetTrigger("WalkUp");
            }
            else
            {
                animator.SetTrigger("WalkDown");
            }
        }

        transform.position += direction * speed;
        playStep = true;
    }

    public void Die()
    {
        isAlive = false;
        animator.SetTrigger("Die");
        VoiceAudioSource.PlayOneShot(DeathSound);
        StopAllCoroutines();
        var enemies = FindObjectsOfType<EnemyBehaviour>();
        foreach(var enemy in enemies) {
            enemy.OnPlayerDead();
        }
    }
}