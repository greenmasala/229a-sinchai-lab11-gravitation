using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    Rigidbody rb;
    const float G = 0.006674f; //gravitional constant

    public static List<Gravity> otherObjects;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        if (otherObjects == null )
        {
            otherObjects = new List<Gravity>();
        }

        otherObjects.Add(this);
    }

    private void FixedUpdate()
    {
        foreach(Gravity obj in otherObjects)
        {
            if (obj != this)
            {
                Attract(obj);
            }
        }
    }

    void Attract(Gravity other)
    {
        Rigidbody otherRb = other.rb;

        //get direction between two objs
        Vector3 direction = rb.position - otherRb.position;
        
        //get distance of direction between two objs
        float distance = direction.magnitude;

        //if objs are at the same pos, just return to do nothing and avoid collision
        if (distance == 0f)
        {
            return;
        }

        //F = G*((m1*m2)/r*r
        float forceMagnitude = G*(rb.mass * otherRb.mass)/Mathf.Pow(distance, 2); //pow is power duh!

        //get final gravitational force
        Vector3 gravityForce = forceMagnitude * direction.normalized;

        otherRb.AddForce(gravityForce); 
    }
}
