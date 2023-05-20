using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellScipt : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float Speed;
    void Start()
    {
        SetRotation();
    }
    void SetRotation()
    {
        Transform smoke = this.gameObject.transform.GetChild(0);
        Vector3 direction = endPosition - startPosition;
        Quaternion rotation = Quaternion.LookRotation(-direction, Vector3.up);

        // Assuming your script is attached to the game object you want to rotate
        smoke.rotation = rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = (endPosition - startPosition).normalized;
        float distance = Vector3.Distance(gameObject.transform.position, endPosition);
        if (distance <= 0.5f)
        {
            OnTargetReach();
        }

            // Move the object incrementally towards the target position
            transform.Translate(direction * Speed/10f);
    }
    public void OnTargetReach()
    {
        Destroy(this.gameObject);
    }
}
