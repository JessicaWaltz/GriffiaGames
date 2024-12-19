using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundMusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    // we have starting music, we want badguy music, game action music, no sound for pause.
    // for pause: Time. timescale =0
    public AudioClip miniBossSong;
    public AudioClip BossSong;
    public AudioClip ActionSong;
    public AudioClip PeacefullSong;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
