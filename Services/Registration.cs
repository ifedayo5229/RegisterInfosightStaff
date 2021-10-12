using Microsoft.EntityFrameworkCore;
using register.Entities;
using register.Models;
using register.Services.Interfaces;

namespace register.Services
{
    public class Registration : IRegister
    {

           private readonly  AppDBContext  _options;

        public Registration( AppDBContext  options)
        {
            _options = options; 
        }

        public void RegisterNewUser(RegisterViewModel model)
        {
           var GrossIncome= model.Salary*12;
           var tax = Tax ( model.Salary, 25);
           decimal annualTax=tax*12;

           decimal netSalary= GrossIncome-annualTax;

           var entry = new  ApplicationUser
           {
                GrossIncome=GrossIncome,
                NetIncome=netSalary,
                Salary=model.Salary,
                FirstName=model.FirstName,
                LastName=model.LastName,
                Email=model.Email,
               
                
           };

              _options.Add(entry);
              _options.SaveChanges();

        }

        private decimal Tax(decimal SalaryAmount , int Percentage)
        {
            decimal tax= Percentage/100*SalaryAmount;
            return tax;       
         }

    }
}