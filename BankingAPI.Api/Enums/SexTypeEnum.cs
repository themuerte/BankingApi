using System.Text.Json.Serialization;

namespace BankingAPI.Api.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]

    public enum SexTypeEnum
    {
        Male,
        Female,
        Other
    }
}