using UdemyUnitTest.Intro;

var userManagement = new UserManagement(new UserManager());

var result = userManagement.Add(new User
{
	Id = "Naber",
	Name= "Test",
	Title= "Test",
});

Console.ReadLine();
