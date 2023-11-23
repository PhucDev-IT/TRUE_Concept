using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TRUE_CONCEPT.Models
{
    public class DbMangager
    {
        private static TRUE_CONCEPTEntities db;
        private static readonly object lockObject = new object();

        private DbMangager()
        {
        }

        public static TRUE_CONCEPTEntities GetInstance()
        {
            if (db == null)
            {
                lock (lockObject)
                {
                    if (db == null)
                    {
                        db = new TRUE_CONCEPTEntities();
                    }
                }
            }
            return db;
        }


    }
}