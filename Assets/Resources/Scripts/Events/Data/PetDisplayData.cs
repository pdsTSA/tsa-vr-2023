using Resources.Scripts.Container;
using UnityEngine.EventSystems;

namespace Resources.Scripts.Events.Data
{
    public class PetDisplayData : BaseEventData
    {
        public AnimalData.Root data;

        public PetDisplayData(AnimalData.Root data)
            : base(EventSystem.current)
        {
            this.data = data;
        }
    }
}
