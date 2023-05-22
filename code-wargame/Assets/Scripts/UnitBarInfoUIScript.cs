using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitBarInfoUIScript : MonoBehaviour
{
    public TMP_Text hpText;
    public TMP_Text weaponsText;
    public TMP_Text nameText;

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
    private void ControllerScript_OnSelected(object sender, ClientPlayerControllerScript.OnSelectedArgs e)
    {
        if (e.selected.Count == 0)
        {
            HideBar();
            return;
        }
        ShowBar();
        Unit unitInfo = e.selected[0];
        hpText.text = "hp "+ + unitInfo.temp_CurrentHP+" / "+ unitInfo.unitStats.hp;
        nameText.text = unitInfo.unitStats.name;
        //for(int i =0;i<unitInfo.)
    }
}
