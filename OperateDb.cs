using System;
using System.Data.OleDb;
using System.Data;

namespace ModelChange
{
    public static class OperateDb//操作数据库 
    {
        //创建数据库连接           
        //public static OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OleDb.4.0;Data Source= C:\Users\hp-pc\Desktop\数据库1.accdb;User Id=admin; Password=''");有问题
        //public static OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0;Data Source= C:\\Users\\hp-pc\\Desktop\\数据库1.accdb;User Id=admin; Password=''");有问题
        //public static OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OleDb.4.0;Data Source= C:\\Users\\hp-pc\\Desktop\\数据库1.accdb;User Id=admin; Password=''");有问题
        //public static OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OleDb.4.0;Data Source= C:\\Users\\hp-pc\\Desktop\\数据库1.accdb;User Id=admin; Password=");有问题
        //public static OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0;Data Source= C:\\Users\\hp-pc\\Desktop\\数据库1.accdb;");有问题
        //public static OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OleDb.4.0;Data Source= C:\\Users\\hp-pc\\Desktop\\数据库1.accdb;");有问题
        //public static OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OleDb.4.0;Data Source= C:\Users\hp-pc\Desktop\数据库1.accdb"); 有问题
        //public static OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\hp-pc\\Desktop\\数据库1.accdb;");格式没有问题，结果有问题
        //public static OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\hp-pc\Desktop\数据库1.accdb;User name =;Password =;"); //调试结果，路径正确，但打不开连接字符串
        //public static OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\hp-pc\\Desktop\\数据库1.accdb");调试结果路径不正确
        //public static OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\hp-pc\Desktop\数据库1.accdb;Persist Security Info=False;Database User name =;Password =;"); //调试结果，路径正确，但打不开连接字符串
        //public static OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\hp-pc\Desktop\数据库1.accdb;");
        //public static OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\hp-pc\\Desktop\\数据库1.accdb;Persist Security Info=True");
        //public static OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\hp-pc\\Desktop\\数据库1.accdb;Persist Security Info=True");有问题
        //public static OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\hp-pc\\Desktop\\数据库1.accdb;Persist Security Info=True");
        //public static OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\hp-pc\Desktop\数据库1.accdb;Persist Security Info=True;User name ='';Password ='';");//包含密码
        public static OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\hp-pc\Desktop\数据库1.accdb;Persist Security Info=True; User name = Admin; Password =;");//包含密码
        //打开数据库连接
        public static void OpenDb()
        {
            conn.Open();
        }

        //关闭数据库连接
        public static void CloseDb()
        {
            conn.Close();
        }

        //数据库存入数据中
        public static void DataStore()
        {            
            DataTable changesInfoTable = Def.changesInfoTable;//传入操作信息数据
            int i = changesInfoTable.Rows.Count ;//表头已在数据库

            //模型没有修改
            if (i < 1)
            {
                System.Environment.Exit(0);//强制退出程序
            }
            
            //模型发生修改          
            //监听机制的操作信息与模型的实时信息一一对应通过ID将二表关联
            //获取监听机制的详细信息和模型的实时信息分别存入数据表
            //获取操作信息存入操作信息表,操作信息如何变成语句来存储
            for (int j = 1; j <= i; j++)
            {                   
               //string str = "insert into ModelChangeInfo(ID,ModifyTime,ChangeType,ObjectId,Name,Category,Documnet) values (1,2016.9.19 21:00 ,Added,722087,Model,电气设备,Model.rvt)";//数据表中信息  //string str = "insert into ModelChangeInfo(ID,ModifyTime,ChangeType,ObjectId,Name,Category,Documnet)values(j,changesInfoTable.Rows[j][0] ,changesInfoTable.Rows[j][1],changesInfoTable.Rows[j][2],changesInfoTable.Rows[j][3],changesInfoTable.Rows[j][4],changesInfoTable.Rows[j][5])";//数据表中信息                                                                                                                                                                                                    
                try
                {                    
                    conn.Open();//此处问题所在
                    string insertstr = "insert into ModelChangeInfo(ID,ModifyTime,ChangeType,ObjectId,Name,Category,Documnet) values (1,2016.9.19 21:00 ,Added,722087,Model,电气设备,Model.rvt)";
                    OleDbCommand comm = new OleDbCommand(insertstr, conn);
                    comm.ExecuteNonQuery();//执行插入命令                                          
                    conn.Close();
                }
                catch(Exception e)
                {
                    throw (new Exception("插入数据库出错：" + e.Message));
                }
            }
            
        }
        //数据库查询数据
        private static void DataQuery()
        {          
            //创建command对象并保存sql查询语句 
            OleDbCommand acommand = new OleDbCommand("select * from ModelChangeInfo", conn);
            OleDbCommand acommand1 = new OleDbCommand("select * from ModelInfo", conn);
            try
            {
                conn.Open();
                //OpenDb();
                // 创建OleDbDataReader对象来连接到表单
                OleDbDataReader areader = acommand.ExecuteReader();
                while(areader.Read())
                {
                    //读取数据库信息实时模型信息
                    Console.WriteLine(areader.GetInt32(0).ToString());
                    //读取实时模型信息

                }
                //CloseDb();
                conn.Close();
            }
            catch(OleDbException e)
            {
                Console.Write("error: {0}", e.Errors.ToString());
            }           
        }
    }
}
