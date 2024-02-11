using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var PlayerMovement = other.GetComponent<PlayerMovement>();
        if (PlayerMovement != null)
        {
            PlayerMovement.SetRespawnPoint((Vector2)transform.position);
        }
    }
}
