using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SendMe.Models
{
    public class Upload
    {
        public int Id { get; set; }
        public string File { get; set; }
        public string TypeRef { get; set; }
        public string RefId { get; set; }

        public virtual string FilePath
        {
            get
            {
                return $"/Upload/{File}";
            }
        }
    }

    public class UploadViewModel
    {
        [Required]
        public HttpPostedFile File { get; set; }
        public string refId { get; set; }
        public string type { get; set; }
        public string ReturnUrl { get; set; }
    }
}