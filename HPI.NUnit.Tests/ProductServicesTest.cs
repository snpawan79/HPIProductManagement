#region using namespaces.
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HPI.BusinessEntities;
using HPI.DataAccessLayer;
using HPI.DataAccessLayer.GenericRepository;
using HPI.DataAccessLayer.UnitOfWork;
using Moq;
using NUnit.Framework;
using HPI.NUnit.Tests.TestsHelper;
using HPI.BusinessServices;
using HPI.DataAccessLayer.DataModels;

#endregion

namespace HPI.NUnit.Tests
{
    /// <summary>
    /// Product Service Test
    /// </summary>
    public class ProductServicesTest
    {
        #region Variables

        private IProductServices _productService;
        private IUnitOfWork _unitOfWork;
        private List<Product> _products;
        private GenericRepository<Product> _productRepository;
        private DalDbContext _dbEntities;
        #endregion

        #region Test fixture setup

        /// <summary>
        /// Initial setup for tests
        /// </summary>
        [OneTimeSetUp]
        public void Setup()
        {
            _products = SetUpProducts();
        }

        #endregion

        #region Setup

        /// <summary>
        /// Re-initializes test.
        /// </summary>
        [SetUp]
        public void ReInitializeTest()
        {
            _dbEntities = new Mock<DalDbContext>().Object;
            _productRepository = SetUpProductRepository();
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet(s => s.ProductRepository).Returns(_productRepository);
            _unitOfWork = unitOfWork.Object;
            _productService = new ProductServices(_unitOfWork);
        }

        #endregion

        #region Private member methods

        /// <summary>
        /// Setup dummy repository
        /// </summary>
        /// <returns></returns>
        private GenericRepository<Product> SetUpProductRepository()
        {
            // Initialise repository
            var mockRepo = new Mock<GenericRepository<Product>>(MockBehavior.Default, _dbEntities);

            // Setup mocking behavior
            mockRepo.Setup(p => p.GetAll()).Returns(_products);

            mockRepo.Setup(p => p.GetByID(It.IsAny<string>()))
            .Returns(new Func<string, Product>(
            pCode => _products.Find(p => p.ProductCode.Equals(pCode))));

            mockRepo.Setup(p => p.Insert((It.IsAny<Product>())))
            .Callback(new Action<Product>(newProduct =>
            {
                _products.Add(newProduct);
            }));
                   


            // Return mock implementation object
            return mockRepo.Object;
        }

        /// <summary>
        /// Setup dummy products data
        /// </summary>
        /// <returns></returns>
        private static List<Product> SetUpProducts()
        {
            
            var products = DataInitializer.GetAllProducts();
            
            return products;

        }

        #endregion

        #region Unit Tests

        /// <summary>
        /// Service should return all the products
        /// </summary>
        [Test]
        public void Given_a_product_repository_When_contains_data_Then_get_all_products_from_repository()
        {
            var products = _productService.GetAllProducts();
            if (products != null)
            {
                var productList =
                products.Select(
                productEntity =>
                new Product { ProductCode = productEntity.ProductCode, Price = productEntity.Price }).
                ToList();
                var comparer = new ProductComparer();
                CollectionAssert.AreEqual(
                productList.OrderBy(product => product, comparer),
                _products.OrderBy(product => product, comparer), comparer);
            }
        }

        /// <summary>
        /// Service should return null
        /// </summary>
        [Test]
        public void Given_a_product_repository_When_contains_no_data_Then_do_not_retrieve_any_product_from_repository()
        {
            _products.Clear();
            var products = _productService.GetAllProducts();
            Assert.Null(products);
            SetUpProducts();
        }

        /// <summary>
        /// Service should return product if correct id is supplied
        /// </summary>
        [Test]
        public void Given_a_matching_product_code_When_found_in_repository_Then_retrieve_product_from_repository()
        {
            var mobileProduct = _productService.GetProductByCode("A001");
            if (mobileProduct != null)
            {
                Mapper.CreateMap<ProductModel, Product>();

                var productModel = Mapper.Map<ProductModel,Product>(mobileProduct);
                AssertObjects.PropertyValuesAreEquals(productModel,
                _products.Find(a => a.ProductCode.Contains("A001")));
            }
        }

        /// <summary>
        /// Service should return null
        /// </summary>
        [Test]
        public void Given_a_product_code_When_not_found_in_repository_Then_do_not_retrieve_any_product_from_repository()
        {
            var product = _productService.GetProductByCode("test");
            Assert.Null(product);
        }

        /// <summary>
        /// Add new product test
        /// </summary>
        [Test]
        public void Given_a_product_repository_When_added_new_product_Then_retrieve_the_newly_added_product_from_repository()
        {
           
            var newProduct = new ProductModel()
            {
                ProductCode = "B002",
                Price = 120
            };

            _productService.CreateProduct(newProduct);
            var addedproduct = new Product() { ProductCode = newProduct.ProductCode, Price = newProduct.Price };
            Assert.AreEqual(4, _products.Count);
            Assert.IsNotNull(_products.Find(a => a.ProductCode.Contains("B002")));
            
        }

        
        
        #endregion


        #region Tear Down

        /// <summary>
        /// Tears down each test data
        /// </summary>
        [TearDown]
        public void DisposeTest()
        {
            _productService = null;
            _unitOfWork = null;
            _productRepository = null;
            if (_dbEntities != null)
                _dbEntities.Dispose();
        }

        #endregion

        #region TestFixture TearDown.

        /// <summary>
        /// TestFixture teardown
        /// </summary>
        [OneTimeTearDown]
        public void DisposeAllObjects()
        {
            _products = null;
        }

        #endregion
    }
}
