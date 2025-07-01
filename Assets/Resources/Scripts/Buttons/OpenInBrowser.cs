using Oculus.Platform;
using UnityEditor;
using UnityEngine;
using Application = UnityEngine.Application;

namespace Resources.Scripts.Buttons
{
    public class OpenInBrowser : MonoBehaviour
    {
        public string Url { get; set; }

        public void Open()
        {
            if (!string.IsNullOrEmpty(Url))
            {
                Application.OpenURL("https://" + Url);
            }
        }
    }
}
