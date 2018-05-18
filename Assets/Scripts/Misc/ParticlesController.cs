using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class ParticlesController : MonoBehaviour
    {
        public string FireParticlesGraphicID;
        public string ConfusionParticlesGraphicID;
        public string ParalysisParticlesGraphicID;
        public string SlowParticlesGraphicID;
        public string DashParticlesGraphicID;
        public string IncreaseDamageMeshGraphicID;

        ParticleSystem ParticlesEffect;
        ParticleSystem DashParticles;
        MeshRenderer IncreaseDamageMesh;

        public void ActivateParticles(ParticleType _type)
        {
            if (_type != ParticleType.Dash)
                StopAllParticles();

            switch (_type)
            {
                case ParticleType.Fire:
                    if (FireParticlesGraphicID != null || FireParticlesGraphicID != string.Empty)
                    {
                        ParticlesEffect = GetPoolObj(FireParticlesGraphicID).GetComponentInChildren<ParticleSystem>();
                        ParticlesEffect.Play(); 
                    }
                    break;
                case ParticleType.Slowing:
                    if (SlowParticlesGraphicID !=  null || SlowParticlesGraphicID != string.Empty)
                    {
                        ParticlesEffect = GetPoolObj(SlowParticlesGraphicID).GetComponentInChildren<ParticleSystem>();
                        ParticlesEffect.Play(); 
                    }
                    break;
                case ParticleType.Confusion:
                    if (ConfusionParticlesGraphicID != null || ConfusionParticlesGraphicID != string.Empty)
                    {
                        ParticlesEffect = GetPoolObj(ConfusionParticlesGraphicID).GetComponentInChildren<ParticleSystem>();
                        ParticlesEffect.Play(); 
                    }
                    break;
                case ParticleType.Paralysis:
                    if (ParalysisParticlesGraphicID != null || ParalysisParticlesGraphicID != string.Empty)
                    {
                        ParticlesEffect = GetPoolObj(ParalysisParticlesGraphicID).GetComponentInChildren<ParticleSystem>();
                        ParticlesEffect.Play(); 
                    }
                    break;
                case ParticleType.Dash:
                    if (DashParticlesGraphicID != null || DashParticlesGraphicID != string.Empty)
                    {
                        DashParticles = GetPoolObj(DashParticlesGraphicID).GetComponentInChildren<ParticleSystem>();
                        DashParticles.Play(); 
                    }
                    break;
                case ParticleType.IncreaseDamage:
                    if (IncreaseDamageMeshGraphicID != null || IncreaseDamageMeshGraphicID != string.Empty)
                    {
                        IncreaseDamageMesh = GetPoolObj(IncreaseDamageMeshGraphicID).GetComponentInChildren<MeshRenderer>();
                        IncreaseDamageMesh.enabled = true; 
                    }
                    break;
            }
        }

        public void StopAllParticles()
        {

            if (ParticlesEffect != null)
            {
                ParticlesEffect.Stop();
                ParticlesEffect.GetComponentInParent<Transform>().gameObject.SetActive(false);
                GameManager.I.PoolMng.UpdatePool(FireParticlesGraphicID);
            }

            if (DashParticles != null)
            {
                DashParticles.Stop();
                DashParticles.GetComponentInParent<Transform>().gameObject.SetActive(false);
                GameManager.I.PoolMng.UpdatePool(DashParticlesGraphicID);
            }

            if (IncreaseDamageMesh != null)
            {
                IncreaseDamageMesh.enabled = false;
                IncreaseDamageMesh.GetComponentInParent<Transform>().gameObject.SetActive(false);
                GameManager.I.PoolMng.UpdatePool(IncreaseDamageMeshGraphicID);
            }
        }

        GameObject GetPoolObj(string _graphicID)
        {
            GameObject poolObj = GameManager.I.PoolMng.GetObject(_graphicID);
            poolObj.transform.position = transform.position;
            poolObj.transform.parent = transform;
            return poolObj;
        }

        public enum ParticleType
        {
            Fire,
            Slowing,
            Confusion,
            Paralysis,
            Dash,
            IncreaseDamage
        }
    }
}
