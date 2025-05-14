using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpierenceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI expierenceText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Image bar;
    [SerializeField] private Image barBackground;

    [SerializeField] private ExpierenceManager expierenceManager;

    [SerializeField] private float barAnimationDuration = .5f; // Duration in seconds
    [SerializeField] private float barBackAnimationDuration = .2f; // Duration in seconds

    private int barTweenId = -1;
    private int barBackTweenId = -1;

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

        float targetFill = (float)currentExpierence / expierenceManager.ExpierenceToLevelUp;
        if (barTweenId != -1)
            LeanTween.cancel(barTweenId);

        if (barBackTweenId != -1)
            LeanTween.cancel(barBackTweenId);

        barTweenId = LeanTween.value(bar.gameObject, bar.fillAmount, targetFill, barAnimationDuration)
            .setOnUpdate((float val) =>
            {
                bar.fillAmount = val;
            }).setEase(LeanTweenType.easeOutCubic).id;

        barBackTweenId = LeanTween.value(barBackground.gameObject, barBackground.fillAmount, targetFill, barBackAnimationDuration)
            .setOnUpdate((float val) =>
            {
                barBackground.fillAmount = val;
            }).setEase(LeanTweenType.easeOutCubic).id;
    }
}
