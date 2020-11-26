using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.DAL.ContextClasses;

namespace Project.BLL.DesignPatterns.SingletonPattern
{
    public class DBTool
    {
        DBTool()
        {

        }

        static MyContext _dbIntance;
        public static MyContext DbInstance { get {
                if (_dbIntance==null)
                {
                    _dbIntance = new MyContext();
                }
                return _dbIntance;
            
            } }
    }
}
