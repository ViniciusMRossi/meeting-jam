public class GameState {

    private static GameState instance;
    private bool[] deadCharacters = { false, false, false, true };
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

    public void setCurrentSelectedCharacter(int character)
    {
        currentSelectedCharacter = character;
    }

    public int getCurrentSelectedCharacter()
    {
        return currentSelectedCharacter;
    }

    public bool isCharacterDead(int index)
    {
        return deadCharacters[index];
    }

    public void setCharacterDead(int index)
    {
        deadCharacters[index] = true;
    }

    public int getCurrentPoints()
    {
        return currentPoints;
    }

    public void addPoints(int points)
    {
        currentPoints += points;
    }
}
