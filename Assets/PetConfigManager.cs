using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetConfigManager : MonoBehaviour, IUpdatePetConfigHandler
{
    private string _species;
    private string _size;
    private string _gender;
    private string _age;
    private bool _kids;
    private bool _animals;
    private bool _trained;
    private string _zipcode;

    public void OnUpdatePetConfig(UpdatePetConfigEventData data)
    {
        switch (data.type.ToLower())
        {
            case "species":
                _species = data.prop;
                break;
            case "size":
                _size = data.prop;
                break;
            case "gender":
                _gender = data.prop;
                break;
            case "age":
                _age = data.prop;
                break;
            case "kids":
                _kids = data.prop == "true";
                break;
            case "animals":
                _animals = data.prop == "true";
                break;
            case "trained":
                _trained = data.prop == "true";
                break;
            case "zipcode":
                _zipcode = data.prop;
                break;
        }
        
        Debug.Log(_species);

        data.Use();
    }
}
