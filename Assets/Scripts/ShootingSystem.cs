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
    CinemachineImpulseSource impulseSource;

    void Start()
    {
        impulseSource = virtualCamera.GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {
        Vector3 angle = parentController.localEulerAngles;
        bool pressing = Input.GetMouseButton(0);

        if (Input.GetMouseButton(0))
        {
            VisualPolish();
        }

        if (Input.GetMouseButtonDown(0))
        {   
            parentController.position = splatGunNozzle.position; 
            inkParticle.Play(); 
        }
        else if (Input.GetMouseButtonUp(0))
            inkParticle.Stop();

        parentController.localEulerAngles
            = new Vector3(Mathf.LerpAngle(parentController.localEulerAngles.x, pressing ? virtualCamera.transform.eulerAngles.x : 0, .3f), angle.y, angle.z);
    }

    void VisualPolish()
    {
        if (!DOTween.IsTweening(parentController))
        {
            parentController.DOComplete();
            Vector3 forward = -parentController.forward;
            Vector3 localPos = parentController.localPosition;
            parentController.DOLocalMove(localPos - new Vector3(0, 0, .2f), .03f)
                .OnComplete(() => parentController.DOLocalMove(localPos, .1f).SetEase(Ease.OutSine));

           impulseSource.GenerateImpulse();
        }

        if (!DOTween.IsTweening(splatGunNozzle))
        {
            splatGunNozzle.DOComplete();
            splatGunNozzle.DOPunchScale(new Vector3(0, 1, 1) / 1.5f, .15f, 10, 1);
        }
    }
}
