using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProductionRouting.Domain.Models;

namespace ProductionRouting.Domain.Engine;

public static class OrderFactExtractor
{
    public static Dictionary<string, object> Extract(Order order)
    {
        var facts = new Dictionary<string, object>();

        facts["PublisherNumber"] = order.PublisherNumber;
        facts["OrderMethod"] = order.OrderMethod;

        if (order.Shipments?.Any() == true)
        {
            facts["IsCountry"] = order.Shipments.First().ShipTo.IsoCountry;
        }

        if (order.Items?.Any() == true)
        {
            var item = order.Items.First();

            facts["PrintQuantity"] = item.PrintQuantity;

            var component = item.Components?.FirstOrDefault();
            if (component != null)
            {
                facts["BindTypeCode"] =
                    component.Attributes?.BindTypeCode;
            }
        }

        return facts;
    }
}

