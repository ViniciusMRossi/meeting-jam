using System;
using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public AudioClip StepSound;
	public AudioClip HitSound;
	public AudioClip EnemyHitSound;
    public AudioClip ChaseSound;
    public AudioSource StepAudioSource;
    public AudioSource VoiceAudioSource;
    public float speed = 1.0f;
	public Animator animator;

    public bool isAlive = true;

    private void Start()
    {
    }

    public void Die()
    {
		if (!isAlive)
			return;
		
		Debug.Log ("Die mothafucka");
        isAlive = false;
		animator.SetBool("Die", true);
		StopAllCoroutines ();
		var coliders = GetComponentsInChildren<BoxCollider2D> ();
		VoiceAudioSource.PlayOneShot(EnemyHitSound);

		foreach (var colider in coliders) {
			colider.enabled = false;
		}

        GameState.Instance.OnViolenceOcurred();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
		if (!isAlive)
			return;
		
        if (other.tag == "Player")
        {
            animator = GetComponentInChildren<Animator>();
            StopAllCoroutines();
            StartCoroutine(FollowPlayer(other.transform));
            StartCoroutine(PlayStepSounds());
            VoiceAudioSource.PlayOneShot(ChaseSound);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
		if (!isAlive)
			return;
		
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponentInChildren<PlayerBehaviour>().Die();
            VoiceAudioSource.PlayOneShot(HitSound);
        }

		if (coll.gameObject.tag == "Attack")
		{
			Die ();
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
            Vector3 direction = target.position - transform.position;
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0)
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
                if (direction.y > 0)
                {
                    animator.SetTrigger("WalkUp");
                }
                else
                {
                    animator.SetTrigger("WalkDown");
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
            yield return new WaitForFixedUpdate();
        }
    }

    internal void OnPlayerDead()
    {
		if (!isAlive)
			return;
		
        StopAllCoroutines();
    }
}