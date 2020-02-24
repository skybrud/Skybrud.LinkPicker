using Newtonsoft.Json.Linq;
using Skybrud.LinkPicker.Constants;
using Skybrud.LinkPicker.PropertyEditors;
using Skybrud.Umbraco.Elements.PropertyEditors.Elements;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Migrations;
using Umbraco.Core.Models;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Services;

namespace Skybrud.LinkPicker.Migrations {

    public class LinkPickerMigration : MigrationBase {

        private readonly ILogger _logger;
        private readonly IDataTypeService _dataTypeService;
        private readonly IContentTypeService _contentTypeService;

        public LinkPickerMigration(ILogger logger, IDataTypeService dataTypeService, IContentTypeService contentTypeService, IMigrationContext context) : base(context) {
            _logger = logger;
            _dataTypeService = dataTypeService;
            _contentTypeService = contentTypeService;
        }

        public override void Migrate() {

            // Create containers/folders for data types and element types
            EntityContainer dataTypeContainer = CreateDataTypeContainer();
            EntityContainer elementTypeContainer = CreateElementTypeContainer();

            // WTF?
            if (dataTypeContainer == null) return;
            if (elementTypeContainer == null) return;

            // Create the data types
            IDataType linkDataType = CreateLinkDataType(dataTypeContainer);
            CreateLinkListDataType(dataTypeContainer);

            // Create the element type
            CreateLinkElementType(elementTypeContainer, linkDataType);

        }

        private EntityContainer CreateDataTypeContainer()  {

            EntityContainer container = _dataTypeService.GetContainer(LinkPickerConstants.DataTypes.Container);
            if (container != null) return container;

            container = new EntityContainer(global::Umbraco.Core.Constants.ObjectTypes.DataType) {
                Key = LinkPickerConstants.DataTypes.Container,
                Name = "Skybrud.Umbraco.Elements",
                ParentId = -1
            };

            Attempt<OperationResult> attempt = _dataTypeService.SaveContainer(container);
            return attempt.Success == false ? null : container;

        }

        private IDataType CreateLinkDataType(EntityContainer container) {

            // Get the data type by it's GUID (return if found)
            IDataType link = _dataTypeService.GetDataType(LinkPickerConstants.DataTypes.Link);
            if (link != null) return link;

            // Get a reference to the data editor
            IDataEditor editor = new LinkPropertyEditor(_logger);
            
            // Initialize a new data type
            link = new DataType(editor, container.Id) {
                Key = LinkPickerConstants.DataTypes.Link,
                Name = "Link"
            };

            // Save the data type
            _dataTypeService.Save(link);

            return link;

        }

        private void CreateLinkListDataType(EntityContainer container) {

            // Get the data type by it's GUID (return if found)
            IDataType dataType = _dataTypeService.GetDataType(LinkPickerConstants.DataTypes.LinkList);
            if (dataType != null) return;

            // Get a reference to the data editor
            IDataEditor editor = new ElementsPropertyEditor(_logger);

            // Initialize a new data type
            dataType = new DataType(editor, container.Id) {
                Key = LinkPickerConstants.DataTypes.LinkList,
                Name = "Link List",
                Configuration = new ElementsConfiguration {
                    ConfirmDeletes = true,
                    HideLabel = false,
                    MaxItems = 10,
                    MinItems = 0,
                    SinglePicker = false,
                    View = "/App_Plugins/Skybrud.LinkPicker/Views/Partials/Links.html",
                    AllowedTypes = new JArray(
                        new JObject {
                            {"key", LinkPickerConstants.ElementTypes.Link}
                        }
                    )
                }
            };

            // Save the data type
            _dataTypeService.Save(dataType);

        }

        private EntityContainer CreateElementTypeContainer()  {

            EntityContainer container = _contentTypeService.GetContainer(LinkPickerConstants.ElementTypes.Container);
            if (container != null) return container;

            container = new EntityContainer(global::Umbraco.Core.Constants.ObjectTypes.DocumentType) {
                Key = LinkPickerConstants.ElementTypes.Container,
                Name = "Skybrud.Umbraco.Elements",
                ParentId = -1
            };

            Attempt<OperationResult> attempt = _contentTypeService.SaveContainer(container);
            return attempt.Success == false ? null : container;

        }

        private void CreateLinkElementType(EntityContainer container, IDataType dataType) {

            IContentType type = _contentTypeService.Get(LinkPickerConstants.ElementTypes.Link);
            if (type != null) return;

            // Initialize a new content type
            type = new ContentType(container.Id)  {
                Key = LinkPickerConstants.ElementTypes.Link,
                Alias = "skyLink",
                Name = "Link",
                Icon = "icon-link",
                Description = "Element type representing a single link.",
                IsElement = true
            };

            // Initialize a new property type
            PropertyType linkPropertyType = new PropertyType(dataType, "link")  {
                Name = "Link",
                Description = "Select a destination for the link.",
                Mandatory = true
            };

            // Append a new "Link" property group
            type.AddPropertyGroup("Link");
            
            // Append the property to the "Link" property group
            type.AddPropertyType(linkPropertyType, "Link");

            // Save the element type
            _contentTypeService.Save(type);

        }

    }

}