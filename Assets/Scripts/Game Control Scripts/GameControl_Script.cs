using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl_Script : MonoBehaviour
{
    [SerializeField] private KeyCode escapeKey;
    [SerializeField] private KeyCode ResetKey;

    [SerializeField] public KeyCode Q_Ability_Keycode;
    [SerializeField] public KeyCode W_Ability_Keycode;
    [SerializeField] public KeyCode E_Ability_Keycode;
    [SerializeField] public KeyCode R_Ability_Keycode;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(escapeKey))
        {
            Application.Quit();
        }

        if (Input.GetKey(ResetKey))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
