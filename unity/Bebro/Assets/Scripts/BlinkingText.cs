using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BlinkingText : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 0.1f + (Mathf.Sin(Time.time*2)+1f)*0.5f);
    }
}
