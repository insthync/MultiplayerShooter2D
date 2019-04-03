using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiteNetLibManager;

namespace UnityStandardAssets._2D
{
    public class Bullet : LiteNetLibBehaviour
    {
        public PlatformerCharacter2D shooter;
        public sbyte shootDirection;
        public int damage = 5;
        public float speed = 4;
        public float lifeTime = 2;
        private Rigidbody2D m_Rigidbody2D;
        // Use this for initialization
        void Start()
        {
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            if (IsServer)
                NetworkDestroy(lifeTime);
        }

        void FixedUpdate()
        {
            m_Rigidbody2D.velocity = new Vector2(shootDirection * speed, 0);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Check hit character at server
            if (!IsServer)
                return;

            var character = other.GetComponent<PlatformerCharacter2D>();
            if (character != null && shooter != character)
            {
                // Reduce character hp
                character.hp.Value -= damage;
                // Destroy this bullet so all clients will not see it
                NetworkDestroy();
            }
        }
    }
}
