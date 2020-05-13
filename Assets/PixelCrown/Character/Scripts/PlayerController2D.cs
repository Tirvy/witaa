using System;
using UnityEngine;


namespace PixelCrown
{
    // Control structure
    [Serializable]
    public class ConfigurationControl
    {
        [Tooltip("Enable character to be controlled by the player input")]
        public bool enableControl = true;

        [Tooltip("Jump button identifier")]
        public string controlJump = "Jump";

        [Tooltip("Walk toggle button identifier")]
        public string controlWalkToggle = "WalkToggle";

        [Tooltip("Horizontal axis identifier")]
        public string controlAxisHorizontal = "Horizontal";

        [Tooltip("Enable character to be controlled by the player input")]
        public bool invertHorizontal = false;

        [Tooltip("Vertical axis identifier")]
        public string controlAxisVertical = "Vertical";

        [Tooltip("Enable character to be controlled by the player input")]
        public bool invertVertical = false;
    }


    public class PlayerController2D : PlayerCharacterControl
    {

        [SerializeField]
        public ConfigurationControl m_control;

        // True if the jump button is pressed
        private bool m_pressedJump = false;

        // True if the crouch button is pressed
        private bool m_pressedCrouch = false;

        // True toggle to run or walk
        // Toggle to walk when enableAlwaysRun is true, toggle to run when enableAlwaysRun is false
        private bool m_pressedWalkToggle = false;

        // horizontal direction
        private float m_horizontalMove = 0f;

        // Local Character movement component
        private CharacterMovement2D m_characterMovement;

        private void Start()
        {
            m_characterMovement = GetComponent<CharacterMovement2D>();
        }

        void OnDrawGizmos()
        {
            // Dont draw plz
        }

        void OnDrawGizmosSelected()
        {
            // Dont draw plz
        }

        private void Update()
        {
            if (!m_characterMovement)
            {
                return;
            }

            // Check the controls
            CheckControls();
        }

        private void FixedUpdate()
        {
            if (!m_characterMovement)
            {
                return;
            }

            // Move the character
            m_characterMovement.Move(m_horizontalMove, m_pressedCrouch, m_pressedJump, m_pressedWalkToggle);
        }

        private void CheckControls()
        {
            if (!m_control.enableControl)
            {
                return;
            }

            m_pressedJump = false;
            m_pressedWalkToggle = false;

            if (m_control.controlAxisHorizontal != "")
            {
                m_horizontalMove = GetAxisRaw(m_control.controlAxisHorizontal);
                if (m_control.invertHorizontal)
                {
                    m_horizontalMove = -m_horizontalMove;
                }
            }

            if (m_characterMovement.m_activation.enableWalk && m_control.controlWalkToggle != "" && GetButton(m_control.controlWalkToggle))
            {
                m_pressedWalkToggle = true;
            }

            if (m_characterMovement.m_activation.enableJump && m_control.controlJump != "" && GetButton(m_control.controlJump))
            {
                m_pressedJump = true;
            }


            if (m_control.invertVertical)
            {
                if (m_characterMovement.m_activation.enableCrouch && GetAxis(m_control.controlAxisVertical) < -0.1f)
                {
                    m_pressedCrouch = true;
                } else
                {
                    m_pressedCrouch = false;
                }
            } else
            {
                if (m_characterMovement.m_activation.enableCrouch && GetAxis(m_control.controlAxisVertical) > 0.1f)
                {
                    m_pressedCrouch = true;
                }
                else
                {
                    m_pressedCrouch = false;
                }
            }

        }

    }
    // END of PixelCrown namespace
}