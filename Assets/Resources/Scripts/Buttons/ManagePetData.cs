using System;
using System.Collections;
using System.Collections.Generic;
using Resources.Scripts.Container;
using Resources.Scripts.Core;
using Resources.Scripts.Events.Data;
using Resources.Scripts.Events.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Resources.Scripts.Buttons
{
    public class ManagePetData : MonoBehaviour, IDisplayPetData
    {
        private int _index;
        private List<AnimalData.Animal> _animals;

        public BeltScript belt;
        public DoorMovement frontDoor;
        public DoorMovement backDoor;

        private GameObject _currentAnimal;

        public TextMeshProUGUI petName;
        public TextMeshProUGUI petDescription;
        public TextMeshProUGUI petTags;
        public TextMeshProUGUI petMisc;
        public TextMeshProUGUI petContactLabel;
        public TextMeshProUGUI petContact;
        [SerializeField] public Image petImage;

        public bool working = false;

        private void Start()
        {
            belt.move = false;
            frontDoor.open = false;
            frontDoor.open = false;
            petImage.enabled = false;
        }

        public void OnDisplayPet(PetDisplayData data)
        {
            _animals = data.data.Animals;
            _index = 0;
            StartCoroutine(DisplayAnimal());
        }

        public void CycleAnimal()
        {
            if (working) return;
            _index++;
            if (_index >= _animals.Count) _index = 0;
            StartCoroutine(DisplayAnimal());
        }
        
        // needed information to broad overview - 
        // name, description, age, gender, size, tags, image url, spayed/neutered, color, breed
        private IEnumerator DisplayAnimal()
        {
            working = true;
            //get data
            var animal = _animals[_index];
            
            // turn on doors and move belt, also hide old data
            frontDoor.open = true;
            backDoor.open = true;
            
            petImage.enabled = false;
            petName.text = "";
            petTags.text = "";
            petDescription.text = "";
            petMisc.text = "";
            petContact.text = "";
            petContactLabel.text = "";
            
            //wait for a bit so it doesn't immediately check movement
            yield return new WaitForSeconds(0.2f);

            // wait until doors open
            while (backDoor.moving || frontDoor.moving)
            {
                yield return null;
            }
            
            // instantiate new animal just out of view
            
            
            // turn belt on and move new and old animals same speed as belt
            belt.move = true;
            
            // this should be a yield while animal is not in middle but animal isn't here yet
            // so it's a sleep 2 sec
            yield return new WaitForSeconds(2);
            
            // stop animal and belt, close doors
            belt.move = false;
            frontDoor.open = false;
            backDoor.open = false;
            
            //wait for a bit so it doesn't immediately check movement
            yield return new WaitForSeconds(0.2f);
            
            //wait until doors close
            while (backDoor.moving || frontDoor.moving)
            {
                yield return null;
            }
            
            // render info text
            petName.text = animal.Name;
            petDescription.text = !string.IsNullOrEmpty(animal.Description) ? animal.Description : "Description not given";
            petTags.text = "";
            if (animal.Tags is { Count: > 0 })
            {
                foreach (var tags in animal.Tags)
                {
                    petTags.text += $"{tags}, ";
                }

                petTags.text = petTags.text[..^2];
            }
            
            petMisc.text =
                $"Gender- {animal.Gender}\n\n" +
                $"Age- {animal.Age}\n\n" +
                $"Breed- {(animal.Breeds.Unknown ? "Unknown" : animal.Breeds.Primary + (animal.Breeds.Secondary == null ? "" : $"/{animal.Breeds.Secondary}"))}\n\n" +
                $"Color- {(string.IsNullOrEmpty(animal.Colors.Primary) ? "Not given" : animal.Colors.Primary)}\n\n" +
                $"Spayed/Neutered- {(animal.Attributes.SpayedNeutered ? "Yes" : "No")}\n\n" +
                $"Special Needs- {(animal.Attributes.SpecialNeeds ? "Yes" : "No")}\n\n" +
                $"Fully vaccinated- {(animal.Attributes.ShotsCurrent ? "Yes" : "No")}";
            
            petContactLabel.text = "Contact Information";
            
            petContact.text = $"Website-\n{(string.IsNullOrEmpty(animal.Url) ? "Not given" : animal.Url)}\n\n" +
                              $"Email-\n{(string.IsNullOrEmpty(animal.Contact.Email) ? "Not given" : animal.Contact.Email)}\n\n" +
                              $"Telephone-\n{(string.IsNullOrEmpty(animal.Contact.Phone) ? "Not given" : animal.Contact.Phone)}\n\n" +
                              $"Shelter Location-\n{animal.Contact.Address.City}, {animal.Contact.Address.State} {animal.Contact.Address.Postcode}\n\n" +
                              $"Distance From You-\n{animal.Distance}";

            // make web request for image and render that too
            var www = UnityWebRequestTexture.GetTexture(animal.PrimaryPhotoCropped.Full);
            yield return www.SendWebRequest();
 
            if (www.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError or UnityWebRequest.Result.DataProcessingError)
            {
                petImage.enabled = false;
            }
            else
            {
                var texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                petImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                petImage.enabled = true;
            }
            
            //delete old animal

            working = false;
        }
    }
}