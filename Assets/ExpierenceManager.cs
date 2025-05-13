using UnityEngine;

public class ExpierenceManager : MonoBehaviour
{
    public static event System.Action<int> OnExpierenceGained;
    public static event System.Action<int> OnLevelUp;

    public int CurrentExpierence { get; private set; }
    public int Level { get; private set; } = 1;

    public int ExpierenceToLevelUp { get; private set; } = 100;

    public void AddExpierence(int amount)
    {
        CurrentExpierence += amount;
        OnExpierenceGained?.Invoke(CurrentExpierence);

        if (CurrentExpierence >= ExpierenceToLevelUp)
            LevelUp();
    }

    private void LevelUp()
    {
        CurrentExpierence -= ExpierenceToLevelUp;
        Level++;
        ExpierenceToLevelUp = Mathf.RoundToInt(ExpierenceToLevelUp * 1.5f); // Increase the amount of expierence needed for the next level

        OnLevelUp?.Invoke(Level);
    }
}
