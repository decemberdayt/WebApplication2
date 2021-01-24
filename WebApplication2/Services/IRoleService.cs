using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication2.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication2.Services
{
    public interface IRoleService
    {
        List<IdentityRole> GetAll();
        void CreateRole(IdentityRole Role);
        void DeleteRole(String user, IdentityRole Role);
    }

    public class RoleService : IRoleService
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public List<IdentityRole> GetAll()
        {
            var roles = context.Roles.ToList();
            return roles;
        }
        public void CreateRole(IdentityRole Role)
        {
            context.Roles.Add(Role);
            context.SaveChanges();
        }

    }
}