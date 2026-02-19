using System.Text.Json;

namespace ProductionRouting.Infrastructure.Configuration
{
    public class ConditionConfig
    {
        public string Field { get; set; }
        public string Operator { get; set; }
        public JsonElement Value { get; set; }
    }

}