using System;
using System.Collections.Generic;
using System.Text;
using Autodesk.Revit;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Windows.Forms;
using Revit.SDK.Samples.ParameterUtils.CS;
using ModelChange.Commands;
using Autodesk.Revit.Attributes;

namespace ModelChange.Commands
{   
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class ModifyParameter : IExternalCommand
    {       
        public Result Execute(ExternalCommandData commandData, ref string message,ElementSet elements)
        {   
            Result retRes = Result.Failed;
            UIApplication app = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = app.ActiveUIDocument.Document;            
            ElementSet seletion = new ElementSet();
            Transaction tran = new Transaction(doc);           
            foreach (ElementId elementId in app.ActiveUIDocument.Selection.GetElementIds())
            {
               seletion.Insert(app.ActiveUIDocument.Document.GetElement(elementId));
            }                        
            //仅选择一个实体
            if (seletion.Size == 1)
            {              
                ElementSetIterator it = seletion.ForwardIterator();
                it.MoveNext();
                Element element = it.Current as Element;                
                List<string> parameterItems = new List<string>();//参数列表
                ParameterSet parameters = element.Parameters;//获取元素的所有参数
                Def.parameters = parameters;
                foreach (Parameter param in parameters)
                {
                    if (param == null) continue;                   
                    StringBuilder sb = new StringBuilder();                    
                    sb.AppendFormat("{0}\t", param.Definition.Name);                                       
                    switch (param.StorageType)
                    {                        
                        case StorageType.Double:                           
                            sb.AppendFormat("double\t{0}", param.AsDouble());
                            break;
                        case StorageType.ElementId:                           
                            sb.Append("Element\t");                            
                            ElementId elemId = new ElementId(param.AsElementId().IntegerValue);
                            Element elem = app.ActiveUIDocument.Document.GetElement(elemId);                            
                            sb.Append(elem != null ? elem.Name : "Not set");
                            break;
                        case StorageType.Integer:                          
                            sb.AppendFormat("int\t{0}", param.AsInteger());
                            break;
                        case StorageType.String:                            
                            sb.AppendFormat("string\t{0}", param.AsString());
                            break;
                        case StorageType.None:                            
                            sb.AppendFormat("none\t{0}", param.AsValueString());
                            break;
                        default:
                            break;
                    }                   
                    parameterItems.Add(sb.ToString());
                }
                PropertiesForm propertiesForm = new PropertiesForm(parameterItems.ToArray());
                propertiesForm.StartPosition = FormStartPosition.CenterParent;               
                propertiesForm.ShowDialog();              
                ListView propertyListView = Def.propertyListView;
                int a = Def.a;                              
                    foreach (Parameter param in parameters)
                    {
                    
                         if (param.Definition.Name == propertyListView.Items[a].SubItems[0].Text)
                         {
                            switch (param.StorageType)
                            {
                                case StorageType.Double:
                                MessageBox.Show("The parameter can't be modified!");                                
                                break;
                                case StorageType.ElementId:                               
                                  try
                                  {
                                      tran.Start("test");
                                      {
                                            ElementId elemId = new ElementId(param.AsElementId().IntegerValue);                                           
                                            Element elem = app.ActiveUIDocument.Document.GetElement(elemId);                                           
                                            if (elem != null)
                                            {
                                                elem.Name = propertyListView.Items[a].SubItems[2].Text.ToString();
                                            }
                                             else
                                            {
                                                MessageBox.Show("The parameter doesn't need to be modified!");                                              
                                            }                                                                                                                                
                                            doc.Regenerate();                                           
                                        }
                                        tran.Commit();
                                    }
                                   catch (Exception)
                                   {
                                        tran.RollBack();
                                   }                                                                                             
                                   break;
                                case StorageType.Integer:                                                                                                   
                                    try
                                    {
                                        tran.Start("test");
                                        {                                                                                      
                                            param.Set(int.Parse(propertyListView.Items[a].SubItems[2].Text));
                                        }
                                        tran.Commit();                                        
                                    }                                    
                                    catch (Exception)
                                    {
                                        tran.RollBack();
                                    }                                                                    
                                    break;                                                              
                                case StorageType.String:                                                                                         
                                    try
                                    {
                                        tran.Start("test");
                                        {
                                            param.Set(propertyListView.Items[a].SubItems[2].Text.ToString());                                           
                                        }
                                        tran.Commit();
                                    }
                                    catch (Exception)
                                    {
                                        tran.RollBack();
                                    }                                                        
                                   break;
                                case StorageType.None:                                
                                  break;
                                default:
                                  break;
                        }                            
                    }
                }                              
                retRes = Result.Succeeded;
            }
            else
            {
                message = "Please select only one element";
            }
            return retRes;
        }
    }
}

