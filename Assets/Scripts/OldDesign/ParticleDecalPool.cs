using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDecalPool : MonoBehaviour
{
    public static readonly int maxDecalNum = 5000;
    public static readonly float minDecalSize = 0.5f;
    public static readonly float maxDecalSize = 1.5f;

    private int decalDataIndex = 0;
    private ParticleDecalData[] decalDatas = new ParticleDecalData[maxDecalNum];
    private ParticleSystem.Particle[] decals = new ParticleSystem.Particle[maxDecalNum];
    private ParticleSystem decalParticleSystem;

    private void Start()
    {
        decalParticleSystem = GetComponent<ParticleSystem>();
        for (int i = 0; i < decalDatas.Length; i++)
        {
            decalDatas[i] = new ParticleDecalData();
        }
    }

    public void ParticleHit(ParticleCollisionEvent particleCollisionEvent, Gradient gradient)
    {
        SetDecalData(particleCollisionEvent, gradient);
        DisplayDecals();
    }

    private void SetDecalData(ParticleCollisionEvent particleCollisionEvent, Gradient gradient)
    {
        if (decalDataIndex >= maxDecalNum)
        {
            decalDataIndex = 0;
        }

        // set position
        decalDatas[decalDataIndex].position = particleCollisionEvent.intersection;
        // set rotation
        Vector3 decalRotationEuler = Quaternion.LookRotation(particleCollisionEvent.normal).eulerAngles;
        decalRotationEuler.z = Random.Range(0f, 360f);
        decalDatas[decalDataIndex].rotation = decalRotationEuler;
        // set size
        decalDatas[decalDataIndex].size = Random.Range(minDecalSize, maxDecalSize);
        // set color
        decalDatas[decalDataIndex].color = gradient.Evaluate(Random.Range(0f, 1f));

        decalDataIndex += 1;
    }

    private void DisplayDecals()
    {
        for (int i = 0; i < decals.Length; i++)
        {
            decals[i].position = decalDatas[i].position;
            decals[i].rotation3D = decalDatas[i].rotation;
            decals[i].startSize = decalDatas[i].size;
            decals[i].startColor = decalDatas[i].color;
        }
        decalParticleSystem.SetParticles(decals, decals.Length);
    }
}
