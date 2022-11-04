using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerAttack : NetworkBehaviour
{

    public float attackTime = 0f;
    private GameObject hitbox;
    private bool attacking = false;

    private float attackLength = 0.25f;
    private float attackCooldownLength = 0.25f;

    private void Start()
    {
        hitbox = gameObject.transform.GetChild(1).gameObject;
    }

    private void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown("space") && attackTime + attackCooldownLength < Time.time)
        {
            attackTime = Time.time;
            attacking = true;
            RequestFireServerRpc(attacking);
            
        }
        if (attacking == true && attackTime + attackLength < Time.time)
        {
            attacking = false;
            RequestFireServerRpc(attacking);
        }
    }

    [ServerRpc]
    private void RequestFireServerRpc(bool attacking)
    {
        FireClientRpc(attacking);
    }

    [ClientRpc]
    private void FireClientRpc (bool attacking)
    {
        if (attacking)
        {
            ExecuteAttack();
        }
        else
        {
            EndAttack();
        }
    }

    private void ExecuteAttack()
    {
        hitbox.SetActive(true);
    }

    private void EndAttack()
    {
        hitbox.SetActive(false);
    }
}