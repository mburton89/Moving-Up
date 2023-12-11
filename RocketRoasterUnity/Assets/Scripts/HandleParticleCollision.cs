using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleParticleCollision : MonoBehaviour
{
    public GameObject particlePrefab;

    private void OnParticleCollision(GameObject other)
    {
        Instantiate(particlePrefab, transform.position, transform.rotation, transform);
    }
}
