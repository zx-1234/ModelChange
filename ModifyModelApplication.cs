using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using ModelChange.Commands;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace ModelChange
{
    [TransactionAttribute(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class ModifyModelApplication:IExternalApplication
    {
        private static ControlledApplication m_CtrlApp;
        private static DataTable m_ChangesInfoTable;
        private static ChangesInformationForm m_InfoForm;        
                
        public DataTable ChangesInfoTable
        {
            get { return m_ChangesInfoTable; }
            set { m_ChangesInfoTable = value; }
        }
        public static ChangesInformationForm InfoForm
        {
            get { return m_InfoForm; }
            set { m_InfoForm = value; }
        }
       public Result OnStartup(UIControlledApplication app)
        {
            RibbonPanelUI PA_UI = new RibbonPanelUI();
            PA_UI.SetRibbonPanelUI(app);          
            m_CtrlApp = app.ControlledApplication;
            m_ChangesInfoTable = CreateChangeInfoTable();
            m_InfoForm = new ChangesInformationForm(ChangesInfoTable);
            m_CtrlApp.DocumentChanged += new EventHandler<DocumentChangedEventArgs>(CtrlApp_DocumentChanged);
            m_InfoForm.Show();
            return Result.Succeeded;
        }
                               
        public Result OnShutdown(UIControlledApplication application)
        {
            m_CtrlApp.DocumentChanged -= CtrlApp_DocumentChanged;
            m_InfoForm = null;
            m_ChangesInfoTable = null;
            return Result.Succeeded;
        }
     
        void CtrlApp_DocumentChanged(object sender, Autodesk.Revit.DB.Events.DocumentChangedEventArgs e)
        {            
            Document doc = e.GetDocument();
            
            ICollection<ElementId> addedElem = e.GetAddedElementIds();
            foreach (ElementId id in addedElem)
            {
                AddChangeInfoRow(id, doc, "Added");
            }

            ICollection<ElementId> deletedElem = e.GetDeletedElementIds();
            foreach (ElementId id in deletedElem)
            {
                AddChangeInfoRow(id, doc, "Deleted");
            }

            ICollection<ElementId> modifiedElem = e.GetModifiedElementIds();
            foreach (ElementId id in modifiedElem)
            {
                AddChangeInfoRow(id, doc, "Modified");
            }
        }

        private void AddChangeInfoRow(ElementId id, Document doc, string changeType)//填充数据表信息
        {
            // retrieve the changed element
            Element elem = doc.GetElement(id);
            DataRow newRow = m_ChangesInfoTable.NewRow();
            DateTime dt = DateTime.Now;
            if (elem == null)
            {
                newRow["ModifyTime"] = dt.ToString();
                newRow["ChangeType"] = changeType;
                newRow["Id"] = id.IntegerValue.ToString();
                newRow["Name"] = "";
                newRow["Category"] = "";
                newRow["Document"] = "";              
            }
            else
            {
                newRow["ModifyTime"] = dt.ToString();
                newRow["ChangeType"] = changeType;
                newRow["Id"] = id.IntegerValue.ToString();
                newRow["Name"] = elem.Name;
                newRow["Category"] = elem.Category.Name;
                newRow["Document"] = doc.Title;
            }
            m_ChangesInfoTable.Rows.Add(newRow);
            Def.changesInfoTable = m_ChangesInfoTable;
            OperateDb.DataStore();//就是static出了这么多错
        }
        //OperateDb odb = new OperateDb();

        private DataTable CreateChangeInfoTable()
        {            
            DataTable changesInfoTable = new DataTable("ChangesInfoTable");
            DataColumn timeColumn = new DataColumn("ModifyTime", typeof(System.String));
            timeColumn.Caption = "ModifyTime";
            changesInfoTable.Columns.Add(timeColumn);

            DataColumn styleColumn = new DataColumn("ChangeType", typeof(System.String));
            styleColumn.Caption = "ChangeType";
            changesInfoTable.Columns.Add(styleColumn);
            
            DataColumn idColum = new DataColumn("Id", typeof(System.String));
            idColum.Caption = "Id";
            changesInfoTable.Columns.Add(idColum);
            
            DataColumn nameColum = new DataColumn("Name", typeof(System.String));
            nameColum.Caption = "Name";
            changesInfoTable.Columns.Add(nameColum);
            
            DataColumn categoryColum = new DataColumn("Category", typeof(System.String));
            categoryColum.Caption = "Category";
            changesInfoTable.Columns.Add(categoryColum);
            
            DataColumn docColum = new DataColumn("Document", typeof(System.String));
            docColum.Caption = "Document";
            changesInfoTable.Columns.Add(docColum);            
            return changesInfoTable;
        }      
    }
}

