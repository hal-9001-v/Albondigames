using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager {
    // Start is called before the first frame update

    public enum Sound {

        weakDies,
        medDies,
        heavDies,
        mariOomph,
        mariShoot,
        bulletSound,
        healthPickUp,
        speedBoost,
        pill,
        ice,    
        sharpen,
        meleeDmg,
        gyalTalks,
        lokiTalks,
        mariTalks,
        narturoTalks,
        pastaTalks,
        raTalks,
        lokiTP,
        lokiShoot,
        lokiDeath,
        raBallDies,
        raBallShoots,
        raDanceOn,
        raDanceOff,
        raBallRevive,
        raDanceDmg,
        nordTalks,
        isisTalks,
        fenrirOdinTalks,
        zeusTalks,
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
