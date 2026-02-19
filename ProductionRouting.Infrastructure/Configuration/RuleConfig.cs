namespace ProductionRouting.Infrastructure.Configuration
{
    public class RuleConfig
    {
        public string Name { get; set; }
        public List<ConditionConfig> Conditions { get; set; }
        public ResultConfig Result { get; set; }
    }

}