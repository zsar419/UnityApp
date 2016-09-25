using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombieAudioScript : MonoBehaviour {

    public List<AudioClip> sounds;
    public AudioSource source;
    bool keepPlaying = true;
    System.Random rnd = new System.Random();

    //Range of time for which zombies should randomly make another noise. 
    public int minWaitSeconds;
    public int maxWaitSeconds;

	void Start () {
        sounds = new List<AudioClip>();
        sounds.Add((AudioClip)Resources.Load("Audio/Zombie Sounds/zombieangry"));
        sounds.Add((AudioClip)Resources.Load("Audio/Zombie Sounds/zombieangry2"));
        sounds.Add((AudioClip)Resources.Load("Audio/Zombie Sounds/zombieattack"));
        sounds.Add((AudioClip)Resources.Load("Audio/Zombie Sounds/zombiegroancloser"));
        sounds.Add((AudioClip)Resources.Load("Audio/Zombie Sounds/zombiegroandistant"));
        sounds.Add((AudioClip)Resources.Load("Audio/Zombie Sounds/zombiegrunt1"));
        sounds.Add((AudioClip)Resources.Load("Audio/Zombie Sounds/zombiegrunt2"));
        sounds.Add((AudioClip)Resources.Load("Audio/Zombie Sounds/zombiehighgroan"));
        sounds.Add((AudioClip)Resources.Load("Audio/Zombie Sounds/zombielowgroan"));
        source.clip = sounds[0];

        StartCoroutine(PlayRandomSound());

	}
	
    /// <summary>
    /// Plays a random zombie noise for the zombie. The next noise is invoked after a random amount of time has elapsed. 
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayRandomSound()
    {
        while (keepPlaying)
        {
            source.clip = sounds[rnd.Next(0, sounds.Count-1)]; //Play a random clip
            source.Play();
            yield return new WaitForSeconds(rnd.Next(minWaitSeconds, maxWaitSeconds)); //wait a random am
        }
    }
}
