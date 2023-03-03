using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 Roslyn Code Generator 
 */

namespace DotNetMonitor.Patches.System.Windows.Forms
{
    [HarmonyPatch(typeof(MessageBox))]
    class MessageBoxPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("Show", new[] { typeof(string) })]
        static void PrefixShow(string text)
        {
            MainForm.DispatchApiCall(new CallStruct
            {
                Instance = "MessageBox",
                MethodName = "Show",
                Parameters = MethodBase.GetCurrentMethod().GetParameters().WithValues(new CallLookup
                {
                    [nameof(text)] = text
                })
            });
        }

        [HarmonyPrefix]
        [HarmonyPatch("Show", new[] { typeof(string), typeof(string) })]
        static bool PrefixShow(string text,string caption)
        {
            MainForm.DispatchApiCall(new CallStruct
            {
                Instance = "MessageBox",
                MethodName = "Show",
                Parameters = MethodBase.GetCurrentMethod().GetParameters().WithValues(new CallLookup
                {
                    [nameof(text)] = text,
                    [nameof(caption)] = caption,
                })
            });
            // prefix return false that means hook it
            return false;
        }
    }
}
