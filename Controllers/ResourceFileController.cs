using Microsoft.AspNetCore.Mvc;

namespace BTLG06WNC.Controllers;

public class ResourceFileController : Controller
{
    private readonly DbG06wncContext _context;
    private readonly IWebHostEnvironment _environment;
    public ResourceFileController(DbG06wncContext context, IWebHostEnvironment environment)
    {
        this._context = context;
        this._environment = environment;
    }

    public IActionResult Index() {
        var files = _context.ResourceFiles.ToList();
        return View(files);
    }

    public IActionResult Create() {
        var file = new ResourceFileView{
            DUploaddate = DateTime.Now,
            LFileCategories = _context.FileCategories.ToList()
        };
        return View(file);
    }

    [HttpPost]
    public IActionResult Create(ResourceFileView fileView){
        if(!ModelState.IsValid){
            fileView.LFileCategories = _context.FileCategories.ToList();
            return View(fileView);
        }
        ///thêm tệp tại đây
        string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff"); //tên mới cho file 
        newFileName += Path.GetExtension(fileView.SFilename!.FileName); //lấy extension của file nguồn
        string imageFullSavePath = getFullFilePath(fileView.ICategoryId, newFileName);
        using (var stream = System.IO.File.Create(imageFullSavePath)){
            fileView.SFilename.CopyTo(stream);   //sao chép file tới path đầy đủ với tên mới
        }
        ///
        var file = new ResourceFile{
            SFilename = newFileName,
            ICategoryId = fileView.ICategoryId,
            SDescription = fileView.SDescription,
            DUploaddate = fileView.DUploaddate
        };
        _context.ResourceFiles.Add(file);
        _context.SaveChanges();
        return RedirectToAction("Index", "ResourceFile");
    }

    private string getFullFilePath(int? fileCategoryID, string? fileName){
        string result = "";
        if(fileCategoryID == 1){
            result = _environment.WebRootPath + "/documents/" + fileName; // tạo path đầy đủ tới thư mục lưu trên server
        }
        else if(fileCategoryID == 2){
            result = _environment.WebRootPath + "/videos/" + fileName; // tạo path đầy đủ tới thư mục lưu trên server
        }
        else {
            result = _environment.WebRootPath + "/images/" + fileName; // tạo path đầy đủ tới thư mục lưu trên server
        }
        return result;
    }


    public IActionResult Delete(int id){
        var file = _context.ResourceFiles.FirstOrDefault(f => f.IResourceFileId == id);
        if(file != null){
            string imgPath = getFullFilePath(file.ICategoryId, file.SFilename);
            System.IO.File.Delete(imgPath);
            _context.ResourceFiles.Remove(file);
            _context.SaveChanges();
        }
        return RedirectToAction("Index","ResourceFile");
    }

    public IActionResult Edit(int id){
        var file = _context.ResourceFiles.FirstOrDefault(f => f.IResourceFileId == id);
        var fileView = new ResourceFileView{
            IResourceFileId = file!.IResourceFileId,
            DUploaddate = file.DUploaddate,
            SFileName = file.SFilename,
            ICategoryId = file.ICategoryId,
            SDescription = file.SDescription,
            LFileCategories = _context.FileCategories.ToList()
        };
        return View(fileView);
    }

    [HttpPost]
    public IActionResult Edit(ResourceFileView fileView){
        if(String.IsNullOrEmpty(fileView.SDescription)){
            ModelState.AddModelError("SDescription", "Phải có mô tả");
            return View(fileView);
        }
        var file = new ResourceFile{
            IResourceFileId = fileView.IResourceFileId,
            SFilename = fileView.SFileName,
            ICategoryId = fileView.ICategoryId,
            SDescription = fileView.SDescription,
            DUploaddate = fileView.DUploaddate
        };
        _context.ResourceFiles.Update(file);
        _context.SaveChanges();
        return RedirectToAction("Index", "ResourceFile");
    }
}
