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
    
    public partial class ProductImage
    {
        public string ProductID { get; set; }
        public System.Guid ImageID { get; set; }
        public string Folder { get; set; }
        public string FileName { get; set; }
        public string OrigFileName { get; set; }
        public byte Sort { get; set; }
        public bool Master { get; set; }
        public bool Visible { get; set; }
        public bool Deleted { get; set; }
        public bool Publish { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
