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

        string currentID;

        GameObject ParticleObj;
        GameObject DashObj;
        GameObject IncreaseDamageObj;

        ParticleSystem ParticlesEffect;
        ParticleSystem DashParticles;
        MeshRenderer IncreaseDamageMesh;

        public void ActivateParticles(ParticleType _type)
        {
            if (_type != ParticleType.Dash)
                StopAllParticles();
            else
                StopDashParticle();

            switch (_type)
            {
                case ParticleType.Fire:
                    if (FireParticlesGraphicID != null || FireParticlesGraphicID != string.Empty)
                    {
                        currentID = FireParticlesGraphicID;
                        ParticleObj = GetPoolObj(currentID);
                        ParticlesEffect = ParticleObj.GetComponentInChildren<ParticleSystem>();
                        ParticlesEffect.Play();
                    }
                    break;
                case ParticleType.Slowing:
                    if (SlowParticlesGraphicID !=  null || SlowParticlesGraphicID != string.Empty)
                    {
                        currentID = SlowParticlesGraphicID;
                        ParticleObj = GetPoolObj(currentID);
                        ParticlesEffect = ParticleObj.GetComponentInChildren<ParticleSystem>();
                        ParticlesEffect.Play();
                    }
                    break;
                case ParticleType.Confusion:
                    if (ConfusionParticlesGraphicID != null || ConfusionParticlesGraphicID != string.Empty)
                    {
                        currentID = ConfusionParticlesGraphicID;
                        ParticleObj = GetPoolObj(currentID);
                        ParticlesEffect = ParticleObj.GetComponentInChildren<ParticleSystem>();
                        ParticlesEffect.Play();
                    }
                    break;
                case ParticleType.Paralysis:
                    if (ParalysisParticlesGraphicID != null || ParalysisParticlesGraphicID != string.Empty)
                    {
                        currentID = ParalysisParticlesGraphicID;
                        ParticleObj = GetPoolObj(currentID);
                        ParticlesEffect = ParticleObj.GetComponentInChildren<ParticleSystem>();
                        ParticlesEffect.Play();
                    }
                    break;
                case ParticleType.Dash:
                    if (DashParticlesGraphicID != null || DashParticlesGraphicID != string.Empty)
                    {
                        DashObj = GetPoolObj(DashParticlesGraphicID);
                        DashParticles = DashObj.GetComponentInChildren<ParticleSystem>();
                        DashParticles.Play(); 
                    }
                    break;
                case ParticleType.IncreaseDamage:
                    if (IncreaseDamageMeshGraphicID != null || IncreaseDamageMeshGraphicID != string.Empty)
                    {
                        IncreaseDamageObj = GetPoolObj(IncreaseDamageMeshGraphicID);
                        IncreaseDamageMesh = IncreaseDamageObj.GetComponentInChildren<MeshRenderer>();
                        IncreaseDamageMesh.enabled = true; 
                    }
                    break;
            }
        }

        public void StopDashParticle()
        {
            if (DashParticles != null)
            {
                DashParticles.Stop();
                GameManager.I.PoolMng.ReturnObject(DashParticlesGraphicID, DashObj);
            }
        }

        public void StopAllParticles()
        {

            if (ParticlesEffect != null)
            {
                ParticlesEffect.Stop();
                GameManager.I.PoolMng.ReturnObject(currentID, ParticleObj);
            }

            StopDashParticle();

            if (IncreaseDamageMesh != null)
            {
                IncreaseDamageMesh.enabled = false;
                GameManager.I.PoolMng.ReturnObject(IncreaseDamageMeshGraphicID, IncreaseDamageObj);
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
