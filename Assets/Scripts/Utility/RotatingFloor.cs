using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingFloor : MonoBehaviour
{
    public void Update()
    {
        transform.Rotate(new Vector3(0f, 100f, 0f) * Time.deltaTime);
    }
}
