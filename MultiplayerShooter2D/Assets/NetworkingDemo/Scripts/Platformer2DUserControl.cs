using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using LiteNetLibManager;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : LiteNetLibBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!IsOwnerClient)
            {
                // If it is other player's character (not character with you currently play), don't update inputs
                return;
            }
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
            if (CrossPlatformInputManager.GetButtonDown("Fire1"))
                m_Character.Shoot();
        }


        private void FixedUpdate()
        {
            if (!IsOwnerClient)
            {
                // If it is other player's character (not character with you currently play), don't update inputs
                return;
            }
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump);
            m_Jump = false;
        }
    }
}
