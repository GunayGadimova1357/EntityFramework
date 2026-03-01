namespace ef2.Models;

public class Customer
{
    public int Id { get; set; }
    public string FullName { get; set; }

    public virtual ICollection<CarOrder> CarOrders { get; set; }

}