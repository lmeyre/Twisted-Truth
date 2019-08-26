using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyArrow : MonoBehaviour
{
    public GameObject mob;
    [HideInInspector]public Vector3 offset;
    
    void Update()
    {
        if (mob == null)
            Destroy(gameObject);
    }
}
