using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ModelChange
{
    public static class OperateSqlDb
    {
        //数据库存入数据中
        public static void DataStore()
        {
            DataTable changesInfoTable = Def.changesInfoTable;//传入操作信息数据
            int i = changesInfoTable.Rows.Count;//表头已在数据库
            SqlConnection  conn = new SqlConnection();

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
                catch (Exception e)
                {
                    throw (new Exception("插入数据库出错：" + e.Message));
                }
            }

        }
    }
}
