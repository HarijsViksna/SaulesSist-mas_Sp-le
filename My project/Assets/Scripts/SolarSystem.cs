using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{

    readonly float G = 100f;
    GameObject[] Planets;

    // Start is called before the first frame update
    void Start()
    {
        Planets = GameObject.FindGameObjectsWithTag("Planets");
        InitialVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Gravity();
    }

    void Gravity()
    {
        foreach (GameObject a in Planets)
        {
            foreach (GameObject b in Planets)
            {
                if (!a.Equals(b))
                {
                    Rigidbody rbA = a.GetComponent<Rigidbody>();
                    Rigidbody rbB = b.GetComponent<Rigidbody>();

                    Vector3 direction = (b.transform.position - a.transform.position).normalized;
                    float distance = Vector3.Distance(a.transform.position, b.transform.position);
                    if (distance > 0.001f)
                    {
                        float m1 = rbA.mass;
                        float m2 = rbB.mass;
                        float forceMagnitude = G * (m1 * m2) / (distance * distance);
                        Vector3 force = direction * forceMagnitude;
                        rbA.AddForce(force);
                    }
                }
            }
        }
    }


    void InitialVelocity()
    {
        foreach (GameObject a in Planets)
        {
            foreach (GameObject b in Planets)
            {
                if(a!.Equals(b))
                {
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);
                    a.transform.LookAt(b.transform);

                    a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt((G * m2) / r);
                }
            }
        }
    }
}
