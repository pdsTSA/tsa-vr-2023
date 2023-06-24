using System.Collections;
using System.Collections.Generic;
using Resources.Scripts.Events.Data;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Resources.Scripts.Events.Interfaces
{
    public interface IUpdatePetConfigHandler : IEventSystemHandler
    {
        void OnUpdatePetConfig(UpdatePetConfigEventData eventData);
    }
}
