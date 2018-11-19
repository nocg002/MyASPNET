using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using WebApp1.Models;

//Identity設定檔
namespace WebApp1 {
    //這是Email驗證
    public class EmailService : IIdentityMessageService {
        public Task SendAsync(IdentityMessage message) {
            // 將您的電子郵件服務外掛到這裡以傳送電子郵件。
            /* 11/18
             * AccountController Register() HttpPost
             * 註冊裡有寄送驗證信件的程式被註解
             * 註冊驗證成功後, AspNetUsers資料表 EmailConfirmed=true
                System.Web.Helpers.WebMail.SmtpPort = 0;
                System.Web.Helpers.WebMail.SmtpServer = "smtp位置";
            */
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService {
        public Task SendAsync(IdentityMessage message) {
            // 將您的 SMS 服務外掛到這裡以傳送簡訊。
            return Task.FromResult(0);
        }
    }

    // 設定此應用程式中使用的應用程式使用者管理員。UserManager 在 ASP.NET Identity 中定義且由應用程式中使用。
    public class ApplicationUserManager : UserManager<ApplicationUser> {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store) {
        }


        //11/18 創建帳號
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // 設定使用者名稱的驗證邏輯
            manager.UserValidator = new UserValidator<ApplicationUser>(manager) {
                AllowOnlyAlphanumericUserNames = false, //允許使用者名稱只有數字
                RequireUniqueEmail = true //mail須唯一
            };

            // 設定密碼的驗證邏輯
            manager.PasswordValidator = new PasswordValidator {
                RequiredLength = 6, //密碼最小長度
                RequireNonLetterOrDigit = true, //必須包含特殊字元
                RequireDigit = true, //必須包含數字
                RequireLowercase = true, //必須包含小寫
                RequireUppercase = false, //必須包含大寫 true
            };

            // 設定使用者鎖定詳細資料
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // 註冊雙因素驗證提供者。此應用程式使用手機和電子郵件接收驗證碼以驗證使用者
            // 您可以撰寫專屬提供者，並將它外掛到這裡。
            manager.RegisterTwoFactorProvider("電話代碼", new PhoneNumberTokenProvider<ApplicationUser> {
                MessageFormat = "您的安全碼為 {0}"
            });
            manager.RegisterTwoFactorProvider("電子郵件代碼", new EmailTokenProvider<ApplicationUser> {
                Subject = "安全碼",
                BodyFormat = "您的安全碼為 {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null) {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // 設定在此應用程式中使用的應用程式登入管理員。
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string> {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager) {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user) {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context) {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }



    //11/19 建立角色
    //之後要在 Startup.Auth.cs app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
    public class ApplicationRoleManager : RoleManager<IdentityRole> {
        public ApplicationRoleManager(IRoleStore<IdentityRole,string> roleStore):base(roleStore) { 
        
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context) {
            return new ApplicationRoleManager(new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>()));
        }
    }
}
