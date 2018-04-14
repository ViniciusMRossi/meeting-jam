using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public SpriteRenderer[] characters;  
    public Color selectedColor;
    public Color unselectedColor;
    public Color deadColor;

    private int currentCharacter;
    private GameState gameState;

    void Start () {
        gameState = GameState.Instance;
        for(int i = 0; i < characters.Length; i++)
        {
            if (gameState.isCharacterDead(i))
            {
                characters[i].color = deadColor;
            }
        }
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            characters[currentCharacter].color = unselectedColor;
            setNextCharIndex();
            characters[currentCharacter].color = selectedColor;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            characters[currentCharacter].color = unselectedColor;
            setPrevCharIndex();
            characters[currentCharacter].color = selectedColor;
        }
        else if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            gameState.setCurrentSelectedCharacter(currentCharacter);
            SceneManager.LoadSceneAsync("Level1");
        }
    }

    void setNextCharIndex()
    {
        if (currentCharacter >= 2)
            currentCharacter = 0;
        else
            ++currentCharacter;

        if (gameState.isCharacterDead(currentCharacter))
            setNextCharIndex();
    }

    void setPrevCharIndex()
    {
        if (currentCharacter <= 0)
            currentCharacter = 2;
        else
            --currentCharacter;

        if (gameState.isCharacterDead(currentCharacter))
            setPrevCharIndex();
    }
}
