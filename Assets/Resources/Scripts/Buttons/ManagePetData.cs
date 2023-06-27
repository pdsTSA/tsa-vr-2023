using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Resources.Scripts.Container;
using Resources.Scripts.Core;
using Resources.Scripts.Events.Data;
using Resources.Scripts.Events.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
        private bool _currentInitialized = false;

        public TextMeshProUGUI petName;
        public TextMeshProUGUI petDescription;
        public TextMeshProUGUI petTags;
        public TextMeshProUGUI petMisc;
        public TextMeshProUGUI petContactLabel;
        public TextMeshProUGUI petContact;
        public TextMeshProUGUI petCount;
        
        [SerializeField] public Image petImage;

        public bool working = false;

        public List<GameObject> dogModels;
        public List<GameObject> catModels;

        public Material baseColor;
        
        public AudioSource beltSfx;
        public AudioSource buttonSfx;
        public AudioSource doorSfx;

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
            if (_animals.Count == 0)
            {
                StartCoroutine(ClearStage());
                return;
            }
            StartCoroutine(DisplayAnimal());
        }

        public void CycleAnimal()
        {
            buttonSfx.Play();
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
            doorSfx.Play();
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
            
            doorSfx.Pause();

            GameObject sprite = null;
            // instantiate new animal just out of view
            if (animal.Species.ToLower() == "dog")
            {
                var spriteNum = animal.Gender.ToLower() == "female" ? 0 : 1;
                sprite = Instantiate(dogModels[spriteNum], Vector3.zero, Quaternion.identity);
                sprite.transform.Rotate(0, -90, 0);
                var y = animal.Gender.ToLower() == "female" ? 0.5f : 0.55f;
                switch (animal.Size.ToLower())
                {
                    case "small":
                        sprite.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                        sprite.transform.position = new Vector3(-0.75f, y * 0.25f, 0.2f);
                        break;
                    case "medium":
                        sprite.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
                        sprite.transform.position = new Vector3(-0.75f, y * 0.35f, 0.2f);
                        break;
                    case "large":
                        sprite.transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
                        sprite.transform.position = new Vector3(-0.75f, y * 0.45f, 0.2f);
                        break;
                    case "xlarge":
                        sprite.transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);
                        sprite.transform.position = new Vector3(-0.75f, y * 0.55f, 0.2f);
                        break;
                }

                foreach(var rend in sprite.GetComponentsInChildren<Renderer>(true))
                {
                    rend.material = baseColor;
                }
            }
            else if (animal.Species.ToLower() == "cat")
            {
                var spriteNum = animal.Gender.ToLower() == "female" ? 0 : 1;
                sprite = Instantiate(catModels[spriteNum], Vector3.zero, Quaternion.identity);
                sprite.transform.Rotate(0, -90, 0);
                var y = animal.Gender.ToLower() == "female" ? 0.6f : 0.55f;
                switch (animal.Size.ToLower())
                {
                    case "small":
                        sprite.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                        sprite.transform.position = new Vector3(-0.75f, y * 0.25f, 0.2f);
                        break;
                    case "medium":
                        sprite.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                        sprite.transform.position = new Vector3(-0.75f, y * 0.3f, 0.2f);
                        break;
                    case "large":
                        sprite.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
                        sprite.transform.position = new Vector3(-0.75f, y * 0.35f, 0.2f);
                        break;
                    case "xlarge":
                        sprite.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                        sprite.transform.position = new Vector3(-0.75f, y * 0.4f, 0.2f);
                        break;
                }
                
                foreach(var rend in sprite.GetComponentsInChildren<Renderer>(true))
                {
                    rend.material = baseColor;
                }
            }   
            
            sprite.transform.Rotate(0, Random.Range(-45, 45), 0);
            
            // turn belt on and move new and old animals same speed as belt
            belt.move = true;
            beltSfx.Play();
            
            while (sprite.transform.position.z > -1.5)
            {
                sprite.transform.position -= new Vector3(0, 0, belt.movementSpeed);
                if (_currentInitialized) _currentAnimal.transform.position -= new Vector3(0, 0, belt.movementSpeed);
                yield return null;
            }

            // stop animal and belt, close doors
            belt.move = false;
            beltSfx.Stop();
            frontDoor.open = false;
            backDoor.open = false;
            doorSfx.UnPause();
            
            //wait for a bit so it doesn't immediately check movement
            yield return new WaitForSeconds(0.2f);
            
            //wait until doors close
            while (backDoor.moving || frontDoor.moving)
            {
                yield return null;
            }
            doorSfx.Stop();
            
            // render info text
            petName.text = WebUtility.HtmlDecode(animal.Name);
            petDescription.text = !string.IsNullOrEmpty(animal.Description) ? WebUtility.HtmlDecode(animal.Description) : "Description not given";
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
                WebUtility.HtmlDecode($"Gender- {animal.Gender}\n\n" +
                                      $"Age- {animal.Age}\n\n" +
                                      $"Breed- {(animal.Breeds.Unknown ? "Unknown" : animal.Breeds.Primary + (animal.Breeds.Secondary == null ? "" : $"/{animal.Breeds.Secondary}"))}\n\n" +
                                      $"Color- {(string.IsNullOrEmpty(animal.Colors.Primary) ? "Not given" : animal.Colors.Primary)}\n\n" +
                                      $"Spayed/Neutered- {(animal.Attributes.SpayedNeutered ? "Yes" : "No")}\n\n" +
                                      $"Special Needs- {(animal.Attributes.SpecialNeeds ? "Yes" : "No")}\n\n" +
                                      $"Fully vaccinated- {(animal.Attributes.ShotsCurrent ? "Yes" : "No")}");
            
            petContactLabel.text = "Contact Information";
            
            petContact.text = WebUtility.HtmlDecode($"Website-\n{(string.IsNullOrEmpty(animal.Url) ? "Not given" : animal.Url)}\n\n" +
                                                    $"Email-\n{(string.IsNullOrEmpty(animal.Contact.Email) ? "Not given" : animal.Contact.Email)}\n\n" +
                                                    $"Telephone-\n{(string.IsNullOrEmpty(animal.Contact.Phone) ? "Not given" : animal.Contact.Phone)}\n\n" +
                                                    $"Shelter Location-\n{animal.Contact.Address.City}, {animal.Contact.Address.State} {animal.Contact.Address.Postcode}\n\n" +
                                                    $"Distance From You-\n{animal.Distance} miles");

            petCount.text = $"#{_index + 1} / {_animals.Count}";

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
            if (_currentInitialized) Destroy(_currentAnimal);
            _currentAnimal = sprite;
            _currentInitialized = true;
            working = false;
        }
        
        // this is if there are no search results
        private IEnumerator ClearStage()
        {
            if (_currentInitialized == false)
            {
                petName.text = "No Results Found";
                yield break;
            }
            working = true;
            
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
            petCount.text = "";
            
            //wait for a bit so it doesn't immediately check movement
            yield return new WaitForSeconds(0.2f);

            // wait until doors open
            while (backDoor.moving || frontDoor.moving)
            {
                yield return null;
            }
            
            belt.move = true;
            
            while (_currentAnimal.transform.position.z > -3.7)
            {
                _currentAnimal.transform.position -= new Vector3(0, 0, belt.movementSpeed);
                yield return null;
            }

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
            
            petName.text = "No Results Found";
            Destroy(_currentAnimal);
            _currentInitialized = false;
            working = false;
        }
    }
}