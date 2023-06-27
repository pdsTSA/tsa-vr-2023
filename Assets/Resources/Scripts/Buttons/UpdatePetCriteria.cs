using System.Collections.Generic;
using Resources.Scripts.Events.Data;
using Resources.Scripts.Events.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Resources.Scripts.Buttons
{
    public class UpdatePetCriteria : MonoBehaviour
    {
        public string type;
        public List<string> properties;
        private int _index = 0;
        public GameObject handler;
        public TextMeshProUGUI label;
        public AudioSource buttonSfx;

        private void Start()
        {
            label.text = properties[_index];
            ExecuteEvents.Execute<IUpdatePetConfigHandler>(
                handler, 
                new UpdatePetConfigEventData(type, properties[_index]), 
                (a, b) => a.OnUpdatePetConfig((UpdatePetConfigEventData)b));
        }

        public void CycleProperty()
        {
            buttonSfx.Play();
            _index++;
            if (_index >= properties.Count) _index = 0;
            label.text = properties[_index];

            ExecuteEvents.Execute<IUpdatePetConfigHandler>(
                handler, 
                new UpdatePetConfigEventData(type, properties[_index]), 
                (a, b) => a.OnUpdatePetConfig((UpdatePetConfigEventData)b));
        }
    }
}
