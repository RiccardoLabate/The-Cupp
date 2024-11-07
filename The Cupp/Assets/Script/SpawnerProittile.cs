using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class NemicoController : MonoBehaviour
{
    public GameObject enemy;
    public Transform player;

    public GameObject target;

    [SerializeField] private float timer = 5;
    private float bulletTime;

    public GameObject enemyBullet;
    public Transform spawnPoint;
    public float enemySpeed;

    public float turnRate;
    void Update()
    {
        Vector3 directionToTarget = target.transform.position - transform.position;
        directionToTarget.y = 0; // Project the direction onto the XZ plane

        // Calculate the angle between the forward vector and the target direction
        float angleToTarget = Vector3.SignedAngle(transform.forward, directionToTarget, Vector3.up);

        // Create a rotation around the Y-axis
        Quaternion rotation = Quaternion.AngleAxis(angleToTarget * Time.deltaTime * turnRate, Vector3.up);

        // Apply the rotation
        transform.rotation = rotation * transform.rotation;


        /*Vector3 targetDelta = target.transform.position - transform.position;
        float angleToTarget = Vector3.Angle(transform.forward, targetDelta);
        Vector3 turnAxis = Vector3.Cross(transform.forward, targetDelta);

        transform.RotateAround(transform.position, turnAxis, Time.deltaTime * turnRate * angleToTarget);*/
        Spara();
    }

    void Spara()
    {
        bulletTime -= Time.deltaTime;

        if (bulletTime > 0) return;

        bulletTime = timer;

        GameObject bulletobj = Instantiate(enemyBullet, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        Rigidbody bulletRig = bulletobj.GetComponent<Rigidbody>();
        bulletRig.AddForce(bulletRig.transform.forward * enemySpeed);

    }
}