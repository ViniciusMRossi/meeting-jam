using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public SpriteRenderer[] charactersSpriteRenderer;
    public Animator[] charactersAnimator;
    public Color selectedColor;
    public Color unselectedColor;
    public Color deadColor;

    private int currentCharacter = 0;
    private GameState gameState;

    void Start ()
    { 
        gameState = GameState.Instance;

        if (gameState.IsCharacterDead(currentCharacter))
        {
            SetCharIndex(1);
        }

        SelectCurrentCharacter();

        for(int i = 0; i < charactersSpriteRenderer.Length; i++)
        {
            if (gameState.IsCharacterDead(i))
            {
                charactersSpriteRenderer[i].color = deadColor;
                charactersAnimator[i].SetBool("Death", true);
            }
        }
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetCharProperties(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetCharProperties(-1);
        }
        else if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            gameState.SetCurrentSelectedCharacter(currentCharacter);
            SceneManager.LoadSceneAsync("Level1");
        }
    }

    void SetCharProperties(int addNumber)
    {
        UnselectCurrentCharacter();
        SetCharIndex(addNumber);
        SelectCurrentCharacter();        
    }

    void SelectCurrentCharacter()
    {
        charactersSpriteRenderer[currentCharacter].color = selectedColor;
        charactersAnimator[currentCharacter].GetComponent<Animator>().SetBool("WalkDown", true);
    }

    void UnselectCurrentCharacter()
    {
        charactersSpriteRenderer[currentCharacter].color = unselectedColor;
        charactersAnimator[currentCharacter].SetBool("SelectionOff", true);
    }

    void SetCharIndex(int addNumber)
    {
        currentCharacter += addNumber;

        if (currentCharacter >= charactersAnimator.Length)
            currentCharacter = 0;
        else if (currentCharacter < 0)
            currentCharacter = charactersAnimator.Length - 1;

        if(gameState.IsCharacterDead(currentCharacter))
            SetCharIndex(addNumber);
    }    
}
