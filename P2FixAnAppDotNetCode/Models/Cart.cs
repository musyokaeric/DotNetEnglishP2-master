using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {
        /// <summary>
        /// Read-only property for dispaly only
        /// </summary>
        public IEnumerable<CartLine> Lines => GetCartLineList();

        /// <summary>
        //Create a private lines property whose output will be pulled by public
        /// Lines property
        /// </summary>
        private List<CartLine> lines { get; set; }
        /// <summary>
        /// Return the actual cartline list
        /// </summary>
        /// <returns></returns>
        private List<CartLine> GetCartLineList()
        {
            //Checks if the lines property is null, and initializes if true
            if (lines == null)
            {
                lines = new List<CartLine>();
            }
            return lines;
        }

        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(Product product, int quantity)
        {
            GetCartLineList();
            bool addedOrUpdated = false;
            if (lines.Count > 0) //checks if a item has been added to cart
            {
                //goes through each cartline to check if the added product matches with those added to cart
                foreach (var line in lines)
                {
                    if (line.Product == product)
                    {
                        line.Quantity += quantity;
                        addedOrUpdated = true;
                        break;
                    }
                }
            }
            if (!addedOrUpdated) //checks if the product hasn't been added or its quantity updated
            {
                lines.Add(new CartLine { Product = product, Quantity = quantity });
            }
        }

        /// <summary>
        /// Removes a product form the cart
        /// </summary>
        public void RemoveLine(Product product) =>
            GetCartLineList().RemoveAll(l => l.Product.Id == product.Id);

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue()
        {
            double total = 0;
            //go through each line in the cartlines
            foreach (var line in Lines)
            {
                total += (line.Product.Price * line.Quantity);
            }
            return total;
        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            double priceSum = 0;
            double totalQuantity = 0;
            //go through each line in the cartlines
            foreach (var line in Lines)
            {
                priceSum += (line.Product.Price * line.Quantity);
                totalQuantity += line.Quantity;
            }
            //prevent dividebyzero exception
            if (totalQuantity == 0)
            {
                return 0;
            }
            return priceSum / totalQuantity;
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            //this line returns the producut from a list of lines
            //whose product id matches the id being queried
            return Lines.First(c => c.Product.Id == productId).Product;
        }

        /// <summary>
        /// Get a specifid cartline by its index
        /// </summary>
        public CartLine GetCartLineByIndex(int index)
        {
            return Lines.ToArray()[index];
        }

        /// <summary>
        /// Clears a the cart of all added products
        /// </summary>
        public void Clear()
        {
            List<CartLine> cartLines = GetCartLineList();
            cartLines.Clear();
        }
    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
