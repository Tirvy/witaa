using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Changelog:
// Create PlayerCharacterControl component for player controlled objects
// Control manager with multiplayer feature
// Stops register input when the input is used in multiple components

namespace PixelCrown
{

    [Serializable]
    public class PlayerControlList
    {
        [Tooltip("Player object to be detected espcially in multiplayer mode")]
        public GameObject m_player;

        [Tooltip("List of buttons for the player")]
        public string[] m_buttons;

        public GameObject GetPlayer(string button)
        {
            for (int i = 0; i < m_buttons.Length; i++)
            {
                if (m_buttons[i] == button)
                {
                    return m_player;
                }
            }
            return null;
        }
    }

    public class PlayerCharacterControl: MonoBehaviour
    {

        [Tooltip("Control input prefix for the player. In a multiplayer mode, you can set this prefix if your input names have different prefixes for each players. Leave blank for single player mode.")]
        public string m_controlInputPrefix = "";

        private PlayerControlManager m_controlManager;

        private void Awake()
        {
            m_controlManager = PlayerControlManager.instance;
        }

        public void SetInputPrefix(string prefix)
        {
            m_controlInputPrefix = prefix;
        }

        protected float GetAxis(string axisName)
        {
            if (m_controlManager)
            {
                return m_controlManager.GetAxis(m_controlInputPrefix + axisName);
            }
            return Input.GetAxis(m_controlInputPrefix + axisName);
        }

        protected float GetAxisRaw(string axisName)
        {
            if (m_controlManager)
            {
                return m_controlManager.GetAxisRaw(m_controlInputPrefix + axisName);
            }
            return Input.GetAxisRaw(m_controlInputPrefix + axisName);
        }

        protected bool GetButton(string buttonName)
        {
            if (m_controlManager)
            {
                return m_controlManager.GetButtonOnce(m_controlInputPrefix + buttonName);
            }
            return Input.GetButton(m_controlInputPrefix + buttonName);
        }

        protected bool GetButtonDown(string buttonName)
        {
            if (m_controlManager)
            {
                return m_controlManager.GetButtonDownOnce(m_controlInputPrefix + buttonName);
            }
            return Input.GetButtonDown(m_controlInputPrefix + buttonName);
        }

        protected bool GetButtonUp(string buttonName)
        {
            if (m_controlManager)
            {
                return m_controlManager.GetButtonUpOnce(m_controlInputPrefix + buttonName);
            }
            return Input.GetButtonUp(m_controlInputPrefix + buttonName);
        }
    }

    public class PlayerControlManager : MonoBehaviour
    {
        public PlayerControlList[] m_players;

        [Tooltip("If the manager is in single player mode")]
        public bool m_isSinglePlayer = true;

        static public PlayerControlManager instance;

        private List<string> m_stoppedButton = new List<string>();
        private List<string> m_stoppedButtonUp = new List<string>();
        private List<string> m_stoppedButtonDown = new List<string>();

        private void Awake()
        {
            PlayerControlManager.instance = this;
        }

        void Update()
        {
            m_stoppedButton = new List<string>();
            m_stoppedButtonUp = new List<string>();
            m_stoppedButtonDown = new List<string>();
        }

        public bool IsSinglePlayer()
        {
            return m_isSinglePlayer;
        }

        public float GetAxis(string axisName)
        {
            return Input.GetAxis(axisName);
        }

        public float GetAxisRaw(string axisName)
        {
            return Input.GetAxisRaw(axisName);
        }

        public GameObject GetPlayer(string button)
        {
            for (int i = 0; i < m_players.Length; i++)
            {
                GameObject player = m_players[i].GetPlayer(button);
                if (player != null)
                {
                    return player;
                }
            }
            return null;
        }
        
        public bool GetButton(string buttonName, GameObject player = null)
        {
            if (m_stoppedButton.Contains(buttonName))
            {
                return false;
            }
            if (!m_isSinglePlayer && player != null && GetPlayer(buttonName) != player)
            {
                return false;
            }
            return Input.GetButton(buttonName);
        }

        public bool GetButtonOnce(string buttonName)
        {
            bool pressed = Input.GetButton(buttonName);
            if (pressed)
            {
                StopButton(buttonName);
            }
            return pressed;
        }
        
        public bool GetButtonDown(string buttonName, GameObject player = null)
        {
            if (m_stoppedButton.Contains(buttonName))
            {
                return false;
            }
            if (!m_isSinglePlayer && player != null && GetPlayer(buttonName) != player)
            {
                return false;
            }
            return Input.GetButtonDown(buttonName);
        }

        public bool GetButtonDownOnce(string buttonName)
        {
            bool pressed = Input.GetButtonDown(buttonName);
            if (pressed)
            {
                StopButton(buttonName);
            }
            return pressed;
        }

        public bool GetButtonUp(string buttonName, GameObject player = null)
        {
            if (m_stoppedButton.Contains(buttonName))
            {
                return false;
            }
            if (!m_isSinglePlayer && player != null && GetPlayer(buttonName) != player)
            {
                return false;
            }
            return Input.GetButtonUp(buttonName);
        }

        public bool GetButtonUpOnce(string buttonName)
        {
            bool pressed = Input.GetButtonUp(buttonName);
            if (pressed)
            {
                StopButton(buttonName);
            }
            return pressed;
        }

        public void StopButton(string buttonName)
        {
            m_stoppedButton.Add(buttonName);
        }
    }

}
