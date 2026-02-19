using ProductionRouting.Domain.Entities;
using ProductionRouting.Domain.Enums;

namespace ProductionRouting.Infrastructure.Persistence
{
    public static class SeedData
    {
        public static void Seed(ProductionRoutingDbContext context)
        {
            if (context.Rulesets.Any())
                return;

            // ===============================
            // RULESET ONE (Publisher 99990)
            // ===============================
            var rulesetOne = new Ruleset
            {
                Name = "Ruleset One",
                Priority = 1,
                IsActive = true,
                Conditions = new List<Condition>
                {
                    new Condition
                    {
                        Field = "PublisherNumber",
                        Operator = OperatorType.Equals,
                        Value = "99990"
                    },
                    new Condition
                    {
                        Field = "OrderMethod",
                        Operator = OperatorType.Equals,
                        Value = "POD"
                    }
                },
                Rules = new List<Rule>
                {
                    new Rule
                    {
                        Name = "Rule 1",
                        ProductionPlant = "US",
                        Priority = 1,
                        Conditions = new List<Condition>
                        {
                            new Condition
                            {
                                Field = "BindTypeCode",
                                Operator = OperatorType.Equals,
                                Value = "PB"
                            },
                            new Condition
                            {
                                Field = "IsCountry",
                                Operator = OperatorType.Equals,
                                Value = "US"
                            },
                            new Condition
                            {
                                Field = "PrintQuantity",
                                Operator = OperatorType.LessThanOrEqual,
                                Value = "20"
                            }
                        }
                    }
                }
            };

            // ===============================
            // RULESET TWO (Publisher 99999)
            // ===============================
            var rulesetTwo = new Ruleset
            {
                Name = "Ruleset Two",
                Priority = 2,
                IsActive = true,
                Conditions = new List<Condition>
                {
                    new Condition
                    {
                        Field = "PublisherNumber",
                        Operator = OperatorType.Equals,
                        Value = "99999"
                    },
                    new Condition
                    {
                        Field = "OrderMethod",
                        Operator = OperatorType.Equals,
                        Value = "POD"
                    }
                },
                Rules = new List<Rule>
                {
                    // Rule 2 → UK
                    new Rule
                    {
                        Name = "Rule 2",
                        ProductionPlant = "UK",
                        Priority = 1,
                        Conditions = new List<Condition>
                        {
                            new Condition
                            {
                                Field = "BindTypeCode",
                                Operator = OperatorType.Equals,
                                Value = "CV"
                            },
                            new Condition
                            {
                                Field = "IsCountry",
                                Operator = OperatorType.Equals,
                                Value = "UK"
                            },
                            new Condition
                            {
                                Field = "PrintQuantity",
                                Operator = OperatorType.LessThanOrEqual,
                                Value = "20"
                            }
                        }
                    },

                    // Rule 3 → KGL
                    new Rule
                    {
                        Name = "Rule 3",
                        ProductionPlant = "KGL",
                        Priority = 2,
                        Conditions = new List<Condition>
                        {
                            new Condition
                            {
                                Field = "BindTypeCode",
                                Operator = OperatorType.Equals,
                                Value = "PB"
                            },
                            new Condition
                            {
                                Field = "IsCountry",
                                Operator = OperatorType.Equals,
                                Value = "US"
                            },
                            new Condition
                            {
                                Field = "PrintQuantity",
                                Operator = OperatorType.GreaterThanOrEqual,
                                Value = "20"
                            }
                        }
                    }
                }
            };

            context.Rulesets.AddRange(rulesetOne, rulesetTwo);
            context.SaveChanges();
        }
    }
}
