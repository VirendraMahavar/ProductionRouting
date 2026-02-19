using NUnit.Framework;
using ProductionRouting.Domain.Entities;
using ProductionRouting.Domain.Enums;
using System;

namespace ProductionRouting.Tests;

[TestFixture]
public class ConditionTests
{
    // ==========================================
    // EQUALS OPERATOR
    // ==========================================

    [Test]
    public void Evaluate_Equals_Should_Return_True_When_Values_Match()
    {
        var condition = new Condition
        {
            Operator = OperatorType.Equals,
            Value = "POD"
        };

        var result = condition.Evaluate("POD");

        Assert.IsTrue(result);
    }

    [Test]
    public void Evaluate_Equals_Should_Return_False_When_Values_Do_Not_Match()
    {
        var condition = new Condition
        {
            Operator = OperatorType.Equals,
            Value = "POD"
        };

        var result = condition.Evaluate("OFFSET");

        Assert.IsFalse(result);
    }

    [Test]
    public void Evaluate_Equals_Should_Return_False_When_Actual_Is_Null()
    {
        var condition = new Condition
        {
            Operator = OperatorType.Equals,
            Value = "POD"
        };

        var result = condition.Evaluate(null);

        Assert.IsFalse(result);
    }

    // ==========================================
    // LESS THAN OR EQUAL
    // ==========================================

    [Test]
    public void Evaluate_LessThanOrEqual_Should_Return_True_When_Less()
    {
        var condition = new Condition
        {
            Operator = OperatorType.LessThanOrEqual,
            Value = "20"
        };

        var result = condition.Evaluate(10);

        Assert.IsTrue(result);
    }

    [Test]
    public void Evaluate_LessThanOrEqual_Should_Return_True_When_Equal()
    {
        var condition = new Condition
        {
            Operator = OperatorType.LessThanOrEqual,
            Value = "20"
        };

        var result = condition.Evaluate(20);

        Assert.IsTrue(result);
    }

    [Test]
    public void Evaluate_LessThanOrEqual_Should_Return_False_When_Greater()
    {
        var condition = new Condition
        {
            Operator = OperatorType.LessThanOrEqual,
            Value = "20"
        };

        var result = condition.Evaluate(25);

        Assert.IsFalse(result);
    }

    // ==========================================
    // GREATER THAN OR EQUAL
    // ==========================================

    [Test]
    public void Evaluate_GreaterThanOrEqual_Should_Return_True_When_Greater()
    {
        var condition = new Condition
        {
            Operator = OperatorType.GreaterThanOrEqual,
            Value = "20"
        };

        var result = condition.Evaluate(25);

        Assert.IsTrue(result);
    }

    [Test]
    public void Evaluate_GreaterThanOrEqual_Should_Return_True_When_Equal()
    {
        var condition = new Condition
        {
            Operator = OperatorType.GreaterThanOrEqual,
            Value = "20"
        };

        var result = condition.Evaluate(20);

        Assert.IsTrue(result);
    }

    [Test]
    public void Evaluate_GreaterThanOrEqual_Should_Return_False_When_Less()
    {
        var condition = new Condition
        {
            Operator = OperatorType.GreaterThanOrEqual,
            Value = "20"
        };

        var result = condition.Evaluate(10);

        Assert.IsFalse(result);
    }

    // ==========================================
    // INVALID NUMERIC INPUT
    // ==========================================

    [Test]
    public void Evaluate_NumericOperator_Should_Throw_When_Invalid_Number()
    {
        var condition = new Condition
        {
            Operator = OperatorType.LessThanOrEqual,
            Value = "20"
        };

        Assert.Throws<FormatException>(() =>
            condition.Evaluate("INVALID_NUMBER"));
    }

    // ==========================================
    // UNKNOWN OPERATOR
    // ==========================================

    [Test]
    public void Evaluate_Should_Return_False_For_Unknown_Operator()
    {
        var condition = new Condition
        {
            Operator = (OperatorType)999,
            Value = "ANY"
        };

        var result = condition.Evaluate("ANY");

        Assert.IsFalse(result);
    }

    // ==========================================
    // NULL VALUE PROPERTY
    // ==========================================

    [Test]
    public void Evaluate_Equals_Should_Handle_Null_Value_Property()
    {
        var condition = new Condition
        {
            Operator = OperatorType.Equals,
            Value = null
        };

        var result = condition.Evaluate("ANY");

        Assert.IsFalse(result);
    }
}
