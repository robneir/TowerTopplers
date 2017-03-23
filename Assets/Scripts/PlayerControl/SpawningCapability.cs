using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UI;

namespace PlayerControl
{
    public class SpawningCapability : NetworkBehaviour
    {
        [Tooltip("List of things that can be spawned by player.")]
        public List<Spawnable> SpawnablesList;

        [SerializeField]
        [Tooltip("Max spawn raycast will travel.")]
        private float MaxRaycastDistance;

        [SerializeField]
        [Tooltip("Initial distance object is spawned from camera.")]
        private float StartSpawnDistFromCamera;

        [SerializeField]
        [Tooltip("Raycast layer for spawning object.")]
        private LayerMask raycastLayerMask;

        private Spawnable mCurrentSpawnableInstance; // local instance of current spawnable instance.
        private Spawnable mCurrentSpawnablePrefab; // prefab used to spawn instance of spawnable on server.
        private SpawnPanel m_SpawnPanel; // UI panel that shows what player can spawn.

        // Use this for initialization
        void Start()
        {
            m_SpawnPanel = FindObjectOfType<SpawnPanel>();
            if (m_SpawnPanel != null)
            {
                if (isLocalPlayer)
                {
                    m_SpawnPanel.PopulateSpawnables(this);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!isLocalPlayer)
            {
                return;
            }

            if (mCurrentSpawnableInstance)
            {
                FollowCursor();
                CheckForCancellation();
                CheckForPlacement();
                Cursor.visible = false;
            }
        }

        private void CheckForPlacement()
        {
            if (!isLocalPlayer)
            {
                return;
            }

            // Make sure the placement is in a valid build area before placing.
            if (Input.GetButtonDown("Fire1") && mCurrentSpawnableInstance.IsInValidBuildArea())
            {
                NetworkIdentity owningPlayer = GetComponent<NetworkIdentity>();
                if (owningPlayer != null)
                {
                    CmdSpawnOnServer(0, //CHANGE THIS SO NOT HARD CODED! -from Rob
                        mCurrentSpawnableInstance.transform.position,
                        mCurrentSpawnableInstance.transform.rotation,
                        owningPlayer);
                }
                Cursor.visible = true;
            }
        }

        private void CheckForCancellation()
        {
            if (!isLocalPlayer)
            {
                return;
            }

            if (Input.GetButtonDown("Cancel"))
            {
                Destroy(mCurrentSpawnableInstance.gameObject);
                mCurrentSpawnableInstance = null;
                Cursor.visible = true;
            }
        }

        [Command]
        public void CmdSpawnOnServer(int _spawnIndex, Vector3 _position, Quaternion _rotation, NetworkIdentity _owningPlayerID)
        {
            Spawnable spawningObj = SpawnablesList[_spawnIndex];
            Spawnable spawningInstance = (Spawnable)Instantiate(spawningObj, _position, _rotation);
            NetworkServer.Spawn(spawningInstance.gameObject);
            //mCurrentSpawnableInstance = null;
        }

        [ClientRpc]
        public void RpcSpawn(int spawnIndex, Vector3 _position, Quaternion rotation, NetworkIdentity _owningPlayerID)
        {
            Spawnable spawnablePrefab = SpawnablesList[spawnIndex];
            Spawnable spawnableInstace = (Spawnable)Instantiate(spawnablePrefab);
        }

        // Spawns locally. Then once it is actually placed spawn on server.
        public void Spawn(Spawnable spawnable)
        {
            if (!isLocalPlayer)
            {
                return;
            }

            // Destroy existing spawnable instance if there is one.
            if (mCurrentSpawnableInstance != null)
            {
                Destroy(mCurrentSpawnableInstance);
            }

            Debug.Log("Spawning Local " + spawnable.SpawnName);
            // Spawn new object locally and store the type of object it was.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 endOfRay = ray.origin + ray.direction.normalized * StartSpawnDistFromCamera;
            mCurrentSpawnableInstance = Instantiate(spawnable, endOfRay, Quaternion.identity);
            mCurrentSpawnablePrefab = spawnable;
        }

        private void FollowCursor()
        {
            if (!isLocalPlayer)
            {
                return;
            }

            RaycastHit hitInfo = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo, MaxRaycastDistance, raycastLayerMask))
            {
                GameObject hitObject = hitInfo.collider.gameObject;

                Quaternion normalAsRotation = Quaternion.Euler(hitInfo.normal);
                mCurrentSpawnableInstance.transform.position = hitInfo.point;
                mCurrentSpawnableInstance.transform.localRotation = normalAsRotation;
            }
        }
    }
}