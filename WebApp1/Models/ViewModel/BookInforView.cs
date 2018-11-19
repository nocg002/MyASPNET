using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace WebApp1.Models {
    public class BookInforView {
    }

    [System.ComponentModel.DataAnnotations.MetadataType(typeof(BookInforMetaData))]
    public partial class BookInfor {
        /// <summary>
        /// MetaData 中繼資料
        /// </summary>
        public partial class BookInforMetaData {
            #region
            [DisplayName("商品編號")]
            public string ProductID { get; set; }

            [DisplayName("出版社編號")]
            public Nullable<System.Guid> PublisherID { get; set; }
            
            [DataType(DataType.Date)] //2018/3/6
            [DisplayName("出版社日期")]
            public DateTime IssueDate { get; set; }

            [DisplayName("ISBN")]
            public string ISBN13 { get; set; }
            [DisplayName("語系")]
            public string LanguageID { get; set; }
            [DisplayName("書籍介紹 ")]
            public string Introduction { get; set; }
            [DisplayName("書籍作者")]
            public string AuthorAbout { get; set; }
            [DisplayName("目錄")]
            public string Catalog { get; set; }
            [DisplayName("序")]
            public string Preface { get; set; }
            #endregion
        }
    }
}