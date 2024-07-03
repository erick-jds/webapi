using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class DatabaseMigrationService
    {
            public static void DatabaseMigrationServiceInitialisation(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
            db.Database.Migrate();
            }
        }
    }
}