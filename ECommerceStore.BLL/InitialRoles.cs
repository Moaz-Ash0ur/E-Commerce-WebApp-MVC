using ECommerceStore.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceStore.BLL
{
    public static class InitialRoles
    {

        public static async Task SeedData(UserManager<ApplicationUser>? userManager,RoleManager<IdentityRole>? roleManager) 
        { 
        
         if(userManager == null || roleManager == null)
            {
                Console.WriteLine("User Manager or Role Mamager is Null");
                return;
            }

            //check if have admin role or not
            var isExistRole = await roleManager.RoleExistsAsync("admin");
            if (!isExistRole)
            {
                Console.WriteLine("Admin Role is not defiend, will be created");
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }

            isExistRole = await roleManager.RoleExistsAsync("customer");
            if (!isExistRole)
            {
                Console.WriteLine("customer Role is not defiend, will be created");
                await roleManager.CreateAsync(new IdentityRole("customer"));
            }


            //create admin user
            //check if at least have one admin 
            var adminUser =  await userManager.GetUsersInRoleAsync("admin");
            if (adminUser.Any())
            {
                Console.WriteLine("Admin User already exist");
                return;
            }


            var user = new ApplicationUser()
            {
                FirstName = "Moaz",
                LastName = "Ashour",
                UserName = "adminMoaz@gmail.com",
                Email = "adminMoaz@gmail.com",
                Address = "Cairo,Egypt",
                CreatedAt = DateTime.Now,           
            };

            string initialPassword = "asdf123";

             
            var newUser = await userManager.CreateAsync(user, initialPassword);
            if (newUser.Succeeded) {

                await userManager.AddToRoleAsync(user,"admin");
                Console.WriteLine("Admin User created Successfully");
                Console.WriteLine("Emial" + user.Email);
                Console.WriteLine("Initial Pass" + initialPassword);

            }



        }


    }
}
