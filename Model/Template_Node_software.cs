using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOEC_Inspection.Model
{
    public class Template_Node_software
    {
        public string type { get; set; }
        public string srvname { get; set; }
        public string comment { get; set; }
        public string zipname { get; set; }
        public string exename { get; set; }
        public string start { get; set; }
        public bool update { get; set; }
        public List<Template_SubNode_configXML> List_configXML { get; set; }
    }
}
