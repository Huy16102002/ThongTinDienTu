using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BTLG06WNC.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;

namespace BTLG06WNC.Controllers;

public class ContentController : Controller
{

    private readonly DbG06wncContext _context;
    private readonly IWebHostEnvironment _environment;
    public ContentController(DbG06wncContext context, IWebHostEnvironment environment)
    {
        this._context = context;
        this._environment = environment;
    }

    public IActionResult Index()
    {   
        var categoriesL = _context.Categories.ToList();
        List<ContentView> contents = new List<ContentView>();
        foreach(var content in _context.Contents){
            contents.Add(new ContentView{
                IContentId = content.IContentId,
                STitle = content.STitle,
                DCreatedate = content.DCreatedate,
                SMainbody = content.SMainbody,
                SSource = content.SSource,
                ICategoryId = content.ICategoryId,
                SFilename = content.SImage,
                categories = categoriesL,
                sImage = null
            });
        }
        return View(contents);
    }

    public IActionResult Create()
    {
        var contentView = new ContentView{
            DCreatedate = DateTime.Now,
            categories = _context.Categories.ToList()
        };
        return View(contentView);
    }


    [HttpPost]
    public IActionResult Create(ContentView contentView){
        if(contentView.sImage == null){
            ModelState.AddModelError("SImage","Phải có ảnh cho tin tức");
        }
        if(!ModelState.IsValid){
            contentView.categories = _context.Categories.ToList();
            return View(contentView);
        }
        ///xử lý thêm ảnh
        string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff"); //tên mới cho file ảnh
        newFileName += Path.GetExtension(contentView.sImage!.FileName); //lấy extension của file nguồn
        string imageFullSavePath = _environment.WebRootPath + "/images/" + newFileName; // tạo path đầy đủ tới thư mục lưu trên server
        using (var stream = System.IO.File.Create(imageFullSavePath)){
            contentView.sImage.CopyTo(stream);   //sao chép file tới path đầy đủ với tên mới
        }
        ResourceFile image = new ResourceFile{
            DUploaddate = DateTime.Now,
            SFilename = newFileName,
            SDescription = contentView.STitle,
            ICategoryId = 3
        };
        _context.ResourceFiles.Add(image);
        _context.SaveChanges();

        ///xử lí thêm content
        Content content = new Content{
            STitle = contentView.STitle,
            SImage = newFileName,
            SSource = contentView.SSource,
            SMainbody = contentView.SMainbody,
            DCreatedate = contentView.DCreatedate,
            ICategoryId = contentView.ICategoryId
        };
        _context.Contents.Add(content);
        _context.SaveChanges();

        return RedirectToAction("Index","Content");
    }


    public IActionResult Delete(int id){
        var content = _context.Contents.FirstOrDefault(p => p.IContentId == id);
        string imgPath = _environment.WebRootPath + "/images/" + content!.SImage;
        System.IO.File.Delete(imgPath);
        _context.Contents.Remove(content);
        _context.SaveChanges();
        return RedirectToAction("Index","Content");
    }

    public IActionResult Edit(int id){
        var content = _context.Contents.FirstOrDefault(c => c.IContentId == id);
        if(content == null){
            RedirectToAction("Index","Content");
        }
        var contentView = new ContentView{
            IContentId = content!.IContentId,
            STitle = content.STitle,
            DCreatedate = content!.DCreatedate,
            SFilename = content.SImage,
            categories = _context.Categories.ToList(),
            SSource = content.SSource,
            SMainbody = content.SMainbody
        };
        return View(contentView);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(ContentView contentView){
        if(!ModelState.IsValid){
            contentView.categories = _context.Categories.ToList();
            return View(contentView);
        }
        ///xử lý cập nhật file ảnh
        string newFileName = contentView.SFilename!;
        if(contentView.sImage != null){
            newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(contentView.sImage!.FileName);

            string imgFullPath = _environment.WebRootPath + "/images/" + newFileName;
            using (var stream = System.IO.File.Create(imgFullPath)){
                contentView.sImage.CopyTo(stream);
            }
            if(!contentView.SFilename.IsNullOrEmpty()){
                string oldImgFullPath = _environment.WebRootPath + "/images/" + contentView.SFilename;
                System.IO.File.Delete(oldImgFullPath);
            }
        }
        ///cập nhật bản ghi
        var content = new Content{
            IContentId = contentView.IContentId,
            DCreatedate = contentView.DCreatedate,
            ICategoryId = contentView.ICategoryId,
            SImage = newFileName,
            SMainbody = contentView.SMainbody,
            STitle = contentView.STitle,
            SSource = contentView.SSource
        };
        _context.Contents.Update(content);
        _context.SaveChanges();
        return RedirectToAction("Index","Content");
    }


    [HttpGet]
    public IActionResult Search(string searchInputString){
        var contents = from content in _context.Contents join category in _context.Categories
                        on content.ICategoryId equals category.ICategoryId
                        select new {
                            content.IContentId,
                            content.ICategoryId,
                            categoryName = category.STitle,
                            content.STitle,
                            content.SMainbody,
                            content.SImage,
                            content.DCreatedate,
                            content.SSource
                        };
        if(String.IsNullOrEmpty(searchInputString)){
            return Json(contents);
        }
        int tempID;
        if(int.TryParse(searchInputString,out tempID)){
            contents = contents.Where(content => content.IContentId == int.Parse(searchInputString));
        }
        else{
            contents = contents.Where(content => content.STitle!.Contains(searchInputString) || content.categoryName!.Contains(searchInputString));
        }
        return Json(contents);
    }


    public IActionResult Detail(int id){
        var content = _context.Contents.FirstOrDefault(p => p.IContentId == id);
        var contentView = new ContentView {
            ICategoryId = content!.ICategoryId,
            DCreatedate = content.DCreatedate,
            SSource = content.SSource,
            SFilename = content.SImage,
            STitle = content.STitle,
            SMainbody = content.SMainbody,
        };

        contentView!.relatedContents = _context.Contents.Where(c => c.IContentId != id && c.ICategoryId == content.ICategoryId).Take(10).ToList();
        return View(contentView);
    }


    
}
