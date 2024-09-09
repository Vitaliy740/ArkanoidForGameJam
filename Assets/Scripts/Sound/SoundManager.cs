using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField]
    private AudioSource _sFXSource;

    [SerializeField]
    private AudioSource _musicSource;


    public static SoundManager Instance;

    public AudioClip FailSound;
    public AudioClip ShootSound;
    public AudioClip PlatformHitSound;
    
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(this);
            }
        }
    }
    void Start()
    {
        
    }

    public void PlaySound(AudioClip clip) 
    {
        _sFXSource.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
