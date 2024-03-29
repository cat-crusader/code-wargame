using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientPlayerControllerScript : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    UIManagerScript managerScript;

    public Camera cam;

    public Vector2 SelectBoxStartPosCamera;
    public Vector3 SelectBoxStartPos;
    public Vector3 SelectBoxEndPos;
    public Vector3 SelectBoxCenter;
    public Vector3 SelectBoxSize;

    public bool isFastMove;


    public List<Unit> selected;

    public event EventHandler<OnSelectedArgs> OnSelected;
    public class OnSelectedArgs : EventArgs
    {
       public List<Unit> selected;

    }
    // Start is called before the first frame update
    void Start()
    {
        isFastMove = false;
    }
    public void SelectUnit()
    {
        selected = new List<Unit>();

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Selectable")
            {
                selected.Add(hit.collider.gameObject.GetComponentInParent<Unit>());
            }
            else
            {
                selected = null;
            }
        }
        OnSelected?.Invoke(this, new OnSelectedArgs { selected = selected });
    }
    public void SelectBoxStart()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
        SelectBoxStartPos = hit.point;
        SelectBoxStartPosCamera = Input.mousePosition;
    }
    public void SelectBoxEnd()
    {
        selected = new List<Unit>();

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
        SelectBoxEndPos = hit.point;

        SelectBoxCenter = (SelectBoxStartPos + SelectBoxEndPos) / 2;

        SelectBoxSize = Abs((SelectBoxCenter - SelectBoxEndPos)*2);
        
        SelectBoxSize += new Vector3(0,1f,0);
        Collider[] colliders = Physics.OverlapBox(SelectBoxCenter, SelectBoxSize);

        foreach(Collider col in colliders)
        {
            if (col.tag== "Selectable")
            {
                selected.Add(col.gameObject.GetComponentInParent<Unit>());
            }
        }
        OnSelected?.Invoke(this, new OnSelectedArgs { selected = selected});
    }

    public void OrderToMove()
    {
        
        if (selected == null) return;

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            foreach (Unit unit in selected)
            {
                //Debug.Log(unit.movement);
                if (isFastMove) unit.movement.FastMoveTo(hit.point);
                else unit.movement.MoveTo(hit.point);
                //else unit.movement.MoveTo(hit.point);

            }


            // Do something with the object that was hit by the raycast.
        }
        if (isFastMove) isFastMove = false;
    }


    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    SelectUnit();
        //}

        if (Input.GetMouseButtonDown(0))
        {
            SelectBoxStart();
            managerScript.Select();
        }
        if (Input.GetMouseButtonUp(0))
        {
            SelectBoxEnd();
            managerScript.Deselect();
        }

        if (Input.GetMouseButtonDown(1))
        {

           OrderToMove();
            
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFastMove = true;
        }


    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(SelectBoxCenter,SelectBoxSize);
    }
    public Vector3 Abs(Vector3 v)
    {
        return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
    }
}
