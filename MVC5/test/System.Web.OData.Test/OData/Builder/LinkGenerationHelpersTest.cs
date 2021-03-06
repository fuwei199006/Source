﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.OData.Extensions;
using System.Web.OData.Formatter.Serialization;
using System.Web.OData.TestCommon;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;
using Microsoft.TestCommon;
using Moq;

namespace System.Web.OData.Builder
{
    public class LinkGenerationHelpersTest
    {
        private CustomersModelWithInheritance _model = new CustomersModelWithInheritance();

        [Theory]
        [InlineData(false, "http://localhost/Customers(42)")]
        [InlineData(true, "http://localhost/Customers(42)/NS.SpecialCustomer")]
        public void GenerateSelfLink_GeneratesExpectedSelfLink(bool includeCast, string expectedIdLink)
        {
            HttpRequestMessage request = GetODataRequest(_model.Model);
            var serializerContext = new ODataSerializerContext { Model = _model.Model, EntitySet = _model.Customers, Url = request.GetUrlHelper() };
            var entityContext = new EntityInstanceContext(serializerContext, _model.SpecialCustomer.AsReference(), new { ID = 42 });

            string idLink = entityContext.GenerateSelfLink(includeCast);

            Assert.Equal(expectedIdLink, idLink);
        }

        [Theory]
        [InlineData(false, "http://localhost/Customers(42)/Orders")]
        [InlineData(true, "http://localhost/Customers(42)/NS.SpecialCustomer/Orders")]
        public void GenerateNavigationLink_GeneratesExpectedNavigationLink(bool includeCast, string expectedNavigationLink)
        {
            HttpRequestMessage request = GetODataRequest(_model.Model);
            var serializerContext = new ODataSerializerContext { Model = _model.Model, EntitySet = _model.Customers, Url = request.GetUrlHelper() };
            var entityContext = new EntityInstanceContext(serializerContext, _model.SpecialCustomer.AsReference(), new { ID = 42 });
            IEdmNavigationProperty ordersProperty = _model.Customer.NavigationProperties().Single();

            Uri uri = entityContext.GenerateNavigationPropertyLink(ordersProperty, includeCast);

            Assert.Equal(expectedNavigationLink, uri.AbsoluteUri);
        }

        [Fact]
        public void GenerateSelfLink_ThrowsArgumentNull_EntityContext()
        {
            Assert.ThrowsArgumentNull(
                () => LinkGenerationHelpers.GenerateSelfLink(entityContext: null, includeCast: false),
                "entityContext");
        }

        [Fact]
        public void GenerateSelfLink_ThrowsArgument_IfUrlHelperIsNull()
        {
            EntityInstanceContext context = new EntityInstanceContext();

            Assert.ThrowsArgument(
                () => LinkGenerationHelpers.GenerateSelfLink(context, includeCast: false),
                "entityContext",
                "The property 'Url' of EntityInstanceContext cannot be null.");
        }

        [Fact]
        public void GenerateNavigationPropertyLink_ThrowsArgumentNull_EntityContext()
        {
            IEdmNavigationProperty navigationProperty = new Mock<IEdmNavigationProperty>().Object;

            Assert.ThrowsArgumentNull(
                () => LinkGenerationHelpers.GenerateNavigationPropertyLink(entityContext: null, navigationProperty: navigationProperty, includeCast: false),
                "entityContext");
        }

        [Fact]
        public void GenerateNavigationPropertyLink_ThrowsArgument_IfUrlHelperIsNull()
        {
            IEdmNavigationProperty navigationProperty = new Mock<IEdmNavigationProperty>().Object;
            EntityInstanceContext context = new EntityInstanceContext();

            Assert.ThrowsArgument(
                () => LinkGenerationHelpers.GenerateNavigationPropertyLink(context, navigationProperty, includeCast: false),
                "entityContext",
                "The property 'Url' of EntityInstanceContext cannot be null.");
        }

        [Fact]
        public void GenerateActionLink_ThrowsArgumentNull_EntityInstanceContext()
        {
            EntityInstanceContext entityContext = null;
            IEdmActionImport action = new Mock<IEdmActionImport>().Object;

            Assert.ThrowsArgumentNull(() => entityContext.GenerateActionLink(action.Action), "entityContext");
        }

        [Fact]
        public void GenerateActionLink_ThrowsArgumentNull_Action()
        {
            EntityInstanceContext entityContext = new EntityInstanceContext();

            Assert.ThrowsArgumentNull(() => entityContext.GenerateActionLink(action: null), "action");
        }

        [Fact]
        public void GenerateActionLink_ThrowsActionNotBoundToEntity_IfActionHasNoParameters()
        {
            EntityInstanceContext entityContext = new EntityInstanceContext();
            Mock<IEdmAction> action = new Mock<IEdmAction>();
            action.Setup(a => a.Parameters).Returns(Enumerable.Empty<IEdmOperationParameter>());
            action.Setup(a => a.Name).Returns("SomeAction");

            Assert.ThrowsArgument(
                () => entityContext.GenerateActionLink(action.Object),
                "action",
                "The action 'SomeAction' is not bound to an entity. Only actions that are bound to entities can have action links.");
        }

        [Fact]
        public void GenerateActionLink_GeneratesLinkWithoutCast_IfEntitySetTypeMatchesActionEntityType()
        {
            // Arrange
            HttpRequestMessage request = GetODataRequest(_model.Model);
            var serializerContext = new ODataSerializerContext { Model = _model.Model, EntitySet = _model.Customers, Url = request.GetUrlHelper() };
            var entityContext = new EntityInstanceContext(serializerContext, _model.Customer.AsReference(), new { ID = 42 });

            // Act
            Uri link = entityContext.GenerateActionLink(_model.UpgradeCustomer.Action);

            Assert.Equal("http://localhost/Customers(42)/upgrade", link.AbsoluteUri);
        }

        [Fact]
        public void GenerateActionLink_GeneratesLinkWithCast_IfEntitySetTypeDoesnotMatchActionEntityType()
        {
            // Arrange
            HttpRequestMessage request = GetODataRequest(_model.Model);
            var serializerContext = new ODataSerializerContext { Model = _model.Model, EntitySet = _model.Customers, Url = request.GetUrlHelper() };
            var entityContext = new EntityInstanceContext(serializerContext, _model.SpecialCustomer.AsReference(), new { ID = 42 });

            // Act
            Uri link = entityContext.GenerateActionLink(_model.UpgradeSpecialCustomer.Action);

            Assert.Equal("http://localhost/Customers(42)/NS.SpecialCustomer/specialUpgrade", link.AbsoluteUri);
        }

        [Fact]
        public void GenerateActionLink_GeneratesLinkWithDownCast_IfElementTypeDerivesFromBindingParameterType()
        {
            // Arrange
            IEdmEntitySet specialCustomers = new EdmEntitySet(_model.Container, "SpecialCustomers", _model.SpecialCustomer);
            HttpRequestMessage request = GetODataRequest(_model.Model);
            var serializerContext = new ODataSerializerContext { Model = _model.Model, EntitySet = specialCustomers, Url = request.GetUrlHelper() };
            var entityContext = new EntityInstanceContext(serializerContext, _model.SpecialCustomer.AsReference(), new { ID = 42 });

            // Act
            Uri link = entityContext.GenerateActionLink(_model.UpgradeCustomer.Action);

            Assert.Equal("http://localhost/SpecialCustomers(42)/NS.Customer/upgrade", link.AbsoluteUri);
        }

        [Fact]
        public void GenerateFunctionLink_GeneratesLinkWithoutCast_IfEntitySetTypeMatchesActionEntityType()
        {
            // Arrange
            HttpRequestMessage request = GetODataRequest(_model.Model);
            var serializerContext = new ODataSerializerContext { Model = _model.Model, EntitySet = _model.Customers, Url = request.GetUrlHelper() };
            var entityContext = new EntityInstanceContext(serializerContext, _model.Customer.AsReference(), new { ID = 42 });

            // Act
            Uri link = entityContext.GenerateFunctionLink(_model.IsCustomerUpgraded.Function);

            // Assert
            Assert.Equal("http://localhost/Customers(42)/IsUpgraded(entity=@entity)", link.AbsoluteUri);
        }

        [Fact]
        public void GenerateFunctionLink_GeneratesLinkWithCast_IfEntitySetTypeDoesnotMatchActionEntityType()
        {
            // Arrange
            HttpRequestMessage request = GetODataRequest(_model.Model);
            var serializerContext = new ODataSerializerContext { Model = _model.Model, EntitySet = _model.Customers, Url = request.GetUrlHelper() };
            var entityContext = new EntityInstanceContext(serializerContext, _model.SpecialCustomer.AsReference(), new { ID = 42 });

            // Act
            Uri link = entityContext.GenerateFunctionLink(_model.IsSpecialCustomerUpgraded.Function);

            // Assert
            Assert.Equal("http://localhost/Customers(42)/NS.SpecialCustomer/IsSpecialUpgraded(entity=@entity)", link.AbsoluteUri);
        }

        [Fact]
        public void GenerateFunctionLink_GeneratesLinkWithDownCast_IfElementTypeDerivesFromBindingParameterType()
        {
            // Arrange
            IEdmEntitySet specialCustomers = new EdmEntitySet(_model.Container, "SpecialCustomers", _model.SpecialCustomer);
            HttpRequestMessage request = GetODataRequest(_model.Model);
            var serializerContext = new ODataSerializerContext { Model = _model.Model, EntitySet = specialCustomers, Url = request.GetUrlHelper() };
            var entityContext = new EntityInstanceContext(serializerContext, _model.SpecialCustomer.AsReference(), new { ID = 42 });

            // Act
            Uri link = entityContext.GenerateFunctionLink(_model.IsCustomerUpgraded.Function);

            // Assert
            Assert.Equal("http://localhost/SpecialCustomers(42)/NS.Customer/IsUpgraded(entity=@entity)", link.AbsoluteUri);
        }

        private static HttpRequestMessage GetODataRequest(IEdmModel model)
        {
            HttpConfiguration configuration = new HttpConfiguration();
            string routeName = "Route";
            configuration.Routes.MapODataServiceRoute(routeName, null, model);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "http://localhost");
            request.SetConfiguration(configuration);
            request.ODataProperties().RouteName = routeName;
            return request;
        }
    }
}
