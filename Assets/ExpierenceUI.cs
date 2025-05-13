using UnityEngine;

public class ExpierenceUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI expierenceText;
    [SerializeField] private TMPro.TextMeshProUGUI levelText;

    [SerializeField] private ExpierenceManager expierenceManager;

    private void OnEnable()
    {
        ExpierenceManager.OnExpierenceGained += UpdateExpierenceUI;
    }

    private void OnDisable()
    {
        ExpierenceManager.OnExpierenceGained -= UpdateExpierenceUI;
    }

    private void UpdateExpierenceUI(int currentExpierence)
    {
        expierenceText.text = $"{currentExpierence}/{expierenceManager.ExpierenceToLevelUp}";
        levelText.text = $"Lvl: {expierenceManager.Level}";
    }
}
