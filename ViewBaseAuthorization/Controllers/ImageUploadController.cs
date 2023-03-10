using Dapper;
using Microsoft.AspNetCore.Mvc;
using ViewBasedAuthorization.Models;
using System.Data;
using System.Data.SqlClient;

namespace ViewBasedAuthorization.Controllers
{
    public class ImageUploadController : Controller
    {
        private readonly IDbConnection con;
        public ImageUploadController()
        {
            con = new SqlConnection("Server=localhost;Database=Maui; User ID=sa;Password=admin;");
        }
        [HttpGet]
        public IActionResult Index()
        {
            var result = con.Query<ImageFileDTO>("Select * From Image").ToList();
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ImageFileDTO image)
        {
            con.Query<ImageFileDTO>("insert into Image (Name,FileUrl) Values (@Name,@FileUrl)",image);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = con.Query<ImageFileDTO>("Select * From Image where Id = @id",new {Id=id}).FirstOrDefault();
            return View(result);
        }
        [HttpPost]
        public IActionResult Edit(ImageFileDTO image)
        {
            con.Query<ImageFileDTO>("Update Image SET Name = @Name,FileUrl =@FileUrl where Id = @Id", image);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var result = con.Query<ImageFileDTO>("delete from Image Where Id = @id",new {Id=id}).FirstOrDefault();
            return RedirectToAction("Index");
        }
    }
}
