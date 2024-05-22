using Microsoft.AspNetCore.Mvc;

namespace BTLG06WNC;

public class AccountController : Controller
{
    private DbG06wncContext _context;
    private readonly IWebHostEnvironment _environment;

    public AccountController(DbG06wncContext context, IWebHostEnvironment environment)
    {
        this._context = context;
        this._environment = environment;
    }

    public IActionResult Index(){
        var members = _context.Accounts.Where(p => p.IRoleId == 2).ToList();
        return View(members);
    }

    public IActionResult Edit(int id){
        var account = _context.Accounts.FirstOrDefault(p => p.IAccountId == id);
        var accountView = new AccountView {
            IAccountId = account!.IAccountId,
            SAvatar = account.SAvatar,
            SEmail = account.SEmail,
            SName = account.SName,
            SPassword = account.SPassword,
            SPhone = account.SPhone,
            DBirthofdate = account.DBirthofdate
        };
        return View(accountView);
    }

    [HttpPost]
    public IActionResult Edit(AccountView accountView){
        var account = _context.Accounts.FirstOrDefault(p => p.IAccountId == accountView.IAccountId);
        if(account == null){
            return View(accountView);
        }
        ///xử lý cập nhật file ảnh
        string newFileName = accountView.SAvatar!;
        if(accountView.Image != null){
            newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(accountView.Image!.FileName);

            string imgFullPath = _environment.WebRootPath + "/images/avatars/" + newFileName;
            using (var stream = System.IO.File.Create(imgFullPath)){
                accountView.Image.CopyTo(stream);
            }
            if(!String.IsNullOrEmpty(account.SAvatar)){
                string oldImgFullPath = _environment.WebRootPath + "/images/avatars/" + account.SAvatar;
                System.IO.File.Delete(oldImgFullPath);
            }
        }
        account.SName = accountView.SName;
        account.SAvatar = newFileName;
        account.DBirthofdate = accountView.DBirthofdate;
        account.SEmail = accountView.SEmail;
        account.SPassword = accountView.SPassword;
        account.SPhone = accountView.SPhone;
        _context.Accounts.Update(account);
        _context.SaveChanges();
        return RedirectToAction("Index","Account");
    }


    public IActionResult Delete(int id){
        var account = _context.Accounts.FirstOrDefault(p => p.IAccountId == id);
        var feedbacks = _context.Feedbacks.Where(p => p.IAccountId == id).ToList();
        if(feedbacks.Any()){
            _context.Feedbacks.RemoveRange(feedbacks);
        }
        _context.Accounts.Remove(account!);
        _context.SaveChanges();
        return RedirectToAction("Index","Account");
    }

    public IActionResult Create(){
        var accountView = new AccountView();
        accountView.DBirthofdate = DateTime.Now;
        return View(accountView);
    }

    [HttpPost]
    public IActionResult Create(AccountView accountView){
        if(!ModelState.IsValid){
            return View(accountView);
        }
        foreach(var acc in _context.Accounts.ToList()){
            if(acc.SName!.Equals(accountView.SName)){
                ModelState.AddModelError("SName", "Tên tài khoản đã tồn tại");
                return View(accountView);
            }
            if(acc.SEmail!.Equals(accountView.SEmail)){
                ModelState.AddModelError("SEmail", "Email đã tồn tại");
                return View(accountView);
            }
        }
        ///xử lý thêm ảnh
        string newFileName = "";
        if(accountView.Image != null){
            newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff"); //tên mới cho file ảnh
            newFileName += Path.GetExtension(accountView.Image!.FileName); //lấy extension của file nguồn
            string imageFullSavePath = _environment.WebRootPath + "/images/avatars/" + newFileName; // tạo path đầy đủ tới thư mục lưu trên server
            using (var stream = System.IO.File.Create(imageFullSavePath)){
                accountView.Image.CopyTo(stream);   //sao chép file tới path đầy đủ với tên mới
            }
        }
        
        var account = new Account{
            SName = accountView.SName,
            DBirthofdate = accountView.DBirthofdate,
            SAvatar = newFileName,
            SPhone = accountView.SPhone,
            SPassword  = accountView.SPassword,
            SEmail = accountView.SEmail,
            IRoleId = 2
        };
        _context.Accounts.Add(account);
        _context.SaveChanges();
        return RedirectToAction("Index","Account");
    }
        
    public IActionResult Search(string searchInputString){
        var account = from acc in _context.Accounts where acc.IRoleId == 2 select acc;
        if(String.IsNullOrEmpty(searchInputString)){
            return Json(account);
        }
        account = account.Where(x => x.SName!.Contains(searchInputString) || x.SPhone!.Contains(searchInputString));
        return Json(account);
    }


    public IActionResult Login() { 
        if(HttpContext.Session.GetString("Username") == null){
            return View(); 
        }
        return RedirectToAction("Index","Home");
    }

    [HttpPost] 
    public IActionResult Login(LoginView model) { 
        if(!ModelState.IsValid){
            model.SPassword = "";
            return View(model);
        }
        var account = _context.Accounts.ToList().FirstOrDefault(p => p.SEmail == model.SEmail);
        if(account != null){
            if(account.SPassword!.Equals(model.SPassword)){
                HttpContext.Session.SetString("User",account.SEmail!.ToString());
                HttpContext.Session.SetInt32("Role", account.IRoleId!.Value);
            }else{
                ModelState.AddModelError("SPassword","sai mật khẩu");
                return View(model);
            }
        }
        return RedirectToAction("Index","Home");
    }


    public IActionResult Logout(){
        HttpContext.Session.Clear();
        HttpContext.Session.Remove("Username");
        return RedirectToAction("Login","Account");
    }


    public IActionResult Register() { 
        return View();
    }

    [HttpPost]
    public IActionResult Register(RegisterView register) { 
        if(!ModelState.IsValid){
            return View(register);
        }
        if(register.SPassword != register.SAuthenPassword){
            ModelState.AddModelError("SAuthenPassword", "Xác thực mật khẩu không khớp");
            return View(register);
        }
        Account account = new Account{
            IRoleId = 2,
            SEmail = register.SEmail,
            SName = register.SAccountName,
            DBirthofdate = register.DBirthofdate,
            SPassword = register.SPassword,
        };
        _context.Accounts.Add(account);
        _context.SaveChanges();
        return RedirectToAction("Login","Account");
    }

    public IActionResult Information(){
        var account = _context.Accounts.ToList().FirstOrDefault(
            p => p.SEmail.Equals(HttpContext.Session.GetString("User"))
        );
        if(account != null){
            return View(account);
        }
        return RedirectToAction("Index","Home");
    }
}
