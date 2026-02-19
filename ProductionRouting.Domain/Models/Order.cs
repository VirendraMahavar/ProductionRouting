using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionRouting.Domain.Models;

public class Order
{
    public string OrderId { get; set; }
    public string PublisherNumber { get; set; }
    public string PublisherName { get; set; }
    public string OrderMethod { get; set; }

    public List<Shipment> Shipments { get; set; }
    public List<Item> Items { get; set; }
}

