using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Catapult : Spawnable {

    [Tooltip("Projectile that catapult will shoot.")]
    public GameObject ProjectilePrefab;

    [Tooltip("Projectile speed when shot.")]
    public float ProjectileSpeed;

    [SerializeField]
    [Tooltip("Transform where projectile for catapult is spawned.")]
    public Transform ProjectileSpawn;

    private Animator m_Animator; 

	// Use this for initialization
	void Start () {
        m_Animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        // Check to make sure this client owns this player.
        if(!hasAuthority)
        {
            return;
        }

		if(Input.GetKeyUp(KeyCode.Space))
        {
            RpcPlayLaunchAnimation();
            CmdFire();
        }
	}

    [Command]
    private void CmdFire()
    {
        // Create the Rock from rock prefab
        GameObject projectile = (GameObject)Instantiate(
            ProjectilePrefab,
            ProjectileSpawn.position,
            ProjectileSpawn.rotation);

        // Add velocity to the bullet
        projectile.GetComponent<Rigidbody>().velocity = projectile.transform.up * ProjectileSpeed;

        // Spawns projectile on the clients
        NetworkServer.Spawn(projectile);

        // Destroy the bullet after 2 seconds
        Destroy(projectile, 2.0f);
    }

    [ClientRpc]
    public void RpcPlayLaunchAnimation()
    {
        if (m_Animator != null)
        {
            m_Animator.SetTrigger("Launched");
        }
    }

    [Command]
    public void CmdPlayLaunchAnimation()
    {
        if (!hasAuthority)
        {
            return;
        }

        RpcPlayLaunchAnimation();
    }
}
