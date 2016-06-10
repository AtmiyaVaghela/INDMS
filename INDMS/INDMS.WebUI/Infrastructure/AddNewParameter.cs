using INDMS.WebUI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INDMS.WebUI.Infrastructure
{
    public class AddNewParameter
    {
        internal void Add(string keyName, string keyValue)
        {
            using (INDMSEntities db = new INDMSEntities())
            {
                ParameterMaster obj = new ParameterMaster();
                obj.KeyName = keyName;
                obj.KeyValue = keyValue;
                obj.CreatedBy = HttpContext.Current.Request.Cookies["INDMS"]["UserID"];

                IEnumerable<ParameterMaster> result = from d in db.ParameterMasters
                                                      where d.KeyName == obj.KeyName && d.KeyValue == obj.KeyValue
                                                      select d;

                if (result.Count() < 1)
                {
                    db.ParameterMasters.Add(obj);
                    db.SaveChanges();
                }
            }
        }
    }
}