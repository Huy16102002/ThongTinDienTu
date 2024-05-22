using Microsoft.AspNetCore.Mvc;

namespace BTLG06WNC;

public class FeedbackController : Controller
{
    private readonly DbG06wncContext _context;

    public FeedbackController(DbG06wncContext context){
        this._context = context;
    }

    public IActionResult Index(){
        var feedbacks = _context.Feedbacks.ToList();
        foreach(var item in feedbacks){
            var account = _context.Accounts.FirstOrDefault(a => a.IAccountId == item.IAccountId);
            if(account != null){
                item.IAccount = account;
            }
        }
        return View(feedbacks);
    }


        
} 

