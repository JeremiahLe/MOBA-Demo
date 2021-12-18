using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl_Script : MonoBehaviour
{
    // Init stuff
    TrackHeroInfo_HUDWindow HUDWindow;

    [Header("Game Settings")]
    [SerializeField] private KeyCode escapeKey;
    [SerializeField] private KeyCode ResetKey;

    [Header("Gameplay Binds")]
    [SerializeField] public KeyCode Q_Ability_Keycode;
    [SerializeField] public KeyCode W_Ability_Keycode;
    [SerializeField] public KeyCode E_Ability_Keycode;
    [SerializeField] public KeyCode R_Ability_Keycode;

    [SerializeField] public KeyCode ShowDetailsWindow_Keycode;

    private void Awake()
    {
        // if in game
        HUDWindow = GameObject.FindGameObjectWithTag("HUDWindowTag").GetComponent<TrackHeroInfo_HUDWindow>();
    }

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

        if (Input.GetKeyDown(ShowDetailsWindow_Keycode))
        {
            HUDWindow.TurnOffWindow();
        }
    }

}

