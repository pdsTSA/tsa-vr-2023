using Resources.Scripts.Events.Data;
using Resources.Scripts.Events.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Resources.Scripts.Buttons
{
    public class UpdateZipCode : MonoBehaviour
    {
        private TouchScreenKeyboard _overlayKeyboard;
        public TextMeshProUGUI text;
        public GameObject handler;
        private bool _dirty = false;

        private void Update()
        {
            if (_overlayKeyboard is { active: true })
            {
                text.text = _overlayKeyboard.text;
                _dirty = true;
            }
            else
            {
                if (_overlayKeyboard != null)
                {
                    _overlayKeyboard.active = false;
                }
                if (!_dirty) return;
                ExecuteEvents.Execute<IUpdatePetConfigHandler>(
                    handler, 
                    new UpdatePetConfigEventData("zipcode", text.text), 
                    (a, b) => a.OnUpdatePetConfig((UpdatePetConfigEventData)b));
                _dirty = false;
            }
        }

        public void OpenKeyboard()
        {
            _overlayKeyboard = TouchScreenKeyboard.Open(text.text, TouchScreenKeyboardType.NumberPad);
        }
    }
}
