using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBlog.Web.UI.Controllers
{
    public class ResultModel<T>
        where T : class
    {
        public List<string> Errors { get; set; }
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public T CurrentObject { get; set; }
    }
}