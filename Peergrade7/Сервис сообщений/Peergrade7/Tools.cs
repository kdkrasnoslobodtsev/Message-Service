using Peergrade7.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace Peergrade7
{
    public static class Tools
    {
        /// <summary>
        /// Создать строку случайным образом.
        /// </summary>
        /// <param name="length">Длина строки</param>
        /// <returns>Сгенерированная строка</returns>
        public static string RandomString(int length)
        {
            string alph = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890";
            string result = String.Empty;
            Random rnd = new Random();
            for (int i = 0; i < length; ++i)
            {
                result += alph[rnd.Next(alph.Length)];
            }
            return result;
        }

        /// <summary>
        /// Случайная генерация списка пользователей.
        /// </summary>
        /// <returns>Сгенерированный списко</returns>
        public static List<User> GenerateUsers()
        {
            List<User> result = new List<User>();
            Random rnd = new Random();
            int count = rnd.Next(1, 40);
            for (int i = 0; i < count; ++i)
            {
                int len = rnd.Next(1, 25);
                string name = RandomString(len);
                string email = RandomString(len);
                if (result.FindIndex(x => x.Email == email) == -1)
                {
                    result.Add(new User(name, email));
                }
                else
                {
                    i--;
                }
            }
            return result;
        }

        /// <summary>
        /// Случайна генерация списка сообщений.
        /// </summary>
        /// <param name="users">Список пользователей</param>
        /// <returns>Сгенерированный список сообщений</returns>
        public static List<Post> GenerateMessages(List<User> users)
        {
            List<Post> result = new List<Post>();
            Random rnd = new Random();
            int count = rnd.Next(1, 70);
            for (int i = 0; i < count; ++i)
            {
                Random subject = new Random();
                Random message = new Random();
                Random sender = new Random();
                Random receiver = new Random();
                result.Add(new Post(
                    RandomString(subject.Next(1, 20)),
                    RandomString(message.Next(1, 70)),
                    users[sender.Next(0, users.Count)].Email,
                    users[receiver.Next(0, users.Count)].Email
                ));
            }
            return result;
        }

        /// <summary>
        /// Читает список пользователей из JSON-файла.
        /// </summary>
        /// <returns>Список пользователей</returns>
        public static List<User> OpenUsers()
        {
            List<User> result = new List<User>();
            using (var fs = new FileStream("users.json", FileMode.OpenOrCreate, FileAccess.Read))
            {
                var formatter = new DataContractJsonSerializer(typeof(List<User>));
                result = (List<User>)formatter.ReadObject(fs);
            }
            return result;
        }

        /// <summary>
        /// Читает список сообщений из JSON-файла.
        /// </summary>
        /// <returns>Список сообщений</returns>
        public static List<Post> OpenMessages()
        {
            List<Post> result = new List<Post>();
            using (var fs = new FileStream("posts.json", FileMode.OpenOrCreate, FileAccess.Read))
            {
                var formatter = new DataContractJsonSerializer(typeof(List<Post>));
                result = (List<Post>)formatter.ReadObject(fs);
            }
            return result;
        }

        /// <summary>
        /// Записывает список пользователей в JSON-файл.
        /// </summary>
        /// <param name="users">Список пользователей</param>
        public static void WriteUsers(List<User> users)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<User>));
            using (FileStream fs = new FileStream("users.json", FileMode.OpenOrCreate))
            {
                serializer.WriteObject(fs, users);
            }
        }

        /// <summary>
        /// Записывает список сообщений в JSON-файл.
        /// </summary>
        /// <param name="posts">Список сообщений</param>
        public static void WriteMessages(List<Post> posts)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Post>));
            using (FileStream fs = new FileStream("posts.json", FileMode.OpenOrCreate))
            {
                serializer.WriteObject(fs, posts);
            }
        }
    }
}
