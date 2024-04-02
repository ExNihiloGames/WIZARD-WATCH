using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UILogEntry : MonoBehaviour
{
    [SerializeField] Image portraitImage;
    [SerializeField] TextMeshProUGUI text;

    public void SetImage(bool display, Sprite portrait)
    {
        portraitImage.gameObject.SetActive(display);
        portraitImage.sprite = portrait;
    }

    public void SetText(string logText)
    {
        text.text = logText;
    }
}
