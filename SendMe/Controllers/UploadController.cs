using Microsoft.AspNet.Identity;
using SendMe.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public ActionResult Upload(UploadViewModel formData)
        {
            //Save File and Create Path
            var uploadedFile = Request.Files[0];
            string filename = $"{DateTime.Now.Ticks}{uploadedFile.FileName}";
            var serverPath = Server.MapPath(@"~\Upload");
            var fullPath = Path.Combine(serverPath, filename);

            uploadedFile.SaveAs(fullPath);

            Upload existing = db.Uploads
                .Where(u => u.RefId == formData.refId && u.TypeRef == formData.type)
                .FirstOrDefault();

            if (existing == null)
            {
                var uploadModel = new Upload
                {
                    File = filename,
                    RefId = formData.refId,
                    TypeRef = formData.type
                };

                db.Uploads.Add(uploadModel);
            }
            else
            {
                existing.File = filename;
                db.Entry(existing).State = EntityState.Modified;
            }

            db.SaveChanges();

            return Redirect(formData.ReturnUrl);
        }
        
    }
}
