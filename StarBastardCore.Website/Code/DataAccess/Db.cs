using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarBastardCore.Website.Code.DataAccess
{
    public class Db : IDb
    {
        public dynamic X
        {
            get { return Simple.Data.Database.OpenNamedConnection("DefaultConnection"); }
        }
    }

    public interface IDb
    {
        dynamic X { get; }
    }
}