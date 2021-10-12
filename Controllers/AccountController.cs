
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using register.Entities;
using register.Models;
using register.Services.Interfaces;

namespace register.Controllers
{
 public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRegister _register;
        public AccountController(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager, IRegister register)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _register = register;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                
             
           var GrossIncome= model.Salary*12;
           var tax = Tax ( model.Salary, 25);
           decimal annualTax=tax*12;

           decimal netSalary= GrossIncome-annualTax;

           var user = new  ApplicationUser
           {
                GrossIncome=GrossIncome,
                NetIncome=netSalary,
                Salary=model.Salary,
                FirstName=model.FirstName,
                LastName=model.LastName,
                Email=model.Email,
                UserName=model.Email,
               
                
           };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(model);

        }
         private decimal Tax(decimal SalaryAmount , int Percentage)
        {
            decimal tax= Percentage/100*SalaryAmount;
            return tax;       
         }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {

            //var branch = new Branch
            //{
            //    branchName = "Regie",
            //    address = "Naval"

            //};
            //branchContext.Branch.Add(branch);
            //branchContext.SaveChanges();

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(user);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
}