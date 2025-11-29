using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicDropdown : MonoBehaviour
{
    public Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        // YOU CAN USE AddSong() TO ADD UNLOCKED SONGS TO DROPBAR WHEN THE LEVEL SELECTOR SCENE LOADS - MB
        // *** ONLY PROBLEM IS WE PROBABLY NEED A WAY TO FIGURE OUT WHEN A LEVEL IS BEAT ***

        AddSong("Minecraft Song");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // WHEN SONG IS SELECTED THIS FUNCTION IS EXECUTED - MB
    void OnNewOptionPicked()
    {
        string selectedSong = dropdown.options[dropdown.value].text;
        // YOU CAN USE THIS TO SELECT THE CUSTOM SONG TO USE IN YOUR STATIC MUSIC MANAGER CLASS - MB
    }

    // YOU CAN USE THIS FUNCTION TO ADD SONG TO DROPBAR - MB
    void AddSong(string song)
    {
        dropdown.options.Add(new Dropdown.OptionData(song));
    }
}
