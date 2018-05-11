using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOEC_Inspection
{
    public class TemplateAndFiles
    {
        /// <summary>
        /// 模板ID
        /// </summary>
        public string TemplateID { get; set; }
        /// <summary>
        /// 电报码
        /// </summary>
        public string TelCode { get; set; }
        /// <summary>
        /// 模板对应的安装包路径
        /// </summary>
        public List<string> FilePaths { get; set; }
    }
}
