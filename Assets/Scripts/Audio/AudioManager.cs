using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TeamF
{
    public class AudioManager : MonoBehaviour
    {
        public List<ClipData> Clips = new List<ClipData>();

        List<AudioSource> allSources;

        AudioSource MusicSource;
        AudioSource AmbienceSources;

        List<AudioSource> GenricSources;
        List<AudioSource> CharacterSources;
        List<AudioSource> EnemySources;
        List<AudioSource> ComboSources;

        public void Init()
        {
            allSources = GetComponentsInChildren<AudioSource>().ToList();

            MusicSource = allSources.FirstOrDefault(s => s.tag == "MusicAudioSource");
            AmbienceSources = allSources.FirstOrDefault(s => s.tag == "AmbienceAudioSource");

            GenricSources = allSources.Where(s => s.tag == "GenericAudioSource").ToList();
            CharacterSources = allSources.Where(s => s.tag == "CharacterAudioSource").ToList();
            EnemySources = allSources.Where(s => s.tag == "EnemyAudioSource").ToList();
            ComboSources = allSources.Where(s => s.tag == "ComboAudioSource").ToList();
        }

        public void StopAllSound()
        {
            foreach (AudioSource source in allSources)
            {
                source.Stop();
                source.clip = null;
            }
        }

        public void TogglePauseAll(bool _value)
        {
            foreach (AudioSource source in allSources)
            {
                if (_value)
                    source.Pause();
                else
                    source.UnPause();
            }         
        }

        public void PlaySound(Clips _clip)
        {
            ClipData clipToPlay = GetClip(_clip);
            AudioSource availableSource = GetAvailableSurce((int)_clip);

            if(clipToPlay != null && availableSource != null)
            {
                availableSource.clip = clipToPlay.Clip;
                availableSource.volume = clipToPlay.Volume;
                availableSource.Play();
            }
        }

        AudioSource GetAvailableSurce(int _clipNumber)
        {
            List<AudioSource> sources = null;

            if(_clipNumber == 0 || _clipNumber == 4) // Music
                sources = new List<AudioSource>() { MusicSource };
            else if (_clipNumber >= 1 && _clipNumber < 4) // Menù
                sources = GenricSources;
            else if (_clipNumber >= 5 && _clipNumber < 8) // Ambience
                sources = new List<AudioSource>() { AmbienceSources };
            else if (_clipNumber >= 8 && _clipNumber < 13) // Character
                sources = CharacterSources;
            else if (_clipNumber >= 13 && _clipNumber < 16) // Enemys
                sources = EnemySources;
            else if (_clipNumber >= 16 && _clipNumber < 23) // Combos
                sources = ComboSources;

            if (sources != null || sources.Count > 0)
            {
                foreach (AudioSource source in sources)
                    if (!source.isPlaying)
                        return source;
            }

            return null;
        }

        ClipData GetClip(Clips _clip)
        {
            foreach (ClipData clipData in Clips)
            {
                if (clipData.ID == _clip)
                    return clipData;
            }

            return null;
        }
    }

    [System.Serializable]
    public class ClipData
    {
        public Clips ID;
        public AudioClip Clip;
        [Range(0f,1f)]
        public float Volume;
    }

    public enum Clips
    {
        MenuMusic = 0,
        MenuConfirm,
        MenuMovement,
        MenuBack,

        GameplayMusic,
        ForestAmbience, // potrebbe non esserci
        MineAmbience, // potrebbe non esserci
        CityAmbience, // potrebbe non esserci

        CharacterShoot,
        CharacterDash,
        CharacterDamage, // potrebbe non esserci
        CharacterDeath,
        CharacterPickUp,

        EnemyDamage, // potrebbe non esserci
        EnemyAttack,
        EnemyDeath,

        ComboBlackHole,
        ComboConfusionCloud,
        ComboFireExplosion,
        ComboIncreaseDamage,
        ComboParalyzingCloud,
        ComboSlowingCloud,
        PoisonCloud
    }
}

