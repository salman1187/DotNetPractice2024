using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TaxCalculatorFactory factory = new TaxCalculatorFactory();
            BillingSystem billingSystem = new BillingSystem(factory.CreateTaxCalculator());
            billingSystem.GenerateBill();
        }
    }
    class TaxCalculatorFactory
    {
        public ITaxCalculator CreateTaxCalculator()
        {
            string className = ConfigurationManager.AppSettings["CALC"];
            //Reflextion
            Type theType = Type.GetType(className);
            return (ITaxCalculator) Activator.CreateInstance(theType);  
        }
    }
    public class BillingSystem
    {
        private ITaxCalculator taxCalculator;
        public BillingSystem(ITaxCalculator state)
        {
            taxCalculator = state;
        }
        public void GenerateBill()
        {
            //scan all products and find the total 
            double total = 5843.90;
            //calculate discounts if any
            //apply coupons
            //calculate tax
            double tax = taxCalculator.CalculateTax(total);
            //generate bill
        }
    }
    public interface ITaxCalculator
    {
        double CalculateTax(double total);  
    }
    public class KATaxCalculator : ITaxCalculator
    {
        public double CalculateTax(double total)
        {
            Console.WriteLine("Using KA tax calculator: ");
            double vat = 120;
            int cess = 60;
            int st = 30;
            int sbt = 100;
            int abc = 20;
            double tax = vat + cess + st + sbt + abc;
            return tax;
        }
    }
    public class TNTaxCalculator : ITaxCalculator
    {
        public double CalculateTax(double total)
        {
            Console.WriteLine("Using TN tax calculator: ");
            double vat = 120;
            int cess = 60;
            int st = 30;
            int sbt = 90;
            int xyz = 15;
            double tax = vat + cess + st + sbt + xyz;
            return tax;
        }
    }
}
