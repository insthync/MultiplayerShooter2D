using System;
using UnityEngine;
using LiteNetLibManager;

namespace UnityStandardAssets._2D
{
    public class Restarter : MonoBehaviour
    {
        private LiteNetLibGameManager networkManager;
        private void Start()
        {
            networkManager = FindObjectOfType<LiteNetLibGameManager>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Check hit character at server
            if (!networkManager.IsServer)
                return;
            var character = other.GetComponent<PlatformerCharacter2D>();
            if (character != null)
            {
                // Make character dead
                character.hp.Value = 0;
            }
        }
    }
}
