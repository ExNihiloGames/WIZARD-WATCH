using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSelection : MonoBehaviour
{
    public GameObject highlightEffect;
    // public GameObject onClickEffect;

    public bool isHighlighted;
    public void Highlight()
    {
        if (highlightEffect != null)
		{
            highlightEffect.SetActive(true);
            isHighlighted = true;
        }
    }

    public void ToneDown()
    {
        if (highlightEffect != null)
        {
            highlightEffect.SetActive(false);
            isHighlighted = false;
        }
    }

    public void _MouseClick()
    {
        if (gameObject.GetComponent<Room>())
        {
            gameObject.GetComponent<Room>().OnClick();
        }
        else if (gameObject.GetComponent<Gate>())
        {
            gameObject.GetComponent<Gate>().OnClick();
        }
        else if (gameObject.GetComponent<UIBook>())
		{
            gameObject.GetComponent<UIBook>().OnClick();

        }
    }
}
