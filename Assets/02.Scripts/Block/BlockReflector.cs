using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;

public static class BlockReflector
{
   
    public static IEnumerable<Type> GetAllSealedBlockTypeContainingBlockTitleAttribute()
    {
        return Assembly
   .GetAssembly(typeof(Block))
   .GetTypes()
   .Where(t => t.IsClass && t.IsSealed && t.IsSubclassOf(typeof(Block)) )
   .Select(t => t);

  

    }


}
