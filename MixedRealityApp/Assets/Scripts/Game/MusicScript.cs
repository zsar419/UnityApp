using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicScript : MonoBehaviour
{
    public GameObject uiManager;
    public AudioSource audioTrack;
    public AudioSource heartBeat;
    public List<AudioClip> tracks;
    System.Random rnd = new System.Random();
    public float zombieCloseThreshold;

    private float closestDist;

    void Start()
    {
        tracks = new List<AudioClip>();
        tracks.Add((AudioClip)Resources.Load("Audio/Music/Conceal"));
        tracks.Add((AudioClip)Resources.Load("Audio/Music/Constricted"));
        tracks.Add((AudioClip)Resources.Load("Audio/Music/Corrupted"));
        tracks.Add((AudioClip)Resources.Load("Audio/Music/Responsible"));
        tracks.Add((AudioClip)Resources.Load("Audio/Music/Shadowed"));
        tracks.Add((AudioClip)Resources.Load("Audio/Music/Suspended"));
        tracks.Add((AudioClip)Resources.Load("Audio/Music/Within Sight"));

        audioTrack.loop = false;
        PlayRandomTrack();
    }

    void Update()
    {
        //Once a track has stopped play another one
        if (!audioTrack.isPlaying)
        {
            PlayRandomTrack();
        }

        var otherscript = uiManager.GetComponent<GameMain>();
        closestDist = otherscript.GetClosestDist();

        //increase heart-beat rate as zombie gets very close
        if (closestDist < zombieCloseThreshold )
        {

            heartBeat.pitch = Mathf.Lerp(heartBeat.pitch, 1.5f, 0.5f * Time.deltaTime);
        }
        else
        {
            heartBeat.pitch = 1;
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


    public void StopAllMusic()
    {
        audioTrack.Stop();
        heartBeat.Stop();
    }
}
