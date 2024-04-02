using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    // Selection Mask
    public LayerMask clickableMask;
    public bool IsOverUI;
    // Singleton
    private static SelectionManager _instance;
    public static SelectionManager Instance { get { return _instance; } }

    private Transform prevHighlighted;

    private void Awake()
    {
        if (_instance != null & _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, float.MaxValue, clickableMask) && !IsOverUI)
        {
            //Debug.Log("Hoovering");
            Transform highlighted = hit.transform;
            if (highlighted != prevHighlighted)
            {
                if (prevHighlighted)
                {
                    if (prevHighlighted.GetComponent<ObjectsSelection>().isHighlighted)
                    {
                        prevHighlighted.GetComponent<ObjectsSelection>().ToneDown();
                    }
                }
                highlighted.GetComponent<ObjectsSelection>().Highlight();
                prevHighlighted = highlighted;
            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                highlighted.gameObject.GetComponent<ObjectsSelection>()._MouseClick();
            }
        }
        else
        {
            if (prevHighlighted)
            {
                prevHighlighted.GetComponent<ObjectsSelection>().ToneDown();
                prevHighlighted = null;
            }
        }
    }
}
