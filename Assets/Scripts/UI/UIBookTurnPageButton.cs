using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBookTurnPageButton : MonoBehaviour
{
	public UIBookOfHeroes UIBookOfHeroes;
	public UIBookOfTuto UIBookOfTuto;
	public int IncrementPage;

	private void OnMouseDown()
	{
		if (UIBookOfHeroes!= null)
			UIBookOfHeroes.TurnPage(IncrementPage);
		if (UIBookOfTuto != null)
			UIBookOfTuto.TurnPage(IncrementPage);
	}
}
