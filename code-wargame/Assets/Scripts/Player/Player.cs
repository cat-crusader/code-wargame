using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    PlayerVisionScript vision;

    [SerializeField]
    public ClientPlayerControllerScript controllerScript;

    [SerializeField]
    ClientPlayerCameraScript cameraScript;

    public List<Unit> units;

    public string type;

    private void Start()
    {

    }
    public void SpawnUnit(GameObject unit, Transform spawnPosition)
    {
        GameObject newUnit = Instantiate(unit, spawnPosition.position, Quaternion.identity);
        Unit newUnitScript = newUnit.GetComponent<Unit>();
        newUnitScript.player = this;
        units.Add(newUnitScript);
    }

}
