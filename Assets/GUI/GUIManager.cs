using System.Collections.Generic;
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
    [SerializeField] private RectTransform topUI;
    [SerializeField] private Image blurOverlay;

    public static GUIManager Instance;

    private Vector2 mainMenuShowPos;
    private Vector2 topUIShowPos;

    private void Awake()
    {
        Instance = this;
        if (blurOverlay != null)
            blurOverlay.gameObject.SetActive(false);

        mainMenuShowPos = mainMenu.anchoredPosition;
        topUIShowPos = topUI.anchoredPosition;
    }

    public void ShowMainMenu(bool show, float duration = 0.4f)
    {
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

    public void ShowTopUI(bool show, float duration = 1)
    {
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

    public void ShowWeaponPicker<T>(List<T> values)
    {
        optionPicker.SetOptions(values);
    }

    public void HideOptionPicker()
    {
        optionPicker.ClearOptions();
    }
}
