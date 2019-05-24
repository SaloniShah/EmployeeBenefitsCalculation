using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace EmployeeBenefitsCalculation.Managers.Discounts
{
    public class DiscountHelper : IDiscountHelper
    {
        public List<IDiscount> GetApplicableDiscounts()
        {
            var discounts = new List<IDiscount>();
            var discountClassNames = new List<DiscountName>();
            string dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string path = dir + @"\Discounts\Discounts.json";
            using (var r = new StreamReader(path))
            {
                var json = r.ReadToEnd();
                discountClassNames.AddRange(JsonConvert.DeserializeObject<List<DiscountName>>(json));
            }

            discountClassNames.ForEach(c =>
            {
                discounts.Add((IDiscount)GetDiscountClass(c.discountName));
            });

            return discounts;
        }

        public object GetDiscountClass(string discountClassName)
        {
            Type t = Type.GetType(discountClassName);
            return Activator.CreateInstance(t);
        }
    }

    public class DiscountName
    {
        public string discountName { get; set; }
    }


}
