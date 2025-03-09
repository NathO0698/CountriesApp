namespace CountriesApp.Models
{
    public class Country {
        public string Name {get; set;} = string.Empty;
        public string OfficialName {get; set;} = string.Empty;
        public string Capital {get; set;} = string.Empty;
        public string Region {get; set;} = string.Empty;
        public long Population {get; set;}
        public string Flag {get; set;} = string.Empty;
    }
}