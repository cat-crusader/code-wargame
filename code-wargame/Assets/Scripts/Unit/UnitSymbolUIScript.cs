using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSymbolUIScript : MonoBehaviour
{

    public GameObject UnitSymbolsCanvas;
    public GameObject UnitSybmolPrefab;
    public GameObject UnitSymbol;

    public Camera cam;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
       UnitSymbol = Instantiate(UnitSybmolPrefab, UnitSymbolsCanvas.transform);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = cam.WorldToScreenPoint(transform.position + offset);

        if (UnitSymbol.transform.position != pos)
        {
            UnitSymbol.transform.position = pos;
            //Debug.Log(UnitSymbol.name + " updating position");
        }
    }
}
