using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Healing2Peace.Model
{
    public class CommentMasterModel : CommonPropertiesModel
    {
        public int comment_master_id { get; set; }
        public System.Guid guid { get; set; }
        public int blog_master_id { get; set; }
        public string comment_by_user_name { get; set; }
        public string blog_comment { get; set; }
        
    }
}