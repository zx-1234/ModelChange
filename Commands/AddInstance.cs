using Autodesk.Revit.UI;
using System;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;

namespace ModelChange.Commands
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class AddInstance : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;
            Transaction trans = new Transaction(doc);
            Selection sel = uiapp.ActiveUIDocument.Selection;          
            bool import;            
            TaskDialog.Show("Recommend", "Please choose a family file.");          
            using (Selections form = new Selections());
            string str = Def.str;                       
            trans.Start("load a family file.");
            {
                import = doc.LoadFamily(@"" + str + "");//导入族
                doc.Regenerate();//更新变化到模型中@zx
            }
            trans.Commit();                       
            //编程获取导入的族的OfCategory的BuiltInCategory
            //显示的实体可以很容易获得BuiltIn名称，而没有生成如何显示名称
            //随便的一个导入族,找到这个族
            //如何能然导入的族通过过滤器顺利的获取，并进行实体生成展示
            //目前的资源显示只能够在族实体生成之后进行获取，刚导入不知道能否自己获取。

           /*
            Duct duct = doc.GetElement(sel.PickObject(ObjectType.Element, "选择")) as Duct;
            foreach (Parameter p in duct.Parameters)
            {
                if (p.Definition.Name == "高度")
                {
                    BuiltInParameter biParam = (BuiltInParameter)p.Id.IntegerValue;
                    //TaskDialog.Show("builtIn", biParam.ToString());
                }
            }*/

            //IList<Family> collector1 = new IList<Family>();
            //new FilteredElementCollector(doc).OfClass(typeof(Family)).GetElementIterator();//可以获取所有的族文件,仅指视图中可见的族文件，而没有生成的文件还需
            // IList<Family> collector1 =  F(doc).OfClass(typeof(Family)) ;
            /*
            IList<FamilyInstance> fiset = new List<FamilyInstance>();
            IList<Family> collector1 = new FilteredElementCollector(doc).OfClass(typeof(Family)).GetElementIterator() as IList<Family>;
            Category category;
            foreach (Family family in collector1)
            {
                if (family.Name == Def.name)
                { 
                    category = family.Category;
                }
                else
                { continue; }
            }*/
            //new FilteredElementCollector(doc).OfClass(typeof(Family));
            //IList<Family> collector1 = FilteredElementCollector(doc).OfClass(typeof(Family)).GetElementIterator() as IList<Family>;
            
            //实体位置选择
            TaskDialog.Show("Recommend", "Please choose a location to put the family.");            
            XYZ point = sel.PickPoint("Please pick a point to help create the instance.");
            UV uv = new UV(point.X,point.Y);
            UV uv1 = new UV(0, 0);           
           
            TaskDialog.Show("Recommend", "Please choose a plane to deploy the family instance.");//请选择实体所在位置所处的平面。
            Reference reference = sel.PickObject(ObjectType.Face, "Please select the face which includes the target position.");//内容指向保存在reference
            Element element = doc.GetElement(reference);
            
            GeometryObject go = element.GetGeometryObjectFromReference(reference);
            Face face0 = (go as Face);
            BoundingBoxUV bb0 = face0.GetBoundingBox();
            UV uv0 = new UV((bb0.Max.U + bb0.Min.U) / 2.0, (bb0.Max.V + bb0.Min.V) / 2.0);
            Face face = null;
            Options op = new Options();
            op.ComputeReferences = true;
            GeometryElement ge = element.get_Geometry(op);           
            foreach (GeometryObject geo in ge)
            {
                if (geo is Solid)
                {
                    Solid solid = geo as Solid;
                    if (solid.Volume >= 0)//体积>0,体积可以<0;
                    {
                        foreach (Face face2 in solid.Faces)
                        {
                            BoundingBoxUV bb2 = face2.GetBoundingBox();
                            UV uv2 = new UV((bb2.Max.U + bb2.Min.U) / 2.0, (bb2.Max.V + bb2.Min.V) / 2.0);
                            if (face2.ComputeNormal(uv2).AngleTo(face0.ComputeNormal(uv0)) < 0.0001)//判断夹角范围
                            {
                                face = face2;
                            }
                        }
                    }
                }
            }  
                                
            XYZ xyz1 = face.Evaluate(uv1);
            XYZ xyz = new XYZ(point.X, point.Y, xyz1.Z);           
            FamilySymbol fs1;
            // FamilyInstance fi1 = null;
            Transaction tr = new Transaction(doc, "choose the familysymbol");           
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            //collector.OfClass(typeof(FamilySymbol));
            //collector.OfType(fi1);//(Type FamilyInstance);
            
            //collector.OfCategory(BuiltInCategory.INVALID);           
            //获取内置函数，还要会进行表示，这样才可以。
            collector.OfCategory(BuiltInCategory.OST_ElectricalEquipment);
            //collector.OfCategory(BuiltInCategory.);
            foreach (Element elem in collector)//获取导入fs
            {
                if (elem is FamilySymbol)
                {
                    fs1 = elem as FamilySymbol;
                    if (fs1.Name == Def.name)
                    {
                        FamilySymbol fs = fs1;
                        try
                        {
                            tr.Start("deploy the instance.");
                            {
                                FamilyInstance fi = doc.Create.NewFamilyInstance(face, xyz, new XYZ(1, 0, 0), fs);//此项为正确项
                                //FamilyInstance fi = doc.Create.NewFamilyInstance(face, xyz4, new XYZ(1, 0, 0), fs);
                            }
                            tr.Commit();
                        }
                        catch (Exception)
                        {
                            trans.RollBack();
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    continue;
                }
            }
            return Result.Succeeded;
        }
    }
}
