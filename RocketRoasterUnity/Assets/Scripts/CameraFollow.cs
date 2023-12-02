using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    float _initialZ;
    public float yOffset;


    private void Awake()
    {
        _initialZ = transform.position.z;
    }

    void Update()
    {

        if (_target != null)
        {
            if (_target.position.y < transform.position.y - yOffset) return;

            transform.position = new Vector3(0, _target.transform.position.y + yOffset, _initialZ);
        }
    }
}
