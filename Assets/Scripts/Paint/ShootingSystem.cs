using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class ShootingSystem : MonoBehaviour
{
    [SerializeField] ParticleSystem inkParticle;
    [SerializeField] Transform parentController;
    [SerializeField] Transform splatGunNozzle;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    //CinemachineImpulseSource impulseSource;

    [HideInInspector] public float maxAmmo = 1;
    [Range(0,1)][SerializeField] float ammoConsumptionRatePerSec;
    [HideInInspector] public float currentAmmo;

    //public AudioClip gunReloadSound;

    void Start()
    {
        //impulseSource = virtualCamera.GetComponent<CinemachineImpulseSource>();
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (ShouldFire()) Fire();
        if (Input.GetKeyDown(KeyCode.R)) Reload();
    }

    bool ShouldFire() 
    {
        if (currentAmmo <= 0) 
        {
            inkParticle.Stop();
            return false; 
        }
        return true;
    }
    void Fire()
    {
        Vector3 angle = parentController.localEulerAngles;
        bool pressing = Input.GetMouseButton(0);

        if (Input.GetMouseButton(0))
        {
            currentAmmo -= ammoConsumptionRatePerSec * Time.deltaTime;
            VisualPolish();
        }

        if (Input.GetMouseButtonDown(0))
            inkParticle.Play();
        else if (Input.GetMouseButtonUp(0))
            inkParticle.Stop();

        parentController.localEulerAngles
            = new Vector3(Mathf.LerpAngle(parentController.localEulerAngles.x, pressing ? virtualCamera.transform.eulerAngles.x : 0, .3f), angle.y, angle.z);
        parentController.position = new Vector3(splatGunNozzle.position.x, splatGunNozzle.position.y, splatGunNozzle.position.z);
    }

    void VisualPolish()
    {
        if (!DOTween.IsTweening(splatGunNozzle))
        {
            splatGunNozzle.DOComplete();
            splatGunNozzle.DOPunchScale(new Vector3(0, 1, 1) / 1.5f, .15f, 10, 1);
        }
    }

    public void Reload() 
    {
        currentAmmo = maxAmmo;
    }
}