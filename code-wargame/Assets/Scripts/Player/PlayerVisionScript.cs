using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisionScript : MonoBehaviour
{
    [SerializeField]
    Player player;

    public List<Unit> visibleEnemyUnits;

    public void Scan()
    {
        visibleEnemyUnits = new List<Unit>();
        foreach (Unit unit in player.units)
        {
           visibleEnemyUnits.AddRange(unit.recon.Recon());
        }
    }
    public void addVisibleUnits(List<Unit> units)
    {
        foreach (Unit unit in player.units)
        {
            unit.recon.Recon();
        }
    }
    private void Start()
    {
        StartCoroutine(DoCheck());
    }
    IEnumerator DoCheck()
    {
        for (; ; )
        {
            Scan();

            yield return new WaitForSeconds(1f);
        }
    }
}
