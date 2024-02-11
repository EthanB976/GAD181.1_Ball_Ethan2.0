using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDeath : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        var PlayerMovement = other.collider.GetComponent<PlayerMovement>();
        if (PlayerMovement != null)
        {
            PlayerMovement.Die();
        }
    }
}
