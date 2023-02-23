using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraScript : MonoBehaviour
{
    public Camera camera;
    public GameObject unit;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void RayCastFromCamera()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            unit.GetComponent<UnitScript>().MoveTo(hit.point);

            // Do something with the object that was hit by the raycast.
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RayCastFromCamera();
        }
    }
}
