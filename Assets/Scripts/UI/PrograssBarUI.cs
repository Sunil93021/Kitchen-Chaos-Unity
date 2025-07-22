using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrograssBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private GameObject progressGameObject;
    private IHasProgress hasProgress;
    private void Start()
    {
        hasProgress = progressGameObject.GetComponent<IHasProgress>();

        if (hasProgress == null)
        {
            Debug.LogError("game Object " + progressGameObject + " dont have IHasProgress Component");
        }
        hasProgress.OnProgressChanged += hasProgress_OnProgressChanged;
        barImage.fillAmount = 0;
        Hide();
    }

    private void hasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        if (e.progressNormalized == 0f || e.progressNormalized == 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
            barImage.fillAmount = e.progressNormalized;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
