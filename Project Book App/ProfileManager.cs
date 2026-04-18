using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Project_Book_App
{

    public static class ProfileManager
    {
        private static readonly string FilePath = "user_profile.json";

        public static UserProfile Load()
        {
            if (!File.Exists(FilePath))
                return new UserProfile();
            try
            {
                string json = File.ReadAllText(FilePath);
                return JsonConvert.DeserializeObject<UserProfile>(json) ?? new UserProfile();
            }
            catch
            {
                return new UserProfile();
            }
        }

        public static List<UserProfile> LoadAll()
        {
            if (!File.Exists(FilePath))
                return new List<UserProfile>();
            try
            {
                string json = File.ReadAllText(FilePath);
                return JsonConvert.DeserializeObject<List<UserProfile>>(json) ?? new List<UserProfile>();
            }
            catch
            {
                return new List<UserProfile>();
            }
        }

        public static UserProfile Find(string username)
        {
            var users = LoadAll();
            return users.FirstOrDefault(u => u.Username.ToLower() == username.ToLower());
        }

        public static void Save(UserProfile profile)
        {
            var users = LoadAll();
            var existing = users.FirstOrDefault(u => u.Username.ToLower() == profile.Username.ToLower());

            if (existing != null)
                users[users.IndexOf(existing)] = profile;
            else
                users.Add(profile);

            File.WriteAllText(FilePath, JsonConvert.SerializeObject(users, Formatting.Indented));
        }
    }
}
