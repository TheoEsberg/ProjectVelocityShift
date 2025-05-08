using Unity.Netcode;
using UnityEngine;

public class BaseBullet : NetworkBehaviour
{
    public float lifetime = 5f;

    private void Start()
    {
        if (IsServer)
        {
            Invoke(nameof(DespawnSelf), lifetime);
        }
    }

    void DespawnSelf()
    {
        NetworkObject.Despawn();
    }
}
