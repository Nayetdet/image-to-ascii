using System.Text.Json.Serialization;

namespace ImageToASCII.Settings
{
    [JsonSerializable(typeof(DisplaySettings))]
    public partial class DisplaySettingsContext : JsonSerializerContext 
    { 
    
    }
}
