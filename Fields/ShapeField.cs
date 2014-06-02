using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Orchard.ContentManagement.FieldStorage;

namespace Hazza.ShapeField.Fields
{
    public class ShapeField : ContentField
    {
        public string Value {
            get { return Storage.Get<string>(); }
            set { Storage.Set(value ?? String.Empty); }
        }
    }
}