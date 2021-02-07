using System;

namespace Demo.SharedKernel
{
    public abstract class BaseSelectModel
    {
        public abstract string GetCacheKey();
    }
}
