using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    public AudioClip StepSound;
    public AudioClip AttackSound;
    public AudioClip DeathSound;
    public AudioSource StepAudioSource;
    public AudioSource VoiceAudioSource;
    public float speed = 1.0f;
	public Animator animator;
	public int charType;
	public LayerMask layerMask;

    public bool isAlive = true;

    private bool playStep = false;

    private void Start()
    {
        StartCoroutine(PlayStepSounds());
    }

    private void Update()
    {
        if (!isAlive)
        {
            return;
        }
        HandleMovement();
		if (Input.GetButton ("Fire1")) {
			animator.SetTrigger ("Attack");
			VoiceAudioSource.PlayOneShot (AttackSound);


			RaycastHit2D hit;
			if (hit = Physics2D.CircleCast (transform.position, 0.2f, Vector2.right, 0.2f, layerMask)) {
				if (hit.transform.name.Contains ("Orc") || hit.transform.name.Contains ("Skeleton")
					|| hit.transform.name.Contains ("EnemyMage") || hit.transform.name.Contains ("EnemyWarrior")) {
					hit.transform.GetComponent<EnemyBehaviour> ().Die ();
				}

			}
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
		if (!isAlive)
			return;
		
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
        animator.SetBool("Die", true);
        VoiceAudioSource.PlayOneShot(DeathSound);
        StopAllCoroutines();
        var enemies = FindObjectsOfType<EnemyBehaviour>();
        StartCoroutine(DieCoroutine());
        foreach (var enemy in enemies)
        {
            enemy.OnPlayerDead();
        }

        GameState.Instance.SetCharacterDead(charType);
    }

    private IEnumerator DieCoroutine()
    {        
        yield return new WaitForSeconds(1f);
        
        SceneManager.LoadSceneAsync("GameOver");
    }
}