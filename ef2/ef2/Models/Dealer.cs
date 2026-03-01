namespace ef2.Models;

public class Dealer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }

    public virtual ICollection<Car> Cars { get; set; }

}