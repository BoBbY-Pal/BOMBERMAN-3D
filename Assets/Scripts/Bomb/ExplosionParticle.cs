using System;
using Interfaces;
using UnityEngine;
using Utilities;

// This script will be used for manipulating particles if needed in future.
public class ExplosionParticle : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, .7f);
    }

    private void OnTriggerEnter(Collider collider)
    {
        GameLogManager.CustomLog("Collided");
        
        IDestructible destructible = collider.gameObject.GetComponent<IDestructible>();
        if (destructible != null)
        {
            destructible.DestroyObject();
        }
    }
}
