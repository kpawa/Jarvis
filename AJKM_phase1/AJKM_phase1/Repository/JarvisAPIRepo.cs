using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AJKM_phase1.ViewModels;

namespace AJKM_phase1.Repository
{
    public class JarvisAPIRepo
    {
        public async Task<List<JarvisAPI>> GetAll()
        {
            JarvisEntities db = new JarvisEntities();
            List<JarvisAPI> response = new List<JarvisAPI>();
            response = await db.AspNetUsers.Select(j => new JarvisAPI { UserName = j.UserName, Email = j.Email }).ToListAsync();
            return response;
        }
    }
}