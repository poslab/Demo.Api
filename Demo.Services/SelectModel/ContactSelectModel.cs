using Demo.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Services.SelectModel
{
    public class ContactSelectModel : BaseSelectModel
    {
        public int UserID { get; set; }
        public override string GetCacheKey()
        {
            //if we have redis
            //return $"Contact_{UserID}";
            throw new NotImplementedException();
        }
    }
}
