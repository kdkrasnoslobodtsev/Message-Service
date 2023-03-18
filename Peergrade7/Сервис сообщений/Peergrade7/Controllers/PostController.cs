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
    public class PostController: Controller
    {
        /// <summary>
        /// Находит сообщение по заданному отправителю и получателю.
        /// </summary>
        /// <param name="sender">Идентификатор отправителя</param>
        /// <param name="receiver">Идентификатор получателя</param>
        /// <returns>Результат запроса</returns>
        [HttpGet("find-messages")]
        public IActionResult FindMessages(string sender, string receiver)
        {
            List<Post> posts = Tools.OpenMessages();
            List<Post> result = posts.FindAll(post => post.SenderId == sender && post.ReceiverId == receiver);
            if (result.Count == 0)
            {
                return NotFound("Cannot find message with such sender and receiver");
            }
            else
            {
                return Ok(result);
            }
        }

        /// <summary>
        /// Находит сообщения по заданному отправителю.
        /// </summary>
        /// <param name="sender">Идентификатор отправителя</param>
        /// <returns>Результат запроса</returns>
        [HttpGet("find-messages-by-sender")]
        public IActionResult FindMessagesBySender(string sender)
        {
            List<Post> posts = Tools.OpenMessages();
            List<Post> result = posts.FindAll(post => post.SenderId == sender);
            if (result.Count == 0)
            {
                return NotFound("Cannot find message with such sender");
            }
            else
            {
                return Ok(result);
            }
        }

        /// <summary>
        /// Находит сообщения по заданному получателю.
        /// </summary>
        /// <param name="receiver">Идентификатор получателя</param>
        /// <returns>Результат запроса</returns>
        [HttpGet("find-messages-by-receiver")]
        public IActionResult FindMessagesByReceiver(string receiver)
        {
            List<Post> posts = Tools.OpenMessages();
            List<Post> result = posts.FindAll(post => post.ReceiverId == receiver);
            if (result.Count == 0)
            {
                return NotFound("Cannot find message with such receiver");
            }
            else
            {
                return Ok(result);
            }
        }

        /// <summary>
        /// Создать сообщение.
        /// </summary>
        /// <param name="subject">Тема</param>
        /// <param name="message">Содержание</param>
        /// <param name="sender">Идентификатор отправителя</param>
        /// <param name="receiver">Идентификатор получателя</param>
        /// <returns>Результат запроса</returns>
        [HttpPost("create_message")]
        public IActionResult CreateMessage(string subject, string message, string sender, string receiver)
        {
            List<User> users = Tools.OpenUsers();
            List<Post> posts = Tools.OpenMessages();
            if (users.FindIndex(user => user.Email == sender) == -1)
            {
                return NotFound($"User with Email {sender} does not exist");
            }
            else if (users.FindIndex(user => user.Email == receiver) == -1)
            {
                return NotFound($"User with Email{receiver} does not exist");
            }
            else
            {
                Post newPost = new Post(subject, message, sender, receiver);
                posts.Add(newPost);
                System.IO.File.WriteAllText("posts.json", "[]");
                Tools.WriteMessages(posts);
                return Ok(newPost);
            }
        }
    }
}
