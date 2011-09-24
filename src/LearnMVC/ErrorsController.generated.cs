// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace LearnMVC.Controllers {
    public partial class ErrorsController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ErrorsController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected ErrorsController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result) {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult NotFound() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.NotFound);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ErrorsController Actions { get { return MVC.Errors; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Errors";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly string NotFound = "NotFound";
            public readonly string Forbidden = "Forbidden";
            public readonly string General = "General";
        }


        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
            public readonly string Forbidden = "~/Views/Errors/Forbidden.cshtml";
            public readonly string General = "~/Views/Errors/General.cshtml";
            public readonly string NotFound = "~/Views/Errors/NotFound.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_ErrorsController: LearnMVC.Controllers.ErrorsController {
        public T4MVC_ErrorsController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult NotFound(string aspxerrorpath) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.NotFound);
            callInfo.RouteValueDictionary.Add("aspxerrorpath", aspxerrorpath);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Forbidden() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Forbidden);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult General() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.General);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591