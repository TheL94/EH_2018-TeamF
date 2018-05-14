using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class ParalyzeEffect : IElementalEffectBehaviour
    {
        ElementalEffectData elementalData;
        IEffectable target;

        public void DoInit(IEffectable _target, ElementalEffectData _data)
        {
            target = _target;
            elementalData = _data;
            target.IsParalyzed = true;
            (target as MonoBehaviour).GetComponentInChildren<ParticlesController>().ActivateParticles(ParticlesController.ParticleType.Paralysis);
        }

        public void DoStopEffect()
        {
            if (target != null)
            {
                target.IsParalyzed = false;
                (target as MonoBehaviour).GetComponentInChildren<ParticlesController>().StopAllParticles();
            }
        }

        public bool DoUpdate()
        {
            elementalData.TimeOfEffect -= Time.deltaTime;
            if (elementalData.TimeOfEffect <= 0)
                return true;

            return false;
        }
    }
}