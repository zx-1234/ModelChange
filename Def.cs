
using Autodesk.Revit.DB;
using Revit.SDK.Samples.ParameterUtils.CS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModelChange
{
    class Def
    {
        public static String str;
        public static Document doc;
        public static string name;
        public static int a;
        public static ListView propertyListView;
        public static Commands.Form1 frm;
        public static PropertiesForm propertiesForm;
        public static ParameterSet parameters;
        public static Element element;
        public static Element element1;
        public static DataTable changesInfoTable;
    }
}
