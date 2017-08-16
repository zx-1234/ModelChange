using Autodesk.Revit.UI;
using System;
using System.IO;
using Autodesk.Revit.DB;
using System.Windows.Media.Imaging;

namespace ModelChange
{
    public class RibbonPanelUI
    {
        static string ExecutingAssemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        private void AddTab(UIControlledApplication app, string tabName)
        {
            app.CreateRibbonTab(tabName);
        }
        private RibbonPanel AddPanel(UIControlledApplication app, string panelName, string tabName)
        {
            if (tabName == null)
            {
                return app.CreateRibbonPanel(panelName);
            }
            else
            {
                return app.CreateRibbonPanel(tabName, panelName);
            }
        }

        private PushButton AddPushButton(RibbonPanel panel, string buttonID, string buttonName, string cmdPath, string className)
        {
            PushButtonData data = new PushButtonData(buttonID, buttonName, cmdPath, className);
            PushButton button = panel.AddItem(data) as PushButton;
            return button;
        }

        private void AddPushButtonMsg(PushButton button, string tip, string description)
        {
            button.ToolTip = tip;
            button.LongDescription = description;
        }

        private BitmapImage GetBitmapImage(string imageName)
        {
            return new BitmapImage(new Uri(imageName));
        }

        private string GetTopDir(string CurDir)
        {
            DirectoryInfo TopDir = Directory.GetParent(CurDir);
            string TopName = TopDir.FullName;
            return TopName;
        }

        public void SetRibbonPanelUI(UIControlledApplication app)
        {
            string m_paTab = "ModifyModel";
            AddTab(app, m_paTab);
            RibbonPanel Panel = app.CreateRibbonPanel(m_paTab, "ModifyModel");

            //添加实体            
            PushButtonData APBD1 = new PushButtonData("Add Instance", "Add Instance", ExecutingAssemblyPath, "ModelChange.Commands.AddInstance");//ModifyModel.AddObject
            APBD1.ToolTip = "This command is to add some instances.";
            string RadiationIconPath = GetTopDir(ExecutingAssemblyPath) + @"\Images\Add.png";
            APBD1.LargeImage = GetBitmapImage(RadiationIconPath);
            PushButton pb1 = Panel.AddItem(APBD1) as PushButton;//强制转换            

            //删除实体         
            PushButtonData DPBD = new PushButtonData("Pick for delete", "Pick for delete", ExecutingAssemblyPath, "ModelChange.Commands.DeleteElement");//ModifyModel.DeleteObject
            DPBD.ToolTip = "This command is to pick element and delete.";
            string DpBDIconPath = GetTopDir(ExecutingAssemblyPath) + @"\Images\Delete.png";
            DPBD.LargeImage = GetBitmapImage(DpBDIconPath);
            Panel.AddItem(DPBD);
            
            //修正实体参数           
            PushButtonData MPBD = new PushButtonData("ModifyParameter", "ModifyParameter", ExecutingAssemblyPath, "ModelChange.Commands.ModifyParameter");//ModifyModel.ModifyParameter
            MPBD.ToolTip = "Set and modify the relevant instance parameters to meet the requirement.";
            string PVpsPBDIconPath = GetTopDir(ExecutingAssemblyPath) + @"\Images\ModifyParameter.bmp";
            MPBD.LargeImage = GetBitmapImage(PVpsPBDIconPath);
            Panel.AddItem(MPBD);                                   
        }
    }
    public class CommandNotAvailable : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(Autodesk.Revit.UI.UIApplication applicationData, CategorySet selectedCategories)
        {
            return false;
        }
    }
}
