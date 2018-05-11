using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOEC_Inspection.Model
{
    public class Template_SubNode_configXML
    {
        /// <summary>
        /// 配置文件相对路径
        /// </summary>
        public string filepath { get; set; }
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 属性名称
        /// </summary>
        public string attr { get; set; }
        /// <summary>
        /// 属性值
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 节点值
        /// </summary>
        public string innertext { get; set; }
        /// <summary>
        /// 编码格式
        /// </summary>
        public string encode { get; set; }
        /// <summary>
        /// 注释说明
        /// </summary>
        public string comment { get; set; }
    }
}
