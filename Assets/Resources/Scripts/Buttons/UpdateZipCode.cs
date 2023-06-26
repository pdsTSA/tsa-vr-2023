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
        public TextMeshProUGUI debug;
        private bool _dirty = false;

        private void Update()
        {
            if (_overlayKeyboard != null && _overlayKeyboard.text.Length == 5)
            {
                text.text = _overlayKeyboard.text;
                _overlayKeyboard.active = false;
                _overlayKeyboard = null;
                _dirty = true;
            }
            else
            {
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
            if (_overlayKeyboard == null) _overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.NumberPad);
        }
    }
}
