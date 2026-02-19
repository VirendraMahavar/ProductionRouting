using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionRouting.Domain.Models;

public class Item
{
    public string Sku { get; set; }
    public int PrintQuantity { get; set; }

    public List<Component> Components { get; set; }
}

