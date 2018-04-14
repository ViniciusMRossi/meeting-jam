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
    public AudioClip choosing;
    public AudioClip selected;

    private AudioSource audioSource;
    private int currentCharacter;
    private GameState gameState;

    void Start ()
    { 
        gameState = GameState.Instance;
        audioSource = GetComponent<AudioSource>();
        currentCharacter = 0;
        for (int i = 0; i < charactersSpriteRenderer.Length; i++)
        {            
            if (gameState.IsCharacterDead(i))
            {
                charactersSpriteRenderer[i].color = deadColor;
                charactersAnimator[i].SetBool("Die", true);
            }
            else
            {
                charactersAnimator[i].SetBool("SelectionOff", true);
            }
        }

        if (gameState.IsCharacterDead(currentCharacter))
        {
            SetCharIndex(1);            
        }

        SelectCurrentCharacter();
        audioSource.PlayOneShot(choosing);
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
        else if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetButtonDown("Fire1"))
        {
            gameState.SetCurrentSelectedCharacter(currentCharacter);
            SceneManager.LoadSceneAsync("Level" + gameState.currentLevel);
            audioSource.PlayOneShot(selected);
        }
    }

    void SetCharProperties(int addNumber)
    {
        UnselectCurrentCharacter();
        SetCharIndex(addNumber);
        SelectCurrentCharacter();
        audioSource.PlayOneShot(choosing);
    }

    void SelectCurrentCharacter()    {
        
        charactersSpriteRenderer[currentCharacter].color = selectedColor;
        charactersAnimator[currentCharacter].SetBool("WalkDown", true);
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
