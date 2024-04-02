using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainGame : MonoBehaviour
{
	public RectTransform ManaBackground;
	public RectTransform ManaCurrent;
	public GameObject BookOfHeroes;
	public UIParchment Parchment;

	void Update()
	{
		SelectionManager.Instance.IsOverUI = BookOfHeroes.activeSelf || Parchment.gameObject.activeSelf;
	}

	public void UpdateCurrentMana(int currentMana, int maxMana)
	{
		var maxHeight = ManaBackground.rect.height;
		ManaCurrent.sizeDelta = new Vector2(ManaCurrent.rect.width, maxHeight * currentMana / maxMana); ;
	}

	public void OpenBookOfHeroes()
	{
		BookOfHeroes.SetActive(true);
	}
}
