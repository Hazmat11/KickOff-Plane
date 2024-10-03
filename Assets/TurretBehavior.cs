using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TurretBehavior : MonoBehaviour
{
    public float rotationSpeed;

    public float shootSpeed;

    public float shootSpread;

    public float angleToShoot = 10;

    public float rotationFrequency;

    public float range;
    private float rotationTimer;

    private Vector3 rotationTarget;

    public GameObject bullet;

    public DebugController player;

    private bool playerInRange;
    
    public float bulletSpeed = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        player = DebugController.instance;
        
        GameManager.instance.AddEnemy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

    // Update is called once per frame
    void Update()
    {
        playerInRange = Vector3.Distance(player.transform.position, transform.position) < range;

        if (playerInRange)
        {
            LockPlayer();
            rotationTimer = 0;
            return;
        }
        rotationTimer += Time.deltaTime;
        if (rotationTimer > rotationFrequency)
            NewRotationTarget();
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.Euler(rotationTarget),rotationSpeed);
    }

    void NewRotationTarget()
    {
        rotationTarget = new Vector3(0,Random.Range(0,360),0);
        rotationTimer = 0;
    }

    private Quaternion tempQuaternion;
    void LockPlayer()
    {
        Debug.DrawRay(transform.position,new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z) - transform.position,Color.magenta);
        rotationTarget = new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z) - transform.position;
        
        tempQuaternion = Quaternion.AngleAxis(Mathf.Atan2(rotationTarget.x,rotationTarget.z) * Mathf.Rad2Deg,Vector3.up);
        tempQuaternion = Quaternion.LookRotation(rotationTarget);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            tempQuaternion
            ,rotationSpeed);

        if (Mathf.Abs(Mathf.Atan2(rotationTarget.x,rotationTarget.z) * Mathf.Rad2Deg) - Mathf.Abs(Mathf.Atan2(transform.forward.x,transform.forward.z) * Mathf.Rad2Deg) 
            < angleToShoot)
        {
            Shoot();
        }
    }

    private float shootTimer;
    void Shoot()
    {
        shootTimer -= Time.deltaTime * shootSpeed;
        if (shootTimer <= 0)
        {
            shootTimer = 1;
            var tempBullet = Instantiate(bullet, transform.position + transform.up * 0.75f + transform.forward * 1f, Quaternion.identity);
            tempBullet.transform.LookAt(player.transform);
            tempBullet.transform.Rotate(Random.Range(-shootSpread,shootSpread),Random.Range(-shootSpread,shootSpread),Random.Range(-shootSpread,shootSpread));
            tempBullet.GetComponent<Rigidbody>().velocity = tempBullet.transform.forward * bulletSpeed;

        }
        
    }
}
