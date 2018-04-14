using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    public GameObject[] characters;
    public GameObject[] enemyPrefabs;
    public Transform[] enemySpawnPoints;
    public Transform deadWarriorSpawnPoint;
    public Transform deadMageSpawnPoint;
    public Transform spawnPoint;
    public Transform winPoint;
    public GameObject deadWarrior;
    public GameObject deadMage;

    public int currentLevel;

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
        if (gameState.IsCharacterDead(0)) // warrior
        {
            Instantiate(deadWarrior, deadWarriorSpawnPoint.position, Quaternion.identity);
        }
        if (gameState.IsCharacterDead(1)) // mage
        {
            Instantiate(deadMage, deadMageSpawnPoint.position, Quaternion.identity);
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(currentCharacter.transform.position, winPoint.position) < 1f)
        {
            if (currentLevel < 3)
            {
                gameState.currentLevel = currentLevel + 1;
                SceneManager.LoadSceneAsync("PlayerSelect");
            } else
            {
				if (currentCharacter.GetComponent<PlayerBehaviour>().charType == 2) {
					SceneManager.LoadSceneAsync ("Finish");
				} else {
					SceneManager.LoadSceneAsync("GameOver");
				}
                
            }
        }
	}
}
