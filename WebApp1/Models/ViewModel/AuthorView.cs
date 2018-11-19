using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace WebApp1.Models {
    public class AuthorView {
    }

    [System.ComponentModel.DataAnnotations.MetadataType(typeof(AuthorMetaData))]
    public partial class Author {
        /// <summary>
        /// MetaData 中繼資料
        /// </summary>
        public partial class AuthorMetaData {
            #region
            public System.Guid AuthorID { get; set; }

            [DisplayName("作家名稱")]
            public string AuthorName { get; set; }

            [DisplayName("性別")]
            public string Gender { get; set; }

            //[DisplayName("流水號")]
            //public int Num { get; set; }
            #endregion
        }
    }
}