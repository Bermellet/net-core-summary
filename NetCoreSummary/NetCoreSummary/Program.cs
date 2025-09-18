
using NetCoreSummary;

// Class example

var sale = new SaleWithTax(15, 1.21m);
var message = sale.GetInfo();

Console.WriteLine(message);

// Generics example

var numbers = new MyList<int>(5);
var names = new MyList<string>(5);

for (int i = 1; i<10; i++)
{
    numbers.Add(i);
}

Console.WriteLine(numbers.GetContent());







