using System.Collections.Generic;
using System.Text;

namespace RefactoringKata
{
    public class OrdersWriter
    {
        private Orders _orders;

        public OrdersWriter(Orders orders)
        {
            _orders = orders;
        }

        public string GetContents()
        {
            var sb = new StringBuilder("{\"orders\": [");
            var orderCount = _orders.GetOrdersCount();

            for (var i = 0; i < orderCount; i++)
            {
                var order = _orders.GetOrder(i);

                sb.Append("{");
                sb.Append("\"id\": ");
                sb.Append(order.GetOrderId());
                sb.Append(", ");
                sb.Append("\"products\": [");

                var productsCount = order.GetProductsCount();

                for (var j = 0; j < productsCount; j++)
                {
                    var product = order.GetProduct(j);

                    sb.Append("{");
                    sb.Append("\"code\": \"");
                    sb.Append(product.Code);
                    sb.Append("\", ");
                    sb.Append("\"color\": \"");
                    sb.Append(_ProductColorMapping[product.Color]);
                    sb.Append("\", ");

                    if (product.Size != Product.SIZE_NOT_APPLICABLE)
                    {
                        sb.Append("\"size\": \"");
                        sb.Append(_ProductSizeMapping[product.Size]);
                        sb.Append("\", ");
                    }

                    sb.Append("\"price\": ");
                    sb.Append(product.Price);
                    sb.Append(", ");
                    sb.Append("\"currency\": \"");
                    sb.Append(product.Currency);
                    sb.Append("\"}, ");
                }

                if (productsCount > 0)
                {
                    RemoveLastXChars(sb, 2);
                }

                sb.Append("]");
                sb.Append("}, ");
            }

            if (orderCount > 0)
            {
                RemoveLastXChars(sb, 2);
            }

            return sb.Append("]}").ToString();
        }

        private void RemoveLastXChars(StringBuilder sb, int removeNumber)
        {
            sb.Remove(sb.Length - removeNumber, removeNumber);
        }

        private readonly Dictionary<int, string> _ProductSizeMapping = new Dictionary<int, string>()
        {
            {0,"Invalid Size"}, 
            {1,"XS"},
            {2,"S"},
            {3,"M"},
            {4,"L"},
            {5,"XL"},
            {6,"XXL"}
        };


        private readonly Dictionary<int, string> _ProductColorMapping = new Dictionary<int, string>()
        {
            {0,"no color"},
            {1,"blue"},
            {2,"red"},
            {3,"yellow"}
        };
    }
}