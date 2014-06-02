using System;
using System.Web.Mvc;
using Orchard;
using Orchard.DisplayManagement;
using Orchard.Themes;


namespace Hazza.ShapeField.Controllers
{
    public class ShaperController : Controller
    {
        private readonly IShapeDisplay shapeDisplay;

        public ShaperController(IShapeFactory shapeFactory, IShapeDisplay shapeDisplay)
        {
            Shape = shapeFactory;
            this.shapeDisplay = shapeDisplay;
        }

        dynamic Shape { get; set; }

        // for some reason this doesn't work, in the "callsite" bit returns the shape as null. works fine in other places...
        public ActionResult TestShape(string shape)
        {
            var test = Shape.Create(shape);
            var html = shapeDisplay.Display(shape);
            return Json(html, JsonRequestBehavior.AllowGet);
        }
    }
}