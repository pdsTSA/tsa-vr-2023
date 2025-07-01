using Resources.Scripts.Events.Data;
using Resources.Scripts.Events.Interfaces;
using UnityEngine;

namespace Resources.Scripts.Core
{
    public class PetConfigManager : MonoBehaviour, IUpdatePetConfigHandler
    {
        private string _species;
        private string _size;
        private string _gender;
        private string _age;
        private string _kids;
        private string _animals;
        private string _trained;
        private string _zipcode = "";

        public void OnUpdatePetConfig(UpdatePetConfigEventData data)
        {
            switch (data.type.ToLower())
            {
                case "species":
                    _species = data.prop;
                    if (data.prop.ToLower() == "any") _species = "";
                    break;
                case "size":
                    _size = data.prop;
                    if (data.prop.ToLower() == "any") _size = "";
                    break;
                case "gender":
                    _gender = data.prop;
                    if (data.prop.ToLower() == "any") _gender = "";
                    break;
                case "age":
                    _age = data.prop;
                    if (data.prop.ToLower() == "any") _age = "";
                    break;
                case "kids":
                    _kids = data.prop.ToLower() == "yes" ? "true" : "";
                    break;
                case "animals":
                    _animals = data.prop.ToLower() == "yes" ? "true" : "";
                    break;
                case "trained":
                    _trained = data.prop.ToLower() == "yes" ? "true" : "";
                    break;
                case "zipcode":
                    _zipcode = data.prop;
                    break;
            }
            
            data.Use();
        }

        public PetConfig ToConfig()
        {
            return new PetConfig(_species, _size, _gender, _age, _kids, _animals, _trained, _zipcode);
        }

        public bool Ready()
        {
            return _species != null &&
                   _size != null &&
                   _gender != null &&
                   _age != null &&
                   _kids != null &&
                   _animals != null &&
                   _trained != null &&
                   _zipcode != null;
        }

        public class PetConfig
        {
            public string species;
            public string size;
            public string gender;
            public string age;
            public string kids;
            public string animals;
            public string trained;
            public string zipcode;

            public PetConfig(string species, string size, string gender, string age, string kids, string animals, string trained, string zipcode)
            {
                this.species = species;
                this.size = size;
                this.gender = gender;
                this.age = age;
                this.kids = kids;
                this.animals = animals;
                this.trained = trained;
                this.zipcode = zipcode;
            }
        }
    }
}
