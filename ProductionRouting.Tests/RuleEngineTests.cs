using NUnit.Framework;
using ProductionRouting.Domain.Engine;
using ProductionRouting.Domain.Entities;
using ProductionRouting.Domain.Enums;
using ProductionRouting.Domain.Models;
using FluentAssertions;
using System.Collections.Generic;

namespace ProductionRouting.Tests;

[TestFixture]
public class RuleEngineTests
{
    private RuleEngine _engine;

    [SetUp]
    public void Setup()
    {
        var ruleset = new Ruleset
        {
            Name = "Ruleset Two",
            Priority = 1,
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
                },
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

        _engine = new RuleEngine(new List<Ruleset> { ruleset });
    }

    private Order CreateOrder(int quantity)
    {
        return new Order
        {
            OrderId = "1245101",
            PublisherNumber = "99999",
            OrderMethod = "POD",
            Shipments = new List<Shipment>
            {
                new Shipment
                {
                    ShipTo = new ShipTo { IsoCountry = "US" }
                }
            },
            Items = new List<Item>
            {
                new Item
                {
                    PrintQuantity = quantity,
                    Components = new List<Component>
                    {
                        new Component
                        {
                            Attributes = new ComponentAttributes
                            {
                                BindTypeCode = "PB"
                            }
                        }
                    }
                }
            }
        };
    }

    [Test]
    public void Should_Return_US_When_Quantity_Less_Than_20()
    {
        var order = CreateOrder(10);

        var result = _engine.Evaluate(order);

        result.Matched.Should().BeTrue();
        result.ProductionPlant.Should().Be("US");
        result.MatchedRule.Should().Be("Rule 1");
    }

    [Test]
    public void Should_Return_KGL_When_Quantity_Greater_Than_Or_Equal_20()
    {
        var order = CreateOrder(25);

        var result = _engine.Evaluate(order);

        result.Matched.Should().BeTrue();
        result.ProductionPlant.Should().Be("KGL");
        result.MatchedRule.Should().Be("Rule 3");
    }

    [Test]
    public void Should_Return_NoMatch_When_Publisher_Invalid()
    {
        var order = CreateOrder(10);
        order.PublisherNumber = "11111";

        var result = _engine.Evaluate(order);

        result.Matched.Should().BeFalse();
    }
}
