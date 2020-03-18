using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {

    #region Fields
    public bool alerted;
    public Transform aimTarget;
    public Transform gunXform;
    public Transform domeXform;
    public GameObject bulletPrefab;
    public float turretSpeedOn;
    public float turretSpeedOff;
    public LayerMask rayCastMask;
    public float gunInterval;

    private float interpolator = 1f;
    private float gunTimer = 0f;
    #endregion

    #region Properties	
    #endregion

    #region Methods
    #region Unity Methods

    // Use this for internal initialization
    void Awake () {
		
	}
		
	// Use this for external initialization
	void Start () {
        if (aimTarget == null)
            aimTarget = GameObject.FindWithTag("Player").transform;
    }
		
	// Update is called once per frame
	void Update () {
        gunTimer -= Time.deltaTime;

        if (alerted)
        {
            interpolator = Mathf.Lerp(interpolator, 1.0f, turretSpeedOn);

            RaycastHit hit;
            if(Physics.Raycast(gunXform.position, gunXform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, rayCastMask))
            {
                if (hit.collider.tag == ("Player") && gunTimer < 0)
                {
                    Instantiate(bulletPrefab, gunXform.position, Quaternion.identity);
                    gunTimer = gunInterval;
                }
            }
        }
        else
        {
            interpolator = Mathf.Lerp(interpolator, 0.0f, turretSpeedOff);
        }

        Vector3 aimVec = aimTarget.position - gunXform.position;
        Quaternion gunQuat = Quaternion.LookRotation(aimVec);
        gunXform.rotation = Quaternion.Slerp(Quaternion.identity, gunQuat, interpolator);
        aimVec.y = 0.0f;
        Quaternion domeQuat = Quaternion.LookRotation(aimVec);
        domeXform.rotation = Quaternion.Slerp(Quaternion.identity, domeQuat, interpolator);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            alerted = true;
    }

    private void OnTriggerExit(Collider other)
    {
        alerted = false;
    }
    #endregion
    #endregion
}