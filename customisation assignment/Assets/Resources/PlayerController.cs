using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace movement
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerController controller;
        [SerializeField] private Transform spawnPoint;

        public float movementSpeed;
        public float rotateSpeed = 0.01F;
        public Animator animation;

        [System.Serializable]
        public class KeyBind
        {
            public string keyName;
            public Text keyDisplayText;
            public string defaultKey;
        }

        [SerializeField] private Dictionary<string, KeyCode> keyBinds = new Dictionary<string, KeyCode>();

        public KeyBind[] defaultSetup;
        public Text[] TextDescriptor;
        public GameObject currentKey;
        public Color32 changedKey = Color.white;
        public Color32 selectedKey = Color.cyan;
        [SerializeField] private int arrayIndex;


        private void Start()
        {
            RefreshKeys();

            //movement speed affected by stats
            movementSpeed = DataMaster.characterStats[0][2];
        }

        public void SaveKeys()
        {
            foreach (var key in keyBinds)
            {
                PlayerPrefs.SetString(key.Key, key.Value.ToString());
            }
            PlayerPrefs.Save();
        }

        public void ChangeKey(GameObject clickedKey)
        {
            currentKey = clickedKey;

            if (clickedKey != null)
            {
                currentKey.GetComponent<Image>().color = selectedKey;
            }

        }

        public void OnGUI()
        {
            string newKey = "";
            Event e = Event.current;

            if (currentKey != null)
            {
                if (e.isKey)
                {
                    newKey = e.keyCode.ToString();

                }

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    newKey = "LeftShift";
                }

                if (Input.GetKey(KeyCode.RightShift))
                {
                    newKey = "RightShift";
                }

                if (newKey != "")
                {

                    Debug.Log(keyBinds[TextDescriptor[arrayIndex].text.ToString()]);
                    keyBinds[TextDescriptor[arrayIndex].text.ToString()] = (KeyCode)System.Enum.Parse(typeof(KeyCode), newKey);
                    defaultSetup[arrayIndex].keyDisplayText.text = newKey.ToString();
                    currentKey.GetComponent<Image>().color = changedKey;
                    currentKey = null;
                    SaveKeys();
                    RefreshKeys();

                }
            }
        }

        public void RefreshKeys()
        {
            keyBinds.Clear();

            for (int i = 0; i < defaultSetup.Length; i++)
            {
                keyBinds.Add(defaultSetup[i].keyName, (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(defaultSetup[i].keyName, defaultSetup[i].defaultKey)));
                defaultSetup[i].keyDisplayText.text = keyBinds[defaultSetup[i].keyName].ToString();
            }
        }

        public void Keyint(int select)
        {
            arrayIndex = select;
        }

        void FixedUpdate()
        {
            //animation trigger
            CharacterController controller = GetComponent<CharacterController>();

            if (Input.GetKey(keyBinds["Forwards"]))
            {
                animation.SetFloat("Speed", 1);
            }
            else
            {
                animation.SetFloat("Speed", 0);
            }

            //crouch walk and run speeds
            if (Input.GetKey(keyBinds["Run"]) && player.AssignableStatManager.regenStats[1][1] > 5)
            {
                // Move forward / backward
                Vector3 forward = transform.TransformDirection(Vector3.forward) * movementSpeed * 1.5f;
                controller.SimpleMove(forward);
            }
            if (Input.GetKey(keyBinds["Crouch"]))
            {
                Vector3 forward = transform.TransformDirection(Vector3.forward) * movementSpeed * 0.5f;
                controller.SimpleMove(forward);
            }

            if(Input.GetKey(keyBinds["Forwards"]))
            {
                // Move forward / backward
                Vector3 forward = transform.TransformDirection(Vector3.forward) * movementSpeed;
                controller.SimpleMove(forward);
            }

            if (Input.GetKey(keyBinds["Backwards"]))
            {
                // Move forward / backward
                Vector3 forward = transform.TransformDirection(Vector3.forward) * -movementSpeed;
                controller.SimpleMove(forward);
            }

            // Rotate around y - axis
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

            if (player.AssignableStatManager.dead)
            {
                controller.transform.position = spawnPoint.position;
            }
        }
    }
}



