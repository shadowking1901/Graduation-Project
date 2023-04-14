using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ActionStateManager : MonoBehaviour
{
    [HideInInspector] public ActionBaseState currentState;

    public ReloadState Reload = new ReloadState();
    public DefaultState Default = new DefaultState();
    [HideInInspector] public ShootingSystem ammo;
    //AudioSource audioSource;

    [HideInInspector] public Animator anim;

    //public MultiAimConstraint rHandAim;
    //public TwoBoneIKConstraint lHandIK;

    // Start is called before the first frame update
    void Start()
    {
        SwitchState(Default);
        ammo = GetComponent<ShootingSystem>();
        //audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(ActionBaseState state) 
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void WeaponReloaded() 
    {
        ammo.Reload();
        //SwitchState(Default);
    }

    //public void SoundOfGunReload()
    //{
        //audioSource.PlayOneShot(ammo.gunReloadSound);
    //}
}
