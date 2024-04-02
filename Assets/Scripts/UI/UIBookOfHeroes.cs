using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBookOfHeroes : MonoBehaviour
{
	public HeroIDList Heroes;
	public UICharacterPage SheetLeft;
	public UICharacterPage SheetRight;
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
			SheetLeft.InitCharacterSheet(Heroes.list[indexLeft]);
			if (indexRight < Heroes.list.Count)
			{
				SheetRight.InitCharacterSheet(Heroes.list[indexRight]);
			}
			else
			{
				SheetRight.InitBlankPage();
			}
		}
	}

	public void TurnPage(int increment)
	{
		if (_currentPageIndex + increment >= 0 && _currentPageIndex + increment <= Mathf.CeilToInt(Heroes.list.Count / 2f))
			_currentPageIndex += increment;
	}
}
