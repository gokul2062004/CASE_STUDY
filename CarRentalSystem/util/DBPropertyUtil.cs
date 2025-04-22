using System.IO;
using System.Collections.Generic;

namespace CarRentalSystem.util
{
    public class DBPropertyUtil
    {
        public static Dictionary<string, string> GetProperties(string filePath)
        {
            var properties = new Dictionary<string, string>();
            foreach (var line in File.ReadAllLines(filePath))
            {
                if (!string.IsNullOrWhiteSpace(line) && line.Contains("="))
                {
                    var keyValue = line.Split('=');
                    properties[keyValue[0].Trim()] = keyValue[1].Trim();
                }
            }
            return properties;
        }
    }
}
