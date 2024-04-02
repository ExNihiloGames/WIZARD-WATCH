using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UITutoPage : MonoBehaviour
{
	public GameObject TutoSheet;
	public TextMeshProUGUI Text;
	public Image Image;

	public void InitTutoPage(string text, Sprite image)
	{
		Text.text = text;
		Image.sprite = image;
		Image.preserveAspect = true;

		TutoSheet.SetActive(true);
	}

	public void InitBlankPage()
	{
		TutoSheet.SetActive(false);
	}
}
