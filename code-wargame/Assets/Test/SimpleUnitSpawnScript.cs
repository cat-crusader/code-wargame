using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimpleUnitSpawnScript : MonoBehaviour
{
    public Transform spawnPosition;
    public int Points;
    public TMP_Text text;
    public Player player1;

    // Start is called before the first frame update
    void Start()
    {
        Points = 100;
        StartCoroutine(PointsUP());
    }

    public void SpawnUnit(GameObject unit)
    {
        int cost = 0;
        if (unit.GetComponent<Unit>()?.unitStats.type == UnitType.Infantry) cost = 20;
        if (unit.GetComponent<Unit>()?.unitStats.name == "APC") cost = 30;
        if (unit.GetComponent<Unit>()?.unitStats.name == "Tank") cost = 50;
        if (Points - cost >= 0)
        {
            player1.SpawnUnit(unit, spawnPosition);


            ChangePoint(-cost);
        }
    }
    IEnumerator PointsUP()
    {
       
        while (true)
        {
            ChangePoint(1);
            yield return new WaitForSeconds(1f);
        }
    }
    public void ChangePoint(int dif)
    {
        Points += dif;
        text.text = Points.ToString();
    }
}
