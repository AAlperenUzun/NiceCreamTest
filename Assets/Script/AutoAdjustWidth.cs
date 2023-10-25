using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class AutoAdjustWidth : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        AdjustWidth();
    }

    void AdjustWidth()
    {
        var preferredWidth = textMeshPro.GetPreferredValues().x;
        var rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(preferredWidth, rect.sizeDelta.y);
    }
}