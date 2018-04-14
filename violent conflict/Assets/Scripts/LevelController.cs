using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    public GameObject[] characters;
    public GameObject[] enemyPrefabs;
    public Transform[] enemySpawnPoints;
    public Transform spawnPoint;
    public Transform winPoint;

    private GameState gameState;
    private GameObject currentCharacter;

	// Use this for initialization
	void Start () {
        gameState = GameState.Instance;
        currentCharacter = Instantiate(characters[gameState.GetCurrentSelectedCharacter()], spawnPoint.position, Quaternion.identity);
        for(int i = 0; i < enemySpawnPoints.Length; i++)
        {
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyIndex], enemySpawnPoints[i].position, Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(currentCharacter.transform.position, winPoint.position) < 1f)
        {
            SceneManager.LoadSceneAsync("PlayerSelect");
        }
	}
}
