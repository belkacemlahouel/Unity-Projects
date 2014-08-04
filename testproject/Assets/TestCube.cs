using UnityEngine;
using System.Collections;

public class TestCube : MonoBehaviour
{
    void Update ()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            gameObject.renderer.material.color = Color.red;
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            gameObject.renderer.material.color = Color.green;
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            gameObject.renderer.material.color = Color.blue;
        }
    }
}