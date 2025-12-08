using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private OptionPicker optionPicker;

    [SerializeField] private RectTransform mainMenu;
    [SerializeField] private RectTransform pauseMenu;

    [SerializeField] private RectTransform gameOverScreen;
    [SerializeField] private RectTransform settingsScreen;
    [SerializeField] private PlayerAndWeaponPicker playerAndWeaponPicker;

    [SerializeField] private RectTransform topUI;
    [SerializeField] private Image blurOverlay;

    public static GUIManager Instance;

    private Vector2 mainMenuShowPos;
    private Vector2 topUIShowPos;

    private Vector2 playerAndWeaponPickerShowPos;
    private Vector2 playerAndWeaponPickerOffscreenRight;
    private Vector2 playerAndWeaponPickerOffscreenLeft;

    private Vector2 settingsScreenShowPos;
    private Vector2 settingsScreenOffscreenRight;
    private Vector2 settingsScreenOffscreenLeft;

    private void Awake()
    {
        Instance = this;
        if (blurOverlay != null)
            blurOverlay.gameObject.SetActive(false);

        mainMenuShowPos = mainMenu.anchoredPosition;
        topUIShowPos = topUI.anchoredPosition;

        // Calculate offscreen positions for playerAndWeaponPicker
        playerAndWeaponPickerShowPos = playerAndWeaponPicker.RectTransform.anchoredPosition;
        playerAndWeaponPickerOffscreenRight = playerAndWeaponPickerShowPos + new Vector2(playerAndWeaponPicker.RectTransform.rect.width, 0);
        playerAndWeaponPickerOffscreenLeft = playerAndWeaponPickerShowPos + new Vector2(-playerAndWeaponPicker.RectTransform.rect.width, 0);

        // Calculate offscreen positions for settingsScreen
        settingsScreenShowPos = settingsScreen.anchoredPosition;
        settingsScreenOffscreenRight = settingsScreenShowPos + new Vector2(settingsScreen.rect.width, 0);
        settingsScreenOffscreenLeft = settingsScreenShowPos + new Vector2(-settingsScreen.rect.width, 0);
    }

    public void ShowMainMenu(bool show)
    {
        float duration = 0.4f;
        LeanTween.cancel(mainMenu);

        Vector2 offscreenLeft = mainMenuShowPos + new Vector2(-mainMenu.rect.width, 0);
        Vector2 offscreenRight = mainMenuShowPos + new Vector2(mainMenu.rect.width, 0);

        if (show)
        {
            mainMenu.gameObject.SetActive(true);
            mainMenu.anchoredPosition = offscreenLeft;
            LeanTween.move(mainMenu, mainMenuShowPos, duration)
                .setEase(LeanTweenType.easeOutCubic);
        }
        else
        {
            LeanTween.move(mainMenu, offscreenRight, duration)
                .setEase(LeanTweenType.easeInCubic)
                .setOnComplete(() => mainMenu.gameObject.SetActive(false));
        }
    }

    public void ShowTopUI(bool show)
    {
        float duration = 1;
        LeanTween.cancel(topUI);

        Vector2 offscreenAbove = topUIShowPos + new Vector2(0, topUI.rect.height + 50);

        if (show)
        {
            topUI.gameObject.SetActive(true);
            topUI.anchoredPosition = offscreenAbove;
            LeanTween.move(topUI, topUIShowPos, duration)
                .setEase(LeanTweenType.easeOutBack);
        }
        else
        {
            LeanTween.move(topUI, offscreenAbove, duration)
                .setEase(LeanTweenType.easeInCubic)
                .setOnComplete(() => topUI.gameObject.SetActive(false));
        }
    }

    public void ShowNewGameStartScreen(bool show)
    {
        float duration = 0.4f;

        LeanTween.cancel(mainMenu);
        LeanTween.cancel(playerAndWeaponPicker.RectTransform);

        if (show)
        {
            mainMenu.gameObject.SetActive(true);
            playerAndWeaponPicker.gameObject.SetActive(true);

            mainMenu.anchoredPosition = mainMenuShowPos;
            playerAndWeaponPicker.RectTransform.anchoredPosition = playerAndWeaponPickerOffscreenRight;

            LeanTween.move(mainMenu, mainMenuShowPos + new Vector2(-mainMenu.rect.width, 0), duration)
                .setEase(LeanTweenType.easeOutCubic);

            LeanTween.move(playerAndWeaponPicker.RectTransform, playerAndWeaponPickerShowPos, duration)
                .setEase(LeanTweenType.easeOutCubic)
                .setOnComplete(() => { playerAndWeaponPicker.SetPickers(); });
        }
        else
        {
            mainMenu.gameObject.SetActive(true);
            LeanTween.move(mainMenu, mainMenuShowPos, duration)
                .setEase(LeanTweenType.easeInCubic);

            LeanTween.move(playerAndWeaponPicker.RectTransform, playerAndWeaponPickerOffscreenRight, duration)
                .setEase(LeanTweenType.easeInCubic)
                .setOnComplete(() => playerAndWeaponPicker.gameObject.SetActive(false));
        }
    }

    public void StartGame()
    {
        LeanTween.move(playerAndWeaponPicker.RectTransform, playerAndWeaponPickerOffscreenLeft, .4f)
            .setEase(LeanTweenType.easeInCubic)
            .setOnComplete(() => { playerAndWeaponPicker.gameObject.SetActive(false); ShowTopUI(true);});
    }

    public void ShowSettingsScreen(bool show)
    {
        float duration = 0.4f;

        LeanTween.cancel(mainMenu);
        LeanTween.cancel(settingsScreen);

        if (show)
        {
            // Move main menu left, bring settings in from right
            mainMenu.gameObject.SetActive(true);
            settingsScreen.gameObject.SetActive(true);

            mainMenu.anchoredPosition = mainMenuShowPos;
            settingsScreen.anchoredPosition = settingsScreenOffscreenRight;

            LeanTween.move(mainMenu, mainMenuShowPos + new Vector2(-mainMenu.rect.width, 0), duration)
                .setEase(LeanTweenType.easeInCubic);

            LeanTween.move(settingsScreen, settingsScreenShowPos, duration)
                .setEase(LeanTweenType.easeOutCubic);
        }
        else
        {
            // Move settings out right, main menu stays
            LeanTween.move(settingsScreen, settingsScreenOffscreenRight, duration)
                .setEase(LeanTweenType.easeInCubic)
                .setOnComplete(() => settingsScreen.gameObject.SetActive(false));

            // Optionally, reset main menu position if needed
            LeanTween.move(mainMenu, mainMenuShowPos, duration)
                .setEase(LeanTweenType.easeOutCubic);
        }
    }
}
