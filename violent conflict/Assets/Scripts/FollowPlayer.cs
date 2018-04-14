using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    Transform playerTransform;
	
	void LateUpdate () {
        if (playerTransform == null)
        {
            playerTransform = FindObjectOfType<PlayerBehaviour>().transform;
        }
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
	}
}
