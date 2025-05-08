using Unity.Netcode;
using UnityEngine;

public class BaseGun : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 20f;

    private void Update()
    {
        if (!IsOwner) return;

        if (IsOwner && Input.GetButtonDown("Fire1"))
        {
            Vector3 pos = firePoint.position;
            Vector3 dir = firePoint.forward;
            ShootServerRpc(pos, dir);
        }
    }

    [ServerRpc]
    void ShootServerRpc(Vector3 position, Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, position, Quaternion.LookRotation(direction));
        NetworkObject netBullet = bullet.GetComponent<NetworkObject>();
        netBullet.Spawn(true); // Important: true = owned by server

        ApplyBulletForceClientRpc(netBullet.NetworkObjectId, direction);
    }

    [ClientRpc]
    void ApplyBulletForceClientRpc(ulong bulletId, Vector3 direction)
    {
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(bulletId, out var netObj))
        {
            Rigidbody rb = netObj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(direction * bulletForce, ForceMode.Impulse);
            }
        }
    }
}
