using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitSymbolUIScript : MonoBehaviour
{
    [SerializeField]
    Unit unit;



    public GameObject UnitSymbolsCanvas;
    public GameObject UnitSybmolPrefab;
    public GameObject UnitSymbol;

    TMP_Text text;
    Transform textChild;
    Transform outline;
    Image labelImage;
    

    public Camera cam;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        UnitSymbol = Instantiate(UnitSybmolPrefab, UnitSymbolsCanvas.transform);
        textChild = UnitSymbol.transform.GetChild(2);
        text = textChild?.GetComponent<TMP_Text>();
        labelImage = UnitSymbol.transform.GetChild(1).GetComponent<Image>();
        outline = UnitSymbol.transform.GetChild(0);

        unit.OnHPChange += Unit_OnHPChange;
        unit.OnUnitSpawn += Unit_OnUnitSpawn;
        if(unit.player.controllerScript!=null) unit.player.controllerScript.OnSelected += ControllerScript_OnSelected;
    }

    private void ControllerScript_OnSelected(object sender, ClientPlayerControllerScript.OnSelectedArgs e)// THERE MAYBE SOME BUG WITH OUTLINE, ENEMY DONT OUTLINE FOR SOME REASON (CAUSE NOW ENEMY HAS NO PLAYER CONTROLLER SCRIPT)
    {
        bool set = false;
        if(e.selected.Count==0)
        {
            outline.gameObject.SetActive(false);
            return;
        }
        foreach(Unit selectedUnit in e.selected)
        {
            if(selectedUnit == unit && set == false)
            {
                outline.gameObject.SetActive(true);
                set = true;
            }
            else if (set == false)
            {
                outline.gameObject.SetActive(false);
            }
        }
    }

    private void Unit_OnUnitSpawn(object sender, Unit.OnUnitSpawnArgs e)
    {
        if (unit.unitStats.type == UnitType.Infantry)
        {
            text.text = e.unit.unitStats.name + " " + e.unit.unitStats.hp;
            //GetComponent<RawImage>();
        }
        else
        {
            text.text = e.unit.unitStats.name;
        }

        if (e.unit.player.type == "enemy")//TEMPORARY ENEMY UNIT COLORING
        {
            Color redcolor;
            if (ColorUtility.TryParseHtmlString("#F84C33C8", out redcolor))
            {//09FF0064
                labelImage.color = redcolor;
            }

        }
        else
        {//
            Color bluecolor;
            if (ColorUtility.TryParseHtmlString("#4A3EFFC8", out bluecolor))
            {
                labelImage.color = bluecolor;
            }
        }
    }

    private void Unit_OnHPChange(object sender, Unit.OnHPChangeArgs e)
    {
        if (unit.unitStats.type == UnitType.Infantry)
        {

            text.text = unit.unitStats.name + " " + e.HP;
        }
        if (e.HP <= 0)
        {
            UnitSymbol.SetActive(false);
        }
        //Debug.Log("UNIT HP " + e.HP);
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
