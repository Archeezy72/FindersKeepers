using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class ThirdMovement : MonoBehaviour
{
    Rigidbody rb;
    public Camera MyCamera;

    bool probihapohyb = false;
    public float rychlostPohybu = 5f;
    public float rychlostotaceni = 5f;
    Vector3 cilovapozice;
    public float zbyvajicicas;
    public bool otaceniprobiha = false;
    float smerotaceni;
    Vector3 cilovaorientace;
    public float raycastDistance = 1f;
    public LayerMask Obstacle;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        while (true)
        {
            if (probihapohyb)
            {
                zbyvajicicas -= Time.deltaTime;
                if (zbyvajicicas <= 0)
                {
                    transform.position = cilovapozice;
                    probihapohyb = false;
                }
                else
                {
                    transform.position += Time.deltaTime * rychlostPohybu * transform.forward;
                    break;
                }
            }

            if (otaceniprobiha)
            {
                zbyvajicicas -= Time.deltaTime;
                if (zbyvajicicas <= 0)
                {
                    transform.eulerAngles = cilovaorientace;
                    otaceniprobiha = false;
                }
                else
                {
                    transform.Rotate(0, Time.deltaTime * rychlostotaceni * smerotaceni, 0);
                    break;
                }
            }

            if ((Input.GetKey("up") || Input.GetKey("w")) && !IsObstacleInFront())
            {
                cilovapozice = transform.position + transform.forward;
                probihapohyb = true;
                zbyvajicicas = 1f / rychlostPohybu;
            }
            else
            {
                if (Input.GetKey("left") || Input.GetKey("right") || Input.GetKey("a") || Input.GetKey("d"))
                {
                    if (Input.GetKey("left") || Input.GetKey("a"))
                    {
                        smerotaceni = -90;
                    }
                    else
                    {
                        smerotaceni = 90;
                    }
                    cilovaorientace = transform.rotation.eulerAngles + (smerotaceni * Vector3.up);
                    otaceniprobiha = true;
                    zbyvajicicas = 1f / rychlostotaceni;
                }
            }
            break;
        }
    }

    bool IsObstacleInFront()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance, Obstacle))
        {
            return true;
        }
        return false;
    }
}