//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApp1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BookAuthor
    {
        public string ProductID { get; set; }
        public System.Guid AuthorID { get; set; }
        public string AuthorType { get; set; }
    
        public virtual Author Author { get; set; }
        public virtual BookInfor BookInfor { get; set; }
    }
}
