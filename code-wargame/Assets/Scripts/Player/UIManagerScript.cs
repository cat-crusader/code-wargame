using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerScript : MonoBehaviour
{
    [SerializeField]
    ClientPlayerControllerScript controllerScript;

    public GameObject BoxSelection;
    public bool isSelected;

    private void Start()
    {
        Deselect();
    }
    public void Select()
    {
        isSelected = true;
        BoxSelection.SetActive(true);
    }
    public void Deselect()
    {
        isSelected = false;
        BoxSelection.SetActive(false);
    }

    private void Update()
    {
        if (isSelected)
        {
            Vector2 boxStartPos = controllerScript.SelectBoxStartPosCamera;
            BoxSelection.GetComponent<RectTransform>().anchoredPosition = boxStartPos;
            BoxSelection.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Input.mousePosition.x- boxStartPos.x);
            BoxSelection.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Input.mousePosition.y- boxStartPos.y);
        }
    }
}
