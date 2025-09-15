
var sale = new SaleWithTax(15, 1.21m);
var message = sale.GetInfo();

Console.WriteLine(message);


class SaleWithTax : Sale, ISave
{
    public decimal Tax { get; set; }

    public SaleWithTax(decimal total, decimal tax) : base(total)
    {
        Tax = tax;
    }

    public override string GetInfo()
    {
        return $"The total is {Total}. The tax is {Tax}";
    }

    public string GetInfo(string message)
    {
        return message;
    }

    public void Save()
    {
        Console.WriteLine("Sale saved");
    }
}

class Sale : ISale
{
    public decimal Total { get; set; }

    public Sale(decimal total)
    {
        Total = total;
    }

    public virtual string GetInfo()
    {
        return $"The total is {Total}";
    }
}
