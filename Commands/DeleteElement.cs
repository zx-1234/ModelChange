using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using System.Collections;
using Autodesk.Revit.Attributes;

namespace ModelChange.Commands
{
    [Transaction(TransactionMode.Automatic)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class DeleteElement : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            ElementSet elementSet = new ElementSet();
            bool error = true;
            //获取选择的元素
            foreach (ElementId elementid in uiapp.ActiveUIDocument.Selection.GetElementIds())
            {
                elementSet.Insert(uiapp.ActiveUIDocument.Document.GetElement(elementid));
            }
            if(elementSet.Size < 1)
            {
                TaskDialog.Show("Recommand", "Please select a object before delete.");
                return Result.Cancelled;
            }                                    
            //执行删除
            try
            {
                error = true;
                IEnumerator elem = elementSet.GetEnumerator();//枚举集合元素
                bool judge = elem.MoveNext();               
                if(judge)
                {                                        
                    Element e = elem.Current as Element;
                    elementSet.Erase(e);
                    uiapp.ActiveUIDocument.Document.Delete(e.Id);                                       
                }                
                 error = false; //完成删除任务     
            }
            catch
            {
                foreach (Element c in elementSet)
                {
                  elements.Insert(c);
                }
                message = "object(s) can't be deleted.";
                return Result.Failed;
            }
            finally
            {
               if (error)
               {
                TaskDialog.Show("Error", "Delete failed.");
               }
            }
            return Result.Succeeded;
        }
    }
}
