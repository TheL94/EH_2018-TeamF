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

        ParticleSystem FireParticles;
        ParticleSystem ConfusionParticles;
        ParticleSystem ParalysisParticles;
        ParticleSystem SlowParticles;
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
                        FireParticles = GetPoolObj(FireParticlesGraphicID).GetComponentInChildren<ParticleSystem>();
                        FireParticles.Play(); 
                    }
                    break;
                case ParticleType.Slowing:
                    if (SlowParticlesGraphicID !=  null || SlowParticlesGraphicID != string.Empty)
                    {
                        SlowParticles = GetPoolObj(SlowParticlesGraphicID).GetComponentInChildren<ParticleSystem>();
                        SlowParticles.Play(); 
                    }
                    break;
                case ParticleType.Confusion:
                    if (ConfusionParticlesGraphicID != null || ConfusionParticlesGraphicID != string.Empty)
                    {
                        ConfusionParticles = GetPoolObj(ConfusionParticlesGraphicID).GetComponentInChildren<ParticleSystem>();
                        ConfusionParticles.Play(); 
                    }
                    break;
                case ParticleType.Paralysis:
                    if (ParalysisParticlesGraphicID != null || ParalysisParticlesGraphicID != string.Empty)
                    {
                        ParalysisParticles = GetPoolObj(ParalysisParticlesGraphicID).GetComponentInChildren<ParticleSystem>();
                        ParalysisParticles.Play(); 
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
            if (FireParticles != null)
            {
                FireParticles.Stop();
                FireParticles.gameObject.SetActive(false);
                GameManager.I.PoolMng.UpdatePool(FireParticlesGraphicID);
            }

            if (SlowParticles != null)
            {
                SlowParticles.Stop();
                SlowParticles.gameObject.SetActive(false);
                GameManager.I.PoolMng.UpdatePool(SlowParticlesGraphicID);
            }

            if (ConfusionParticles != null)
            {
                ConfusionParticles.Stop();
                ConfusionParticles.gameObject.SetActive(false);
                GameManager.I.PoolMng.UpdatePool(ConfusionParticlesGraphicID);
            }

            if (ParalysisParticles != null)
            {
                ParalysisParticles.Stop();
                ParalysisParticles.gameObject.SetActive(false);
                GameManager.I.PoolMng.UpdatePool(ParalysisParticlesGraphicID);
            }

            if (DashParticles != null)
            {
                DashParticles.Stop();
                DashParticles.gameObject.SetActive(false);
                GameManager.I.PoolMng.UpdatePool(DashParticlesGraphicID);
            }

            if (IncreaseDamageMesh != null)
            {
                IncreaseDamageMesh.enabled = false;
                IncreaseDamageMesh.gameObject.SetActive(false);
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
