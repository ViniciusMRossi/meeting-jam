using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    public SpriteRenderer[] characters;    

    public Color selectedColor;
    public Color unselectedColor;

    private int currentCharacter;

    void Start () {
        currentCharacter = 0;
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            characters[currentCharacter].color = unselectedColor;
            setNextCharIndex();
            characters[currentCharacter].color = selectedColor;
        }else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            characters[currentCharacter].color = unselectedColor;
            setPrevCharIndex();
            characters[currentCharacter].color = selectedColor;
        }
    }

    void setNextCharIndex()
    {
        if (currentCharacter >= 2)
            currentCharacter = 0;
        else
            ++currentCharacter;
    }

    void setPrevCharIndex()
    {
        if (currentCharacter <= 0)
            currentCharacter = 2;
        else
            --currentCharacter;
    }
}
