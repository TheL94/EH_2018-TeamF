using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class CharmEffect : IElementalEffectBehaviour
    {
        IEffectable target;
        ElementalEffectData elementalData;

        public void DoInit(IEffectable _target, ElementalEffectData _data)
        {
            target = _target;
            elementalData = _data;
            if (target.GetType().IsAssignableFrom(typeof(Enemy)))
            {
                (target as ICharmable).IsCharmed = true;
                (target as MonoBehaviour).GetComponentInChildren<ParticlesController>().ActivateParticles(ParticlesController.PartucleType.Confusion);
            }
        }

        public void DoStopEffect()
        {
            if ((target as ICharmable) != null)
            {
                (target as ICharmable).IsCharmed = false;
                (target as MonoBehaviour).GetComponentInChildren<ParticlesController>().StopAllParticles();
            }
        }

        public bool DoUpdate()
        {
            elementalData.TimeOfEffect -= Time.deltaTime;
            if (elementalData.TimeOfEffect <= 0)
            {               
                return true;
            }
            return false;
        }
    }
}