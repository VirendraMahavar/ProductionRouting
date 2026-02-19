using NUnit.Framework;
using ProductionRouting.Domain.Engine;
using ProductionRouting.Domain.Models;
using System.Collections.Generic;

namespace ProductionRouting.Tests;

[TestFixture]
public class OrderFactExtractorTests
{
    private Order CreateValidOrder()
    {
        return new Order
        {
            OrderId = "TEST",
            PublisherNumber = "99999",
            OrderMethod = "POD",
            Shipments = new List<Shipment>
            {
                new Shipment
                {
                    ShipTo = new ShipTo
                    {
                        IsoCountry = "US"
                    }
                }
            },
            Items = new List<Item>
            {
                new Item
                {
                    PrintQuantity = 15,
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

    // ==========================================
    // SUCCESS CASE
    // ==========================================

    [Test]
    public void Extract_Should_Return_All_Facts_For_Valid_Order()
    {
        var order = CreateValidOrder();

        var facts = OrderFactExtractor.Extract(order);

        Assert.AreEqual("99999", facts["PublisherNumber"]);
        Assert.AreEqual("POD", facts["OrderMethod"]);
        Assert.AreEqual("US", facts["IsCountry"]);
        Assert.AreEqual(15, facts["PrintQuantity"]);
        Assert.AreEqual("PB", facts["BindTypeCode"]);
    }

    // ==========================================
    // MISSING SHIPMENTS
    // ==========================================

    [Test]
    public void Extract_Should_Not_Add_IsCountry_When_No_Shipments()
    {
        var order = CreateValidOrder();
        order.Shipments = null;

        var facts = OrderFactExtractor.Extract(order);

        Assert.IsFalse(facts.ContainsKey("IsCountry"));
    }

    // ==========================================
    // MISSING ITEMS
    // ==========================================

    [Test]
    public void Extract_Should_Not_Add_PrintQuantity_When_No_Items()
    {
        var order = CreateValidOrder();
        order.Items = null;

        var facts = OrderFactExtractor.Extract(order);

        Assert.IsFalse(facts.ContainsKey("PrintQuantity"));
        Assert.IsFalse(facts.ContainsKey("BindTypeCode"));
    }

    // ==========================================
    // EMPTY COMPONENTS
    // ==========================================

    [Test]
    public void Extract_Should_Not_Add_BindType_When_Component_Missing()
    {
        var order = CreateValidOrder();
        order.Items.First().Components = null;

        var facts = OrderFactExtractor.Extract(order);

        Assert.IsFalse(facts.ContainsKey("BindTypeCode"));
    }

    // ==========================================
    // NULL ATTRIBUTES
    // ==========================================

    [Test]
    public void Extract_Should_Handle_Null_Attributes()
    {
        var order = CreateValidOrder();
        order.Items.First().Components.First().Attributes = null;

        var facts = OrderFactExtractor.Extract(order);

        Assert.IsTrue(facts.ContainsKey("BindTypeCode"));
        Assert.IsNull(facts["BindTypeCode"]);
    }

    // ==========================================
    // MULTIPLE ITEMS - SHOULD TAKE FIRST
    // ==========================================

    [Test]
    public void Extract_Should_Take_First_Item_Only()
    {
        var order = CreateValidOrder();
        order.Items.Add(new Item { PrintQuantity = 99 });

        var facts = OrderFactExtractor.Extract(order);

        Assert.AreEqual(15, facts["PrintQuantity"]);
    }

    // ==========================================
    // MULTIPLE SHIPMENTS - SHOULD TAKE FIRST
    // ==========================================

    [Test]
    public void Extract_Should_Take_First_Shipment_Only()
    {
        var order = CreateValidOrder();
        order.Shipments.Add(new Shipment
        {
            ShipTo = new ShipTo { IsoCountry = "UK" }
        });

        var facts = OrderFactExtractor.Extract(order);

        Assert.AreEqual("US", facts["IsCountry"]);
    }

    // ==========================================
    // EMPTY ORDER
    // ==========================================

    [Test]
    public void Extract_Should_Handle_Empty_Order()
    {
        var order = new Order();

        var facts = OrderFactExtractor.Extract(order);

        Assert.IsTrue(facts.ContainsKey("PublisherNumber"));
        Assert.IsTrue(facts.ContainsKey("OrderMethod"));
        Assert.IsNull(facts["PublisherNumber"]);
        Assert.IsNull(facts["OrderMethod"]);
    }
}
