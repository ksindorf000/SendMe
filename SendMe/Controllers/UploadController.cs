using Microsoft.AspNet.Identity;
using SendMe.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace SendMe.Controllers
{
    public class UploadController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        
        //----------------------------
        //      Process Upload
        //----------------------------
        // POST: Image Upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Upload(UploadViewModel formData)
        {
            //Save File and Create Path
            var uploadedFile = Request.Files[0];
            string filename = $"{DateTime.Now.Ticks}{uploadedFile.FileName}";
            var serverPath = Server.MapPath(@"~\Upload");
            var fullPath = Path.Combine(serverPath, filename);

            //Save Image
            uploadedFile.SaveAs(fullPath);

            //Create Upload Entry
            var uploadModel = new Upload
            {
                File = filename,
                RefId = formData.refId,
                TypeRef = formData.type
            };

            db.Uploads.Add(uploadModel);
            db.SaveChanges();
        }

    }
}
