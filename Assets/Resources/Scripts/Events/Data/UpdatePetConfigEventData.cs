using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Resources.Scripts.Events.Data
{
    public class UpdatePetConfigEventData : BaseEventData
    {
        public readonly string type;
        public readonly string prop;

        public UpdatePetConfigEventData(string type, string prop)
            : base(EventSystem.current)
        {
            this.prop = prop;
            this.type = type;
        }
    }
}