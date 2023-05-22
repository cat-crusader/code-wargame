using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffectsController : MonoBehaviour
{
    private FireController fireController;

    [SerializeField]
    public ParticleSystem explosionPS;

    private void Start()
    {
        fireController = GetComponent<FireController>();
        fireController.OnExplosion += FireController_OnExplosion;
    }

    private void FireController_OnExplosion(object sender, FireController.OnExplosionArgs e)
    {
        explosionPS.gameObject.transform.position = e.position;
        explosionPS.Play();
        
    }

    public void InstantiateExplosion()
    {

    }
}
