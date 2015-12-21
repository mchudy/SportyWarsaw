using Newtonsoft.Json;
using SportyWarsaw.Domain.Entities;
using System.IO;
using Xunit;

namespace SportyWarsaw.Tests
{
    public class JsonParsingTests
    {
        private readonly string singleFacility;

        public JsonParsingTests()
        {
            singleFacility = File.ReadAllText(@"TestData/singleFacility.json");
        }

        [Fact]
        public void SportsFacilityConverter_ShouldAssignAllSimpleProperties()
        {
            SportsFacility facility = JsonConvert.DeserializeObject<SportsFacility>(singleFacility);
            Assert.Equal("Jagiellońska", facility.Street);
            Assert.Equal("DOSiR", facility.Description);
            Assert.Equal("Praga Północ", facility.District);
            Assert.Equal("Warszawa", facility.AdministrativeUnit);
            Assert.Equal("http://www.dosir.waw.pl/", facility.Website);
            Assert.Equal("7", facility.Number);
        }

        [Fact]
        public void SportsFacilityConverter_ShouldAssignPosition()
        {
            SportsFacility facility = JsonConvert.DeserializeObject<SportsFacility>(singleFacility);
            Assert.Equal(52.249991, facility.Position.Latitude);
            Assert.Equal(21.035679, facility.Position.Longitude);
        }

        [Fact]
        public void SportsFacilityConverter_WhenSinglePhoneNumber_ShouldAssignPhoneNumber()
        {
            SportsFacility facility = JsonConvert.DeserializeObject<SportsFacility>(singleFacility);
            Assert.Equal("22 619 81 38", facility.PhoneNumber);
        }

        [Fact]
        public void SportsFacilityConverter_WhenPhoneNumberAndFax_ShouldAssignPhoneNumber()
        {
            string json = @"{
                ""properties"":[
                    {
                        ""key"": ""TEL_FAX"",
                        ""value"": ""22 593 12 83 / 22 847 17 82""
                    }
                ]
            }";
            SportsFacility facility = JsonConvert.DeserializeObject<SportsFacility>(json);
            Assert.Equal("22 593 12 83", facility.PhoneNumber);
        }

        [Fact]
        public void SportsFacilityConverter_WhenPhoneIsEmpty_ShouldNotAssignPhoneNumber()
        {
            string json = @"{
                ""properties"":[
                    {
                        ""key"": ""TEL_FAX"",
                        ""value"": ""brak / 22 847 17 82""
                    }
                ]
            }";
            SportsFacility facility = JsonConvert.DeserializeObject<SportsFacility>(json);
            Assert.Equal(null, facility.PhoneNumber);
        }

        [Fact]
        public void SportsFacilityConverter_WhenSingleEmail_ShouldAddEmail()
        {
            string json = @"{
                ""properties"":[
                    {
                        ""key"": ""MAIL1"",
                        ""value"": ""mailto:sekretariat.jagiellonska@dosir.waw.pl"",
                    },
                    {
                        ""key"": ""MAIL2"",
                        ""value"": ""  "",
                    }
                ]
            }";
            SportsFacility facility = JsonConvert.DeserializeObject<SportsFacility>(json);

            Assert.Equal(1, facility.Emails.Count);
            Assert.Equal("sekretariat.jagiellonska@dosir.waw.pl", facility.Emails[0]);
        }

        [Fact]
        public void SportsFacilityConverter_WhenBothEmailsAreNonEmpty_ShouldAddBothEmails()
        {
            string json = @"{
                ""properties"":[
                    {
                        ""key"": ""MAIL1"",
                        ""value"": ""mailto:sekretariat.jagiellonska@dosir.waw.pl""
                    },
                    {
                        ""key"": ""MAIL2"",
                        ""value"": ""mailto:sekretariat2.jagiellonska@dosir.waw.pl""
                    }
                ]
            }";
            SportsFacility facility = JsonConvert.DeserializeObject<SportsFacility>(json);

            Assert.Equal(2, facility.Emails.Count);
            Assert.Equal("sekretariat.jagiellonska@dosir.waw.pl", facility.Emails[0]);
            Assert.Equal("sekretariat2.jagiellonska@dosir.waw.pl", facility.Emails[1]);
        }
    }
}
