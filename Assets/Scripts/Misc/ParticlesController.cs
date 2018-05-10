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


        public void ActivateParticles(PartucleType _type)
        {
            StopAllParticles();
            switch (_type)
            {
                case PartucleType.Fire:
                    if (FireParticles != null)
                        FireParticles.Play(); 
                    break;
                case PartucleType.Slowing:
                    if (SlowParticles != null)
                        SlowParticles.Play();
                    break;
                case PartucleType.Confusion:
                    if (ConfusionParticles != null)
                        ConfusionParticles.Play();
                    break;
                case PartucleType.Paralysis:
                    if (ParalysisParticles != null)
                        ParalysisParticles.Play();
                    break;
                case PartucleType.IncreaseDamage:
                    if (IncreaseDamageParticles != null)
                        IncreaseDamageParticles.Play();
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
        }


        public enum PartucleType
        {
            Fire,
            Slowing,
            Confusion,
            Paralysis,
            IncreaseDamage
        }
    }
}
