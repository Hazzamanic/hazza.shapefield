using System.Linq;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.DisplayManagement;
using Orchard.Localization;

namespace Hazza.ShapeField.Drivers
{
    public class ShapeFieldDriver : ContentFieldDriver<Fields.ShapeField>
    {
        private const string TemplateName = "Fields/Shape.Edit";

        public ShapeFieldDriver()
        {
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        private static string GetPrefix(ContentField field, ContentPart part) {
            return part.PartDefinition.Name + "." + field.Name;
        }

        private static string GetDifferentiator(Fields.ShapeField field, ContentPart part)
        {
            return field.Name;
        }

        protected override DriverResult Display(ContentPart part, Fields.ShapeField field, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Fields_Shape", GetDifferentiator(field, part), () => {
                // add content item to new shape
                var shape = shapeHelper.Create(field.Value, Arguments.From(Enumerable.Repeat(part.ContentItem, 1), Enumerable.Repeat("ContentItem", 1)));
                return shapeHelper.Fields_Shape(FieldShape: shape);
            });
        }

        protected override DriverResult Editor(ContentPart part, Fields.ShapeField field, dynamic shapeHelper)
        {
            return ContentShape("Fields_Shape_Edit", GetDifferentiator(field, part),
                () => shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: field, Prefix: GetPrefix(field, part)));
        }

        protected override DriverResult Editor(ContentPart part, Fields.ShapeField field, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(field, GetPrefix(field, part), null, null);
            return Editor(part, field, shapeHelper);
        }

        protected override void Importing(ContentPart part, Fields.ShapeField field, ImportContentContext context)
        {
            context.ImportAttribute(field.FieldDefinition.Name + "." + field.Name, "Value", v => field.Value = v);
        }

        protected override void Exporting(ContentPart part, Fields.ShapeField field, ExportContentContext context)
        {
            context.Element(field.FieldDefinition.Name + "." + field.Name).SetAttributeValue("Value", field.Value);
        }

        protected override void Describe(DescribeMembersContext context) {
            context
                .Member(null, typeof(string), T("Value"), T("The value of the field."))
                .Enumerate<Fields.ShapeField>(() => field => new[] { field.Value });
        }
    }
}
