using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class AmmoCratesController : MonoBehaviour
    {

        List<AmmoCrate> Crates = new List<AmmoCrate>();
        public GameObject AmmoCratePrefab;
        public float TimeToSpawnCrate;
        public List<Transform> AmmoSpawnPoints = new List<Transform>();

        public void Init()
        {
            CreateAmmoCrate();
        }

        /// <summary>
        /// Distrugge e pulisce la lista di Ammo Crates e chiama l'init.
        /// </summary>
        public void ReInit()
        {
            for (int i = 0; i < Crates.Count; i++)
            {
                Destroy(Crates[i].gameObject);
                Crates.Remove(Crates[i]);
            }
            Init();
        }

        /// <summary>
        /// Chiamata dalla ammocrate per informare il controller che è stata raccolta. Viene rimossa dalla lista di Crates in scena, Viene distrutta, 
        /// Determina a quale spawnPoint apparteneva, avvia la coroutine per crearne una nuova passando la positione dello spawnPoint
        /// </summary>
        /// <param name="_crate">La Crate che deve essere rimossa</param>
        public void DeleteAmmoCrateFromList(AmmoCrate _crate)
        {
            Crates.Remove(_crate);
            Destroy(_crate.gameObject);

            Transform spanwerPosition = null;
            foreach (Transform transf in AmmoSpawnPoints)
            {
                if (_crate.transform.position == transf.position)
                    spanwerPosition = transf;
            }
            if (spanwerPosition != null)
                StartCoroutine(CreateAmmoCrateAfterTime(spanwerPosition)); 
        }

        /// <summary>
        /// Crea le AmmoCrate nei punti di spawn
        /// </summary>
        void CreateAmmoCrate()
        {
            foreach (Transform pos in AmmoSpawnPoints)
            {
                AmmoCrate temp = Instantiate(AmmoCratePrefab, pos.position, Quaternion.identity, pos).GetComponent<AmmoCrate>();
                temp.Init(this);
                Crates.Add(temp);
            }
        }

        /// <summary>
        /// Crea una AmmoCrate nella posizione e figlia della transform data.
        /// </summary>
        /// <param name="_pos">La trasnform dello spawn dove instanziare la crate</param>
        void CreateAmmoCrate(Transform _pos)
        {
            AmmoCrate temp = Instantiate(AmmoCratePrefab, _pos.position, Quaternion.identity, _pos).GetComponent<AmmoCrate>();
            temp.Init(this);
            Crates.Add(temp);
        }

        /// <summary>
        /// Aspetta X secondi per chiamare la funzione per creare la nuova Crate nella posizione data
        /// </summary>
        /// <param name="_position">La transform dello spawn dove instanziare la crate</param>
        /// <returns></returns>
        IEnumerator CreateAmmoCrateAfterTime(Transform _position)
        {
            yield return new WaitForSeconds(TimeToSpawnCrate);
            CreateAmmoCrate(_position);
        }
    }
}