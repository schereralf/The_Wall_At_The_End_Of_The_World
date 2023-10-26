using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    private Animator doorAnim;
    private bool doorOpen = false;

    public List<AudioClip> creaks = new List<AudioClip>();
    public AudioClip slam;
    public AudioSource source;


    private void Awake()
    {
        doorAnim=gameObject.GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        if(!doorOpen)
        {
            doorAnim.Play("Door1Open", 0, 0.0f);
            doorOpen=true;
            source.PlayOneShot(creaks[(Random.Range(0, creaks.Count))]);
        }
        else
        {
            doorAnim.Play("Door1Close", 0, 0.0f);
            doorOpen=false;
            source.PlayOneShot(creaks[(Random.Range(0, creaks.Count))]);
            source.PlayOneShot(slam);
        }
    }
}
