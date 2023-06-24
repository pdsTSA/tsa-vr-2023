using Resources.Scripts.Container;
using Resources.Scripts.Events.Data;
using UnityEngine.EventSystems;

namespace Resources.Scripts.Events.Interfaces
{
    public interface IDisplayPetData : IEventSystemHandler
    {
        void OnDisplayPet(PetDisplayData data);
    }
}