using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] SphereCollider coll;

    public void Active()
    {
        GetComponent<ParticleSystem>().Play();
    }
}
