using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyView : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    public TMP_Text ValueText;

    public void SetCurrency(Sprite icon, int value)
    {
        if (iconImage != null)
        {
            iconImage.sprite = icon; 
        }
        ValueText.text = value.ToString();
    }
}