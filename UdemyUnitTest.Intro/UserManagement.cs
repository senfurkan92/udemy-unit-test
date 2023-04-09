using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyUnitTest.Intro
{
	public interface IUserService
	{
		User Add(User user);
		LinkedList<User> GetAll();
		User GetOne(string id);
		bool Remove(string id);
		User Update(User user);
	}

	public class UserManager : IUserService
	{
		private static LinkedList<User> Users { get; set; } = new LinkedList<User>();

		public User Add(User user)
		{
			Users.AddLast(user);
			return Users.Single(x => x.Id == user.Id);
		}

		public User Update(User user)
		{
			var current = Users.FirstOrDefault(x => x.Id == user.Id);
			current = user;
			return current;
		}

		public bool Remove(string id)
		{
			var current = Users.FirstOrDefault(x => x.Id == id);
			Users.Remove(current);
			return !Users.Any(x => x.Id == id);
		}

		public LinkedList<User> GetAll()
		{
			return Users;
		}

		public User GetOne(string id)
		{
			var current = Users.FirstOrDefault(x => x.Id == id);
			return current;
		}
	}

	public class UserManagement
	{
		private readonly IUserService _userManager; 

		public UserManagement(IUserService userManager)
		{
			_userManager = userManager;
		}

		public User Add(User user) => _userManager.Add(user);

		public User Update(User user) => _userManager.Update(user);

		public bool Remove(string id) => _userManager.Remove(id);

		public LinkedList<User> GetAll() => _userManager.GetAll();

		public User GetOne(string id) => _userManager.GetOne(id);
	}

	public class User
	{
		public string Id { get; set; } = Guid.NewGuid().ToString();

		public string Name { get; set; }

		public string Title { get; set; }
	}
}
