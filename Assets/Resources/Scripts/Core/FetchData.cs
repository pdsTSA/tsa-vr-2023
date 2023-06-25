using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Resources.Scripts.Buttons;
using Resources.Scripts.Container;
using Resources.Scripts.Events.Data;
using Resources.Scripts.Events.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.Serialization;

namespace Resources.Scripts.Core
{
    public class FetchData : MonoBehaviour
    {
        private const string BaseUrl = "https://phqsh.tech/vr/api";
        public PetConfigManager manager;
        public GameObject recipient;
        public ManagePetData dataManager;
        
        public void Fetch()
        {
            StartCoroutine(FetchAnimalData());
        }

        private IEnumerator FetchAnimalData()
        {
            if (dataManager.working) yield break;
            while (!manager.Ready())
            {
                yield return null;
            }

            var config = manager.ToConfig();
            var url =
                $"{BaseUrl}?species={config.species}&gender={config.gender}&age={config.age}&size={config.size}&good_with_kids={config.kids}&good_with_animals={config.animals}&house_trained={config.trained}&location={config.zipcode}";
            
            var uwr = UnityWebRequest.Get(url);
            yield return uwr.SendWebRequest();

            if (uwr.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.DataProcessingError)
            {
                //Do offline request logic here
            }
            else if (uwr.result is UnityWebRequest.Result.ProtocolError)
            {
                // cursed, if error is 4xx-5xx
                var data = new AnimalData.Root();
                data.Animals = new List<AnimalData.Animal>();
                data.Pagination = new AnimalData.Pagination();
                data.Pagination.TotalCount = 0;
                ExecuteEvents.Execute<IDisplayPetData>(
                    recipient,
                    new PetDisplayData(data),
                    (a, b) => a.OnDisplayPet((PetDisplayData)b)
                );
            }
            else
            {
                // Do online request logic here
                var data = JsonConvert.DeserializeObject<AnimalData.Root>(uwr.downloadHandler.text);
                ExecuteEvents.Execute<IDisplayPetData>(
                    recipient,
                    new PetDisplayData(data),
                    (a, b) => a.OnDisplayPet((PetDisplayData)b)
                );
            }
        }
    }
}
