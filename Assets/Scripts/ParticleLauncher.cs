using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLauncher : MonoBehaviour
{
    [SerializeField] ParticleSystem inkParticle;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetButton("Fire1")) inkParticle.Emit(1);
    }
}
