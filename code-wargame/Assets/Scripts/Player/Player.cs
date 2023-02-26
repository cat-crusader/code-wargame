using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    PlayerVisionScript vision;

    [SerializeField]
    PlayerCameraScript cameraScript;

    public List<GameObject> units;


}
