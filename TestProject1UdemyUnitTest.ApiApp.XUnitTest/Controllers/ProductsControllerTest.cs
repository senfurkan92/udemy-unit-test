using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyUnitTest.ApiApp.Controllers;
using UdemyUnitTest.Dal.Data;
using UdemyUnitTest.Dal.Models;
using Xunit;

namespace TestProject1UdemyUnitTest.ApiApp.XUnitTest.Controllers
{
	public class ProductsControllerTest
	{
		private readonly Mock<IRepository<Product>> _mockProductManager;
		private readonly ProductsController _productController;

		public List<Product> Products { get; set; }

		public ProductsControllerTest()
		{
			_mockProductManager = new Mock<IRepository<Product>>();
			_productController = new ProductsController(_mockProductManager.Object);

			Products = new List<Product> {
				new () {
					ProductId= 1,
					ProductName = "Trek 820 - 2016",
					BrandId = 9,
					CategoryId = 6,
					ModelYear = 2016,
					ListPrice = 379.99M,
				},
				new () {
					ProductId= 2,
					ProductName = "Ritchey Timberwolf Frameset - 2016",
					BrandId = 5,
					CategoryId = 6,
					ModelYear = 2016,
					ListPrice = 749.99M,
				},
				new () {
					ProductId= 3,
					ProductName = "Surly Wednesday Frameset - 2016",
					BrandId = 8,
					CategoryId = 6,
					ModelYear = 2016,
					ListPrice = 999.99M,
				},
				new () {
					ProductId= 4,
					ProductName = "Trek Fuel EX 8 29 - 2016",
					BrandId = 9,
					CategoryId = 6,
					ModelYear = 2016,
					ListPrice = 2899.99M,
				},
			};
		}

		[Fact]
		public async void GetProducts_ActionExecutes_ReturnOkWithProducts()
		{
			// arrange
			_mockProductManager.Setup(x => x.GetAllAsync()).ReturnsAsync(Products);

			// act
			var result = await _productController.GetProducts();
			var okResult = Assert.IsType<OkObjectResult>(result);
			var returnProducts = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);

			Assert.Equal(Products.Count, returnProducts.Count());
		}

		[Theory]
		[InlineData(1)]
		[InlineData(3)]
		public async void GetProduct_ActionExecutes_ReturnOkWithProduct(int id)
		{
			_mockProductManager
				.Setup(x => x.GetByIdAsync(id))
				.ReturnsAsync(Products.First(x => x.ProductId == id));

			var result = await _productController.GetProduct(id);

			var resultType = Assert.IsType<OkObjectResult>(result);
			var returnProduct = Assert.IsAssignableFrom<Product>(resultType.Value);

			Assert.NotNull(returnProduct);
		}

		[Theory]
		[InlineData(5)]
		public async void GetProduct_ActionExecutes_ReturnNotFound(int id)
		{
			_mockProductManager
				.Setup(x => x.GetByIdAsync(id))
				.ReturnsAsync(Products.FirstOrDefault(x => x.ProductId == id));

			var result = await _productController.GetProduct(id);

			var resultType = Assert.IsType<NotFoundResult>(result);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(3)]
		[InlineData(0)]
		public async void GetProduct_ActionExecutes_ReturnAllSituation(int id)
		{
			Product product = null;

			_mockProductManager
				.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
				.Callback<int>((v) => product = Products.FirstOrDefault(x => x.ProductId == v));

			var result = await _productController.GetProduct(id);

			if (result is OkObjectResult)
			{
				var returnType = Assert.IsType<OkObjectResult>(result);
				var returnProduct = Assert.IsAssignableFrom<Product>(returnType.Value);
				Assert.NotNull(returnProduct);
			} 
			else if (result is NotFoundResult)
			{
				Assert.IsType<NotFoundResult>(result);
			}
			else { Assert.Fail("Out of expected result situations"); }
		}
	}
}
