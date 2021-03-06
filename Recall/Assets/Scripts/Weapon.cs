﻿using System.Collections.Generic;
using UnityEngine;
using System.Collections;


public class Weapon : MonoBehaviour
{
    public float fireRate = 0;
    public int Damage = 1;
    public LayerMask whatToHit;

    public Transform BulletTrailPrefab;
    public Transform MuzzleFlashPrefab;

    float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;

    public float timeToFire = 0;
    Transform firePoint;

    Jogador jogador;


    // Use this for initialization
    void Awake()
    {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogError("No firePoint? WHAT?!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }

    public void Shoot()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 20, whatToHit);

        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100, Color.cyan);

        if (Time.time >= timeToSpawnEffect)
        {
            Effect();
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }




        if (hit.collider != null)
        {
            Agente agente = hit.collider.GetComponent<Agente>();
            Sentinela sentinela = hit.collider.GetComponent<Sentinela>();

            if (agente != null)
            {
                agente.DanoAgente(Damage);
            }

            else if (sentinela != null)
            {
                sentinela.DanoSentinela(Damage);
            }
        }
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Doug/Gun shots/Handgun", GetComponent<Transform>().position); // som do tiro
    }

    void Effect()
    {
        Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);

        Transform clone = Instantiate(MuzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
        clone.parent = firePoint;
        float size = Random.Range(0.6f, 0.9f);
        clone.localScale = new Vector3(size, size, size);
        Destroy(clone.gameObject, 0.02f);
    }
}



