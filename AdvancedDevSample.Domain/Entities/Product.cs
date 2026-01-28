using AdvancedDevSample.Domain.Exceptions;

// struct ExceptionStruct
// {
//    public bool b;
//    public string s;
//
//    ExceptionStruct(bool b, string s)
//    {
//       this.b = b;
//       this.s = s;
//    }
// };

namespace AdvancedDevSample.Domain.Entities
{
   
   
public class Product
{
   // private void Check(ExceptionStruct[] tab)
   // {
   //    foreach(ExceptionStruct s in tab)
   //       if(!s.b) throw new DomainException(s.s); 
   //
   // }
   
   public Guid Id { get; private set; }
   public decimal Price  { get; private set; }
   public bool IsActive  { get; private set; }

   public Product()
   {
      IsActive = true;
   }

   public Product(Guid id, decimal price, bool isActive)
   {
      Id = id;
      Price = price;
      IsActive = isActive;
   }
   
   public void UpdatePrice(decimal newPrice)
   {
      // ExceptionStruct[] array = 
      // new ExceptionStruct[]{
      //    new ExceptionStruct() { b = newPrice <= 0, s = "1" },
      //    new ExceptionStruct() { b = !IsActive, s = "2" },
      // };
      //
      // Check(array);
      
      if(newPrice <=0)
         throw new DomainException("Price cannot be negative");
      if(!IsActive)
         throw new DomainException("Product is not active");
      
      Price = newPrice;

   
   }

   public void ApplyDiscount(decimal discount)
   {
      
   }
   

   
}
}    
