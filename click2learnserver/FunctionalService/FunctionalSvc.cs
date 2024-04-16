﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ModelService;
using Serilog;

namespace FunctionalService
{
    public class FunctionalSvc: IFunctionalSvc
    {
        private readonly AdminUserOptions _adminUserOptions;
        private readonly AppUserOptions _appUserOptions;
        private readonly UserManager<ApplicationUser> _userManager;
        public FunctionalSvc(IOptions<AdminUserOptions> adminUserOptions, IOptions<AppUserOptions> appUserOptions, UserManager<ApplicationUser> userManager)
        {
            _adminUserOptions = adminUserOptions.Value;
            _appUserOptions = appUserOptions.Value;
            _userManager = userManager;
        }

        public async Task CreateDefaultAdminUser()
        {
            try
            {
                var adminUser = new ApplicationUser
                {
                    Email = _adminUserOptions.Email,
                    UserName = _adminUserOptions.Username,
                    EmailConfirmed = true,
                    ProfilePic = GetDefaultProfilePic(), // TODO
                    PhoneNumber = "1234567890",
                    PhoneNumberConfirmed = true,
                    FirstName = _adminUserOptions.FirstName,
                    LastName = _adminUserOptions.LastName,
                    UserRole = "Administrator",
                    IsActive = true,
                    UserAddresses = new List<AddressModel>
                    {
                        new AddressModel {Country = _adminUserOptions.Country, Type = "Billing"},
                        new AddressModel {Country = _adminUserOptions.Country, Type = "Shipping"}
                    }
                };


                // Using "UserManager" from dotnet identity, we can access the application user table and we can insert above data into table.
                var result = await _userManager.CreateAsync(adminUser, _adminUserOptions.Password);

                if(result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Administrator");
                    Log.Information("Admin User Created {UserName}", adminUser.UserName);
                }
                else
                {
                    var errorString = string.Join(",", result.Errors);
                    Log.Error("Error while creating user {Error}", errorString);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while creating user {Error} {Stack} {InnerException} {Source}", ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
        }

        public async Task CreateDefaultUser()
        {
            try
            {
                var appUser = new ApplicationUser
                {
                    Email = _appUserOptions.Email,
                    UserName = _appUserOptions.Username,
                    EmailConfirmed = true,
                    ProfilePic = GetDefaultProfilePic(), // TODO
                    PhoneNumber = "1234567890",
                    PhoneNumberConfirmed = true,
                    FirstName = _appUserOptions.FirstName,
                    LastName = _appUserOptions.LastName,
                    UserRole = "Customer",
                    IsActive = true,
                    UserAddresses = new List<AddressModel>
                    {
                        new AddressModel {Country = _appUserOptions.Country, Type = "Billing"},
                        new AddressModel {Country = _appUserOptions.Country, Type = "Shipping"}
                    }
                };


                // Using "UserManager" from dotnet identity, we can access the application user table and we can insert above data into table.
                var result = await _userManager.CreateAsync(appUser, _appUserOptions.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(appUser, "Customer");
                    Log.Information("App User Created {UserName}", appUser.UserName);
                }
                else
                {
                    var errorString = string.Join(",", result.Errors);
                    Log.Error("Error while creating user {Error}", errorString);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while creating user {Error} {Stack} {InnerException} {Source}", ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
        }

        private string GetDefaultProfilePic() // Further implementation
        {
            return string.Empty;
        }
    }
}
