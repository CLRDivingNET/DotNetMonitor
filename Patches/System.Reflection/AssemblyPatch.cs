using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotNetMonitor.Patches.System.Reflection
{
    [HarmonyPatch(typeof(Assembly))]
    class AssemblyPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("Load", new[] { typeof(byte[]) })]
        static void PrefixShow(Byte[] rawAssembly)
        {
            MainForm.DispatchApiCall(new CallStruct
            {
                Instance = "Assembly",
                MethodName = "Load",
                Parameters = MethodBase.GetCurrentMethod().GetParameters().WithValues(new CallLookup
                {
                    [nameof(rawAssembly)] = rawAssembly
                })
            });
        }
    }
}
