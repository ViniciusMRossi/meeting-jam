using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public GameObject[] characters;
    public Transform spawnPoint;

    private GameState gameState;
    private GameObject currentCharacter;

	// Use this for initialization
	void Start () {
        gameState = GameState.Instance;
        currentCharacter = Instantiate(characters[gameState.GetCurrentSelectedCharacter()], spawnPoint.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
