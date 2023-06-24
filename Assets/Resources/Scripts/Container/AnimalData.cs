using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Resources.Scripts.Container
{
    public class AnimalData
    {
        public class Address
        {
            [JsonProperty("address1")] public object Address1 { get; set; }

            [JsonProperty("address2")] public object Address2 { get; set; }

            [JsonProperty("city")] public string City { get; set; }

            [JsonProperty("state")] public string State { get; set; }

            [JsonProperty("postcode")] public string Postcode { get; set; }

            [JsonProperty("country")] public string Country { get; set; }
        }

        public class Animal
        {
            [JsonProperty("id")] public int Id { get; set; }

            [JsonProperty("organization_id")] public string OrganizationId { get; set; }

            [JsonProperty("url")] public string Url { get; set; }

            [JsonProperty("type")] public string Type { get; set; }

            [JsonProperty("species")] public string Species { get; set; }

            [JsonProperty("breeds")] public Breeds Breeds { get; set; }

            [JsonProperty("colors")] public Colors Colors { get; set; }

            [JsonProperty("age")] public string Age { get; set; }

            [JsonProperty("gender")] public string Gender { get; set; }

            [JsonProperty("size")] public string Size { get; set; }

            [JsonProperty("coat")] public string Coat { get; set; }

            [JsonProperty("attributes")] public Attributes Attributes { get; set; }

            [JsonProperty("environment")] public Environment Environment { get; set; }

            [JsonProperty("tags")] public List<string> Tags { get; set; }

            [JsonProperty("name")] public string Name { get; set; }

            [JsonProperty("description")] public string Description { get; set; }

            [JsonProperty("organization_animal_id")]
            public object OrganizationAnimalId { get; set; }

            [JsonProperty("photos")] public List<Photo> Photos { get; set; }

            [JsonProperty("primary_photo_cropped")]
            public PrimaryPhotoCropped PrimaryPhotoCropped { get; set; }

            [JsonProperty("videos")] public List<object> Videos { get; set; }

            [JsonProperty("status")] public string Status { get; set; }

            [JsonProperty("status_changed_at")] public DateTime StatusChangedAt { get; set; }

            [JsonProperty("published_at")] public DateTime PublishedAt { get; set; }

            [JsonProperty("distance")] public double Distance { get; set; }

            [JsonProperty("contact")] public Contact Contact { get; set; }

            [JsonProperty("_links")] public Links Links { get; set; }
        }

        public class Attributes
        {
            [JsonProperty("spayed_neutered")] public bool SpayedNeutered { get; set; }

            [JsonProperty("house_trained")] public bool HouseTrained { get; set; }

            [JsonProperty("declawed")] public object Declawed { get; set; }

            [JsonProperty("special_needs")] public bool SpecialNeeds { get; set; }

            [JsonProperty("shots_current")] public bool ShotsCurrent { get; set; }
        }

        public class Breeds
        {
            [JsonProperty("primary")] public string Primary { get; set; }

            [JsonProperty("secondary")] public object Secondary { get; set; }

            [JsonProperty("mixed")] public bool Mixed { get; set; }

            [JsonProperty("unknown")] public bool Unknown { get; set; }
        }

        public class Colors
        {
            [JsonProperty("primary")] public string Primary { get; set; }

            [JsonProperty("secondary")] public string Secondary { get; set; }

            [JsonProperty("tertiary")] public string Tertiary { get; set; }
        }

        public class Contact
        {
            [JsonProperty("email")] public string Email { get; set; }

            [JsonProperty("phone")] public string Phone { get; set; }

            [JsonProperty("address")] public Address Address { get; set; }
        }

        public class Environment
        {
            [JsonProperty("children")] public bool Children { get; set; }

            [JsonProperty("dogs")] public bool Dogs { get; set; }

            [JsonProperty("cats")] public bool Cats { get; set; }
        }

        public class Links
        {
            [JsonProperty("self")] public Self Self { get; set; }

            [JsonProperty("type")] public Type Type { get; set; }

            [JsonProperty("organization")] public Organization Organization { get; set; }
        }

        public class Organization
        {
            [JsonProperty("href")] public string Href { get; set; }
        }

        public class Pagination
        {
            [JsonProperty("count_per_page")] public int CountPerPage { get; set; }

            [JsonProperty("total_count")] public int TotalCount { get; set; }

            [JsonProperty("current_page")] public int CurrentPage { get; set; }

            [JsonProperty("total_pages")] public int TotalPages { get; set; }
        }

        public class Photo
        {
            [JsonProperty("small")] public string Small { get; set; }

            [JsonProperty("medium")] public string Medium { get; set; }

            [JsonProperty("large")] public string Large { get; set; }

            [JsonProperty("full")] public string Full { get; set; }
        }

        public class PrimaryPhotoCropped
        {
            [JsonProperty("small")] public string Small { get; set; }

            [JsonProperty("medium")] public string Medium { get; set; }

            [JsonProperty("large")] public string Large { get; set; }

            [JsonProperty("full")] public string Full { get; set; }
        }

        public class Root
        {
            [JsonProperty("animals")] public List<Animal> Animals { get; set; }

            [JsonProperty("pagination")] public Pagination Pagination { get; set; }
        }

        public class Self
        {
            [JsonProperty("href")] public string Href { get; set; }
        }

        public class Type
        {
            [JsonProperty("href")] public string Href { get; set; }
        }
    }
}