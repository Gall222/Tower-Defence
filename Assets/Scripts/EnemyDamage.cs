using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int hitPoints = 5;
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject finishVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] AudioClip hitSFX;
    [SerializeField] AudioClip deathSFX;

    AudioSource audioSource;
    BaseHealth baseHealth;
    // Start is called before the first frame update
    void Start()
    {
        baseHealth = FindObjectOfType<BaseHealth>();
        audioSource = FindObjectOfType<AudioSource>();
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoints <= 0)
        {
            Death(false);
        }
        //Destroy(other.gameObject);
    }

    public void Death(bool isPathOwer)
    {
        if (deathVFX)
        {
            GameObject vfx;
            if (isPathOwer)
                vfx = Instantiate(finishVFX, transform.position, transform.rotation);
            else
                vfx = Instantiate(deathVFX, transform.position, transform.rotation);
            var particles = vfx.GetComponent<ParticleSystem>();
            vfx.transform.parent = transform.parent;
            float delay = particles.main.duration;
            audioSource.PlayOneShot(deathSFX);
            Destroy(vfx.gameObject, delay);
        }

        baseHealth.AddScore(5);
        Destroy(gameObject);
    }

    void ProcessHit()
    {
        if (hitVFX)
        {
            var vfx = Instantiate(hitVFX, transform.position, transform.rotation);
            vfx.transform.parent = transform.parent;
            float delay = vfx.GetComponent<ParticleSystem>().main.duration;
            Destroy(vfx.gameObject, delay);
        }
        audioSource.PlayOneShot(hitSFX);
        hitPoints--; ;
    }
}
