using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 goHere = new Vector3 (target.position.x,target.position.y,-10);
        transform.position = goHere;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 goHere = new Vector3(target.position.x, target.position.y, -10);
        transform.position = goHere;
    }
}
