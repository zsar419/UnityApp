using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicScript : MonoBehaviour
{

    public AudioSource audioTrack;
    public List<AudioClip> tracks;
    System.Random rnd = new System.Random();


    void Start()
    {
        tracks = new List<AudioClip>();
        tracks.Add((AudioClip)Resources.Load("Audio/Music/Conceal"));
        tracks.Add((AudioClip)Resources.Load("Audio/Music/Constricted"));
        tracks.Add((AudioClip)Resources.Load("Audio/Music/Corrupted"));

        audioTrack.loop = false;
        PlayRandomTrack();
    }

    void FixedUpdate()
    {
        //Once a track has stopped play another one
        if (!audioTrack.isPlaying)
        {
            PlayRandomTrack();
        }
    }

    /// <summary>
    /// Plays a random audio track.
    /// </summary>
    void PlayRandomTrack()
    {
        int trackIndex = rnd.Next(0, tracks.Count-1);

        audioTrack.clip = tracks[trackIndex];
        audioTrack.Play();
    }
}
