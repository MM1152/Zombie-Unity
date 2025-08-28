using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum State
    {
        Ready,
        Empty,
        Reloading,
    }

    private State currentState = State.Ready;

    public State CurrentState
    {
        get => currentState;
        set
        {
            currentState = value;

            switch(currentState)
            {
                case State.Ready:
                    break;
                case State.Empty:
                    break;
                case State.Reloading:
                    break;
            }
        }
    }

    public GunData gunData;

    public UIManager uiManager;

    public ParticleSystem muzzleEffect;
    public ParticleSystem shellEffect;
    
    private LineRenderer lineRender;
    private AudioSource audioSource;

    public Transform firePosition;
    public AudioClip itemPickUpClip;

    public int ammoRemain;
    public int magAmmo;

    public float lastFireTime;

    private void OnEnable()
    {
        ammoRemain = gunData.startAmmoRemain;
        magAmmo = gunData.magCapacity;

        lastFireTime = 0f;

        CurrentState = State.Ready;
        uiManager.SetAmmoText(magAmmo, ammoRemain);
    }

    private void Awake()
    {
        lineRender = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();

        lineRender.enabled = false;
        lineRender.positionCount = 2;
    }

    private void Update()
    {
        switch(currentState)
        {
            case State.Ready:
                UpdateReady();
                break;
            case State.Empty:
                UpdateEmpty();
                break;
            case State.Reloading:
                UpdateReloading();
                break;
        }
    }
    
    private void UpdateReady()
    {
    
    }

    private void UpdateEmpty()
    {

    }

    private void UpdateReloading()
    {

    }

    private IEnumerator CoShotEffect(Vector3 hitPosition)
    {
        audioSource.PlayOneShot(gunData.shootClip);

        muzzleEffect.Play();
        shellEffect.Play();
        lineRender.enabled = true;
        lineRender.SetPosition(0 , firePosition.position);
        lineRender.SetPosition(1, hitPosition);

        yield return new WaitForSeconds(.2f);

        lineRender.enabled = false;
    }

    public void Fire()
    {
        if(currentState == State.Ready && Time.time > lastFireTime + gunData.timeBetFire)
        {
            lastFireTime = Time.time;
            Shoot();    
        }
    }

    private void Shoot()
    {
        if (magAmmo <= 0)
        {
            currentState = State.Empty;
            return;
        }

        Vector3 hitPosition = Vector3.zero;

        RaycastHit hit;
        if(Physics.Raycast(firePosition.position , firePosition.forward, out hit , gunData.fireDistance)) // 반직선의 개념 적용
        {
            hitPosition = hit.point;
            var target = hit.collider.GetComponent<IDamageAble>();

            target?.OnDamage(gunData.damage , hit.point , hit.normal);
        }else
        {
            hitPosition = firePosition.position + firePosition.forward * gunData.fireDistance;
        }

        --magAmmo;
        uiManager.SetAmmoText(magAmmo, ammoRemain);
        StartCoroutine(CoShotEffect(hitPosition));
    }

    public bool Reload()
    {
        if (ammoRemain <= 0 || currentState == State.Reloading) return false;

        audioSource.PlayOneShot(gunData.reloadClip);
        currentState = State.Reloading;

        StartCoroutine(CoReload());

        return true;
    }

    private IEnumerator CoReload()
    {
        yield return new WaitForSeconds(gunData.reloadTime);
        currentState = State.Ready;

        if (ammoRemain >= gunData.magCapacity)
        {
            ammoRemain -= gunData.magCapacity - magAmmo;
            magAmmo = gunData.magCapacity;
        }
        else
        {
            magAmmo = ammoRemain;
            ammoRemain = 0;
        }
        uiManager.SetAmmoText(magAmmo, ammoRemain);
    }

    public void IncreaseAmmo(int amount)
    {
        ammoRemain = Mathf.Min(ammoRemain + amount, gunData.startAmmoRemain);
        audioSource.PlayOneShot(itemPickUpClip);
        uiManager.SetAmmoText(magAmmo, ammoRemain);
    }
}
