using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager {
    // Start is called before the first frame update

    public enum Sound {

       eructo1,
       eructo2,
       eructo3,
       eructo4,
       alarma,
       caidaEscalera,
       caidaLibre,
       mear,
       qte,
       rataDmg,
       rataDeath,
       takeDmg,
       jump,
       charDeath,
       acabDmg,
       throwing,
       bullet,
       punch,
       golpetaso,
        beep,


    }

    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;
    
    public static void PlaySound(Sound sound, float vol) {

        if (oneShotGameObject == null) {
            oneShotGameObject = new GameObject("OneShotSound");
            oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
        }

        oneShotAudioSource.PlayOneShot(GetAudioClip(sound), vol);

    }

    private static AudioClip GetAudioClip(Sound sound) {
        foreach(GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArryay) {

            if (soundAudioClip.sound == sound) {
                return soundAudioClip.audioClip;
            }

        }

        Debug.LogError("Sound" + sound + " not found!");
        return null;
        

    }

}
