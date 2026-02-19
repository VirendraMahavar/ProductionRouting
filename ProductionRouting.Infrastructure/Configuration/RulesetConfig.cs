namespace ProductionRouting.Infrastructure.Configuration
{
    public class RulesetConfig
    {
        public string Name { get; set; }
        public List<ConditionConfig> Conditions { get; set; }
        public List<RuleConfig> Rules { get; set; }
    }

}