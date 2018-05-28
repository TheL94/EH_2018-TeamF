using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

namespace TeamF
{
    public class PostProcessController : MonoBehaviour
    {
        public PostProcessingProfile ForestPostProcess;
        public PostProcessingProfile MinePostProcess;
        public PostProcessingProfile CityPostProcess;

        PostProcessingBehaviour ppBehaviour;

        private void Start()
        {
            ppBehaviour = GetComponent<PostProcessingBehaviour>();
        }

        public void SetPostProcess(MapType _type)
        {
            switch (_type)
            {
                case MapType.Forest:
                    if (ForestPostProcess != null)
                        ppBehaviour.profile = ForestPostProcess;
                    break;
                case MapType.Mine:
                    if (MinePostProcess != null)
                        ppBehaviour.profile = MinePostProcess;
                    break;
                case MapType.City:
                    if (CityPostProcess != null)
                        ppBehaviour.profile = CityPostProcess;
                    break;
            }           
        }

        public enum MapType { Forest, Mine, City }
    }
}

