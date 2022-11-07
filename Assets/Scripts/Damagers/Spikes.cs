using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : Damager
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            player.TakeDamage();
    }
}
