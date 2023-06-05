using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitBarInfoUIScript : MonoBehaviour
{
    public TMP_Text hpText;
    public TMP_Text weaponsText;
    public TMP_Text nameText;

    public Unit currentSelectedUnit;

    [SerializeField]
    ClientPlayerControllerScript controllerScript;

    // Update is called once per frame
    void Start()
    {
        controllerScript.OnSelected += ControllerScript_OnSelected;

    }


    public void HideBar()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
    }
    public void ShowBar()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
    }
    private void CurrentSelectedUnit_OnHPChange(object sender, Unit.OnHPChangeArgs e)
    {
        if (currentSelectedUnit != null) hpText.text = "hp " + e.HP + " / " + currentSelectedUnit.unitStats.hp;
    }
    void SelectUnit(ClientPlayerControllerScript.OnSelectedArgs e)
    {
        Unit unitInfo = e.selected[0];
        currentSelectedUnit = unitInfo;
        hpText.text = "hp " + +unitInfo.temp_CurrentHP + " / " + unitInfo.unitStats.hp;
        nameText.text = unitInfo.unitStats.name;
        currentSelectedUnit.OnHPChange += CurrentSelectedUnit_OnHPChange;
        currentSelectedUnit.OnUnitSpawn += CurrentSelectedUnit_OnUnitSpawn;
    }

    private void CurrentSelectedUnit_OnUnitSpawn(object sender, Unit.OnUnitSpawnArgs e)
    {
        if (e.spawn == false)
        {
            DeselectUnit();
        }
    }

    void DeselectUnit()
    {
        if (currentSelectedUnit != null)
        {
            currentSelectedUnit.OnHPChange -= CurrentSelectedUnit_OnHPChange;
            currentSelectedUnit.OnUnitSpawn -= CurrentSelectedUnit_OnUnitSpawn;
        }
    }
    private void ControllerScript_OnSelected(object sender, ClientPlayerControllerScript.OnSelectedArgs e)
    {
        if (e.selected.Count == 0)
        {
            HideBar();
            return;
        }
        ShowBar();
        DeselectUnit();
        SelectUnit(e);
        //Unit unitInfo = e.selected[0];
        //currentSelectedUnit = unitInfo;
        //hpText.text = "hp "+ + unitInfo.temp_CurrentHP+" / "+ unitInfo.unitStats.hp;
        //nameText.text = unitInfo.unitStats.name;
        //for(int i =0;i<unitInfo.)
    }
}
