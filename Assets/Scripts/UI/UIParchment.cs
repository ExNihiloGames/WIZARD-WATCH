using UnityEngine;
using UnityEngine.UI;

public class UIParchment : MonoBehaviour
{
	public GameObject OtherParchment;
	public ScrollRect AutoScrollToBottomScrollRect;

	void Update()
	{
		AutoScrollToBottomScrollRect.verticalNormalizedPosition = 0;
	}

	void OnMouseDown()
	{
		gameObject.SetActive(false);
		OtherParchment.SetActive(true);
		AudioManager.Instance.Play("Paper0");
	}
}
