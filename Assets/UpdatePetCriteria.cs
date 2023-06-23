using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpdatePetCriteria : MonoBehaviour
{
    public string type;
    public List<string> properties;
    private int _index = 0;
    public GameObject handler;
    public TextMeshProUGUI label;

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
        _index++;
        if (_index >= properties.Count) _index = 0;
        label.text = properties[_index];

        ExecuteEvents.Execute<IUpdatePetConfigHandler>(
            handler, 
            new UpdatePetConfigEventData(type, properties[_index]), 
            (a, b) => a.OnUpdatePetConfig((UpdatePetConfigEventData)b));
    }
}
