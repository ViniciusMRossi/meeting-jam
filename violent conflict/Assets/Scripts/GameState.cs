﻿public class GameState {

    private static GameState instance;
    private bool[] deadCharacters = { false, true, false, true };
    private int currentPoints;      
    private int currentSelectedCharacter;

    private GameState()
    {
        currentPoints = 0;
    }

    public static GameState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameState();
            }
            return instance;
        }
    }

    public void SetCurrentSelectedCharacter(int character)
    {
        currentSelectedCharacter = character;
    }

    public int GetCurrentSelectedCharacter()
    {
        return currentSelectedCharacter;
    }

    public bool IsCharacterDead(int index)
    {
        return deadCharacters[index];
    }

    public void SetCharacterDead(int index)
    {
        deadCharacters[index] = true;
    }

    public int GetCurrentPoints()
    {
        return currentPoints;
    }

    public void AddPoints(int points)
    {
        currentPoints += points;
    }
}
