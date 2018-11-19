using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp1.Models {
    public class PublisherView {
        public PublisherViewModel ViewModel { get; set; }
    }

    /// <summary>ViewModel</summary>
    public class PublisherViewModel {
        
    }

    /// <summary>DTO</summary>
    public class PublisherDTO {
        
    }

    //MetaData 中繼資料
    [System.ComponentModel.DataAnnotations.MetadataType(typeof(PublisherMetaData))]
    public partial class Publisher {
        /// <summary>MetaData 中繼資料</summary>
        public partial class PublisherMetaData {
            #region
            public System.Guid PublisherID { get; set; }

            /// <summary>出版社名稱</summary>
            [DisplayName("出版社名稱")]
            public string PublisherName { get; set; }

            [DisplayName("聯絡人")]
            public string ContactName { get; set; }

            [DisplayName("電話")]
            public string Telephone { get; set; }

            [DisplayName("手機")]
            public string MobilePhone { get; set; }

            [DisplayName("地址")]
            public string Address { get; set; }

            [DisplayName("備註")]
            public string Remark { get; set; }

            //[DisplayName("流水號")]
            //public int Num { get; set; }
            #endregion
        }
    }
}