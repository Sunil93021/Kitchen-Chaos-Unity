using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAlternateButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button gamepadInteractButton;
    [SerializeField] private Button gamepadInteractAlternateButton;
    [SerializeField] private Button gamepadPauseButton;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAlternateText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI gamepadInteractText;
    [SerializeField] private TextMeshProUGUI gamepadInteractAlternateText;
    [SerializeField] private TextMeshProUGUI gamepadPauseText;
    [SerializeField] private Transform pressToRebindKeyTransform;


    private Action onCloseButtonAction;
    public static OptionsUI Instance { get;private set; }

    private void Awake()
    {
        Instance = this;
        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() =>
        {
            Hide();
            onCloseButtonAction();
        });

        moveUpButton.onClick.AddListener(() => { Rebinding(GameInput.Bindings.Move_Up);} );
        moveDownButton.onClick.AddListener(() => { Rebinding(GameInput.Bindings.Move_Down);} );
        moveLeftButton.onClick.AddListener(() => { Rebinding(GameInput.Bindings.Move_Left);} );
        moveRightButton.onClick.AddListener(() => { Rebinding(GameInput.Bindings.Move_Right);} );
        interactButton.onClick.AddListener(() => { Rebinding(GameInput.Bindings.Interact);} );
        interactAlternateButton.onClick.AddListener(() => { Rebinding(GameInput.Bindings.InteractAlternate);} );
        pauseButton.onClick.AddListener(() => { Rebinding(GameInput.Bindings.Pause);} );
        gamepadInteractButton.onClick.AddListener(() => { Rebinding(GameInput.Bindings.Gamepad_Interact);} );
        gamepadInteractAlternateButton.onClick.AddListener(() => { Rebinding(GameInput.Bindings.Gamepad_InteractAlternate);} );
        gamepadPauseButton.onClick.AddListener(() => { Rebinding(GameInput.Bindings.Gamepad_Pause);} );
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        UpdateVisual();
        HidePressToRebindKey();
        Hide();
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectsText.text = "Sound Effects : "+Math.Round(SoundManager.Instance.GetVolume()*10);
        musicText.text = "Music : "+Math.Round(MusicManager.Instance.GetVolume()*10);


        moveUpText.text = GameInput.Instance.GetBindings(GameInput.Bindings.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindings(GameInput.Bindings.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindings(GameInput.Bindings.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindings(GameInput.Bindings.Move_Right);
        interactText.text = GameInput.Instance.GetBindings(GameInput.Bindings.Interact);
        interactAlternateText.text = GameInput.Instance.GetBindings(GameInput.Bindings.InteractAlternate);
        pauseText.text = GameInput.Instance.GetBindings(GameInput.Bindings.Pause);
        gamepadInteractText.text = GameInput.Instance.GetBindings(GameInput.Bindings.Gamepad_Interact);
        gamepadInteractAlternateText.text = GameInput.Instance.GetBindings(GameInput.Bindings.Gamepad_InteractAlternate);
        gamepadPauseText.text = GameInput.Instance.GetBindings(GameInput.Bindings.Gamepad_Pause);
        

    }

    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction = onCloseButtonAction;

        gameObject.SetActive(true);

        soundEffectsButton.Select();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }
    private void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void Rebinding(GameInput.Bindings bindings)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(bindings, () =>
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }
}
