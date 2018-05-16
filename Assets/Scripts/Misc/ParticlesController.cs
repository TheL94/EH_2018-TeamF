using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class ParticlesController : MonoBehaviour
    {
        public ParticleSystem FireParticles;
        public ParticleSystem ConfusionParticles;
        public ParticleSystem ParalysisParticles;
        public ParticleSystem SlowParticles;
        public ParticleSystem IncreaseDamageParticles;
        public ParticleSystem Dash;

        public void ActivateParticles(ParticleType _type)
        {
            StopAllParticles();
            switch (_type)
            {
                case ParticleType.Fire:
                    if (FireParticles != null)
                        FireParticles.Play(); 
                    break;
                case ParticleType.Slowing:
                    if (SlowParticles != null)
                        SlowParticles.Play();
                    break;
                case ParticleType.Confusion:
                    if (ConfusionParticles != null)
                        ConfusionParticles.Play();
                    break;
                case ParticleType.Paralysis:
                    if (ParalysisParticles != null)
                        ParalysisParticles.Play();
                    break;
                case ParticleType.IncreaseDamage:
                    if (IncreaseDamageParticles != null)
                        IncreaseDamageParticles.Play();
                    break;
                case ParticleType.Dash:
                    if (Dash != null)
                        Dash.Play();
                    break;
                default:
                    break;
            }
        }

        public void StopAllParticles()
        {
            if (FireParticles != null)
                FireParticles.Stop();
            if (SlowParticles != null)
                SlowParticles.Stop();
            if (ConfusionParticles != null)
                ConfusionParticles.Stop();
            if (ParalysisParticles != null)
                ParalysisParticles.Stop();
            if (IncreaseDamageParticles != null)
                IncreaseDamageParticles.Stop();
            if(Dash != null)
                Dash.Stop();
        }


        public enum ParticleType
        {
            Fire,
            Slowing,
            Confusion,
            Paralysis,
            IncreaseDamage,
            Dash
        }
    }
}
