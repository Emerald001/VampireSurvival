using UnityEngine;

public class ExpierenceManager : MonoBehaviour
{
    public static event System.Action<int> OnExpierenceGained;
    public static event System.Action<int> OnLevelUp;

    public int CurrentExpierence { get; private set; }
    public int Level { get; private set; } = 1;

    public int ExpierenceToLevelUp { get; private set; } = 10;

    public static ExpierenceManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void SetUp()
    {
        CurrentExpierence = 0;
        Level = 1;
        ExpierenceToLevelUp = 10;

        OnExpierenceGained?.Invoke(CurrentExpierence);
    }

    public void AddExpierence(int amount)
    {
        CurrentExpierence += amount;

        if (CurrentExpierence >= ExpierenceToLevelUp)
            LevelUp();

        OnExpierenceGained?.Invoke(CurrentExpierence);
    }

    private void LevelUp()
    {
        CurrentExpierence -= ExpierenceToLevelUp;
        Level++;
        ExpierenceToLevelUp = Mathf.RoundToInt(ExpierenceToLevelUp * 1.5f); // Increase the amount of expierence needed for the next level

        EnemyManager.Instance.DoKnockback(0.5f, GameManager.Instance.Player.transform.position);

        OnLevelUp?.Invoke(Level);
    }
}
