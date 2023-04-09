using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyUnitTest.Intro.XUnitTest
{
	public class UserManagementTest
	{
		private readonly Mock<IUserService> _mockUserManager;
		private readonly UserManagement _userManagement;

		public UserManagementTest()
		{
			// UserManagement di gerekliligi mock ile saglandi
			_mockUserManager = new Mock<IUserService>();
			_userManagement = new UserManagement(_mockUserManager.Object);
		}

		[Fact]
		public void Add_NewUser_ReturnNewUser()
		{ 
			// ARRANGE
			var expected = new User { 
				Name = "Test Title",
				Title= "Test Title",
			};
			User actual = null;
			
			// ACT

			// add metodu verilen deger ile calistirilir ise bunu don
			// taklit edilen metod calismaz, beklenen sonucu mock doner
			// cok uzun bir api istegi olsun ama donecegi sonuc bilinsin
			// bu durumda taklit edilmesi uygundur
			_mockUserManager.Setup(x => x.Add(expected)).Returns(expected);

			// add metodunu verile deger ile calistir
			// di si taklit edilen manager metodu calismaz, taklit sonucu doner
			// userManagement class inin metodu calisir
			actual = _userManagement.Add(expected);

			// ASSERT
			
			// umulan ve gercek ayni mi
			Assert.Equal(expected, actual);

			// kac kere calisti kontrolu
			_mockUserManager.Verify(x => x.Add(expected),Times.Once);
		}

		[Theory]
		[InlineData("DummyId")]
		public void Remove_Id_ReturnBoolen(string id)
		{
			// arrange

			// sadece id degeri icin simule edildi, "idx" geceli olmaz 
			//_mockUserManager
			//	.Setup(x => x.Remove(id))
			//	.Returns(true);

			// tum string degerleri icin sumile etti
			var result = false;
			_mockUserManager
				.Setup(x => x.Remove(It.IsAny<string>()))
				.Callback<string>((v) => result = v == v);

			// act
			// yukarida atanan callback fonksiyonu tetikler
			// result atanir
			_userManagement.Remove(id);
			Assert.True(result);

			// farkli degerler icin ikinci bir kontrol saglandi
			_userManagement.Remove(id + "x");
			Assert.True(result);
		}
	}
}
