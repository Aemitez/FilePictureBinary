using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilePictureBinary.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            string image ="";
           // Image image ;
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);
                //Convert to binary 
                FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[fileStream.Length];
                fileStream.Read(buffer, 0, (int)fileStream.Length);
                fileStream.Close();

                //Save file image 
                //byteArrayToImage(buffer);

                //Convert to image
                image = "data:image/png;base64," + Convert.ToBase64String(buffer);
                TempData["pic"] = image;
            }
           
            

            return RedirectToAction("Index");
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            returnImage.Save("C:\\Users\\Watcharapong\\Downloads\\FilePictureBinary\\FilePictureBinary\\App_Data\\downloads\\123.jpg");
            return returnImage;

        }
    }
}