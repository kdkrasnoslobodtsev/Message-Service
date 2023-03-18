using Microsoft.AspNetCore.Mvc;
using Peergrade7.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace Peergrade7.Controllers
{
    public class UserController: Controller
    {
        /// <summary>
        /// Случайным образом создает пользователей и сообщения.
        /// </summary>
        /// <returns>Результат запроса</returns>
        [HttpPost("/create-users")]
        public IActionResult CreateUsers()
        {
            List<User> users = Tools.GenerateUsers();
            users.Sort((user1, user2) => user1.Email.CompareTo(user2.Email));
            System.IO.File.WriteAllText("users.json", "[]");
            Tools.WriteUsers(users);
            List<Post> posts = Tools.GenerateMessages(users);
            System.IO.File.WriteAllText("posts.json", "[]");
            Tools.WriteMessages(posts);
            return Ok("List of users and list of messages are successfully created");
        }

        /// <summary>
        /// Находит пользователя с заданным Email.
        /// </summary>
        /// <param name="email">Идентификатор для поиска</param>
        /// <returns>Результат запроса</returns>
        [HttpGet("find-user")]
        public IActionResult FindUser(string email)
        {
            List<User> users = Tools.OpenUsers();
            if (users.FindIndex(user => user.Email == email) == -1)
            {
                return NotFound($"Cannot find user with E-mail: {email}");
            }
            else
            {
                return Ok(users[users.FindIndex(user => user.Email == email)]);
            }
        }

        /// <summary>
        /// Вывод вссех пользователей.
        /// </summary>
        /// <returns>Результат запроса</returns>
        [HttpGet("show-all-users")]
        public IActionResult ShowUsers()
        {
            List<User> users = Tools.OpenUsers();
            return Ok(users);
        }

        /// <summary>
        /// Добавить пользователя.
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="email">Почта пользователя</param>
        /// <returns>Результат запроса</returns>
        [HttpPost("create-user")]
        public IActionResult CreateUser(string userName, string email)
        {
            List<User> users = Tools.OpenUsers();
            if (users.FindIndex(user => user.Email == email) != -1)
            {
                return Conflict($"User wth Email {email} already exists");
            }
            else
            {
                User newUser = new User(userName, email);
                users.Add(newUser);
                users.Sort((user1, user2) => user1.Email.CompareTo(user2.Email));
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<User>));
                System.IO.File.WriteAllText("users.json", "[]");
                Tools.WriteUsers(users);
                return Ok(newUser);
            }
        }
    }
}
