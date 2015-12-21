using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SportyWarsaw.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SportyWarsaw.Domain.Data
{
    public class SportsFacilityConverter : JsonConverter
    {
        private const string phoneNumberRegex = @"^(0(\s)*)?((\d){2})?(\s)*((\d){2,3}(\s)*){1,3}$";

        private static readonly IDictionary<string, string> mappings = new Dictionary<string, string>
        {
            { "ULICA", nameof(SportsFacility.Street)},
            { "NUMER", nameof(SportsFacility.Number)},
            { "OPIS", nameof(SportsFacility.Description)},
            { "DZIELNICA", nameof(SportsFacility.District)},
            { "JEDN_ADM", nameof(SportsFacility.AdministrativeUnit)},
            { "WWW", nameof(SportsFacility.Website)},
        };

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            var facility = new SportsFacility();
            ParseProperties(jsonObject, facility);
            ParseGeometry(jsonObject, facility);
            return facility;
        }

        private static void ParseProperties(JObject jsonObject, SportsFacility facility)
        {
            var properties = jsonObject["properties"];
            if (properties == null) return;
            foreach (var entry in properties)
            {
                var key = entry["key"].ToString();
                var value = entry["value"].ToString().Trim();
                string property;
                var result = mappings.TryGetValue(key, out property);
                if (result)
                {
                    facility.GetType().GetProperty(mappings[key])
                                      .SetValue(facility, value);
                }
                else if (key == "TEL_FAX")
                {
                    AddPhoneNumber(facility, value);
                }
                else if (key == "MAIL1" || key == "MAIL2")
                {
                    AddEmailAddress(facility, value);
                }
            }
        }

        private static void AddEmailAddress(SportsFacility facility, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                string email = Regex.Replace(value, @"^mailto:(\s)*", string.Empty);
                facility.Emails.Add(email);
            }
        }

        private static void AddPhoneNumber(SportsFacility facility, string value)
        {
            string[] numbers = value.Split('/');
            // the first item is a phone number, the second item is a fax number
            string number = numbers[0].Trim();
            if (Regex.IsMatch(number, phoneNumberRegex))
            {
                facility.PhoneNumber = number;
            }
        }

        private static void ParseGeometry(JObject jsonObject, SportsFacility facility)
        {
            var geometry = jsonObject["geometry"];
            if (geometry == null) return;
            var type = geometry["type"].ToString();
            if (type == "ShapePoint" || type == "Point")
            {
                var coords = geometry["coordinates"];
                facility.Position.Latitude = coords["lat"].ToObject<double>();
                facility.Position.Longitude = coords["lon"].ToObject<double>();
            }
        }

        public override bool CanConvert(Type objectType)
        {
            throw new System.NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new System.NotImplementedException();
        }
    }
}
