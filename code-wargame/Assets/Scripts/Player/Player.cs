using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    PlayerVisionScript vision;

    [SerializeField]
    ClientPlayerCameraScript cameraScript;

    public List<Unit> units;

    private void Start()
    {

    }


}
