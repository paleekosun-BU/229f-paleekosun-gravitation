using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class Gravity : MonoBehaviour
{
    private Rigidbody rb;

    private const float G = 0.00667f;

    public static List<Gravity> gravityObjectList;
    
    [SerializeField] bool planets = false;
    [SerializeField] private int orbitSpeed = 1000;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (gravityObjectList == null)
        {
            gravityObjectList = new List<Gravity>();
        }
        
        gravityObjectList.Add(this);

        if (!planets)
        {
            rb.AddForce(Vector3.right * orbitSpeed);
        }
    }

    private void FixedUpdate()
    {
        foreach (var obj in gravityObjectList)
        {
            if (obj != this)
                Attract(obj);
            
            
        }
    }

    void Attract(Gravity other)
    {
        Rigidbody otherRb = other.rb;
        Vector3 direction = rb.position - otherRb.position;
        float distance = direction.magnitude;
        
        float forceMagnitude = G * (rb.mass * otherRb.mass/Mathf.Pow(distance,2));
        Vector3 gravityForce = forceMagnitude * direction.normalized;
        
        otherRb.AddForce( gravityForce );

    }

}
