using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBookOfTuto : MonoBehaviour
{
	public TutoPage[] Pages;
	public UITutoPage SheetLeft;
	public UITutoPage SheetRight;
	public GameObject BookOpen;
	public GameObject BookClosed;

	int _currentPageIndex = 0;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			gameObject.SetActive(false);

		BookOpen.SetActive(_currentPageIndex != 0);
		BookClosed.SetActive(_currentPageIndex == 0);

		if (_currentPageIndex != 0)
		{
			SheetLeft.gameObject.SetActive(true);
			var indexLeft = _currentPageIndex * 2 - 2;
			var indexRight = _currentPageIndex * 2 - 1;
			SheetLeft.InitTutoPage(Pages[indexLeft].Text, Pages[indexLeft].Image);
			if (indexRight < Pages.Length)
			{
				SheetRight.InitTutoPage(Pages[indexRight].Text, Pages[indexRight].Image);
			}
			else
			{
				SheetRight.InitBlankPage();
			}
		}
	}

	public void TurnPage(int increment)
	{
		if (_currentPageIndex + increment >= 0 && _currentPageIndex + increment <= Mathf.CeilToInt(Pages.Length / 2f))
			_currentPageIndex += increment;
	}
}

[System.Serializable]
public struct TutoPage
{
	public string Text;
	public Sprite Image;
}
