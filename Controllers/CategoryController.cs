using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BTLG06WNC.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;

namespace BTLG06WNC.Controllers;

public class CategoryController: Controller
{

    private readonly DbG06wncContext _context;
    public CategoryController(DbG06wncContext context)
    {
        this._context = context;
    }

    public IActionResult Index(){
        var categories = _context.Categories.ToList();
        return View(categories);
    }

    public IActionResult Create(){
        return View();
    }

    [HttpPost]
    public IActionResult Create(Category category){
        if(category != null){
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index", "Category");
        }
        return View(category);
    }

    public IActionResult Edit(int id){
        var category = _context.Categories.FirstOrDefault(cate => cate.ICategoryId == id);
        return View(category);
    }

    [HttpPost]
    public IActionResult Edit(Category category){
        if(category != null){
            _context.Categories.Update(category);
            _context.SaveChanges();
            return RedirectToAction("Index", "Category");
        }
        return View(category);
    }

    public IActionResult Delete(int id){
        var category = _context.Categories.FirstOrDefault(cate => cate.ICategoryId == id);
        if(category != null){
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
        return RedirectToAction("Index", "Category");
    }
}
