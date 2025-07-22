    using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyMoveUpText;
    [SerializeField] private TextMeshProUGUI keyMoveDownText;
    [SerializeField] private TextMeshProUGUI keyMoveLeftText;
    [SerializeField] private TextMeshProUGUI keyMoveRightText;
    [SerializeField] private TextMeshProUGUI keyInteractText;
    [SerializeField] private TextMeshProUGUI keyInteractAlternateText;
    [SerializeField] private TextMeshProUGUI keyPauseText;
    [SerializeField] private TextMeshProUGUI keyGamepadInteractText;
    [SerializeField] private TextMeshProUGUI keyGamepadInteractAlternateText;
    [SerializeField] private TextMeshProUGUI keyGamepadPauseText;

    private void Start()
    {
        GameInput.Instance.OnKeyBindingRebind += GameInput_OnKeyBindingRebind;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        UpdateVisual();

        Show();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
    }

    private void GameInput_OnKeyBindingRebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        keyMoveUpText.text = GameInput.Instance.GetBindings(GameInput.Bindings.Move_Up);
        keyMoveDownText.text = GameInput.Instance.GetBindings(GameInput.Bindings.Move_Down);
        keyMoveLeftText.text = GameInput.Instance.GetBindings(GameInput.Bindings.Move_Left);
        keyMoveRightText.text = GameInput.Instance.GetBindings(GameInput.Bindings.Move_Right);
        keyInteractText.text = GameInput.Instance.GetBindings(GameInput.Bindings.Interact);
        keyInteractAlternateText.text = GameInput.Instance.GetBindings(GameInput.Bindings.InteractAlternate);
        keyPauseText.text = GameInput.Instance.GetBindings(GameInput.Bindings.Pause);
        keyGamepadInteractText.text = GameInput.Instance.GetBindings(GameInput.Bindings.Gamepad_Interact);
        keyGamepadInteractAlternateText.text = GameInput.Instance.GetBindings(GameInput.Bindings.Gamepad_InteractAlternate);
        keyGamepadPauseText.text = GameInput.Instance.GetBindings(GameInput.Bindings.Gamepad_Pause);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
