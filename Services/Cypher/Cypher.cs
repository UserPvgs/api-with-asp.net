using System.Security.Cryptography;
using System.Text;

namespace Services.Cypher{
    public class CryptoServiceApi{
        public string Encrypt(string str){
            using(var hmac = new HMACSHA512()){
                var salt = hmac.Key;
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(str));
                return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
            };
        }
        public bool CompareHash(string str, string encryptStr){
            var parts = encryptStr.Split(':');
            var salt = Convert.FromBase64String(parts[0]);
            var hash = Convert.FromBase64String(parts[1]);
            using(var hmac = new HMACSHA512(salt)){
                var strHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(str));
                return Convert.ToBase64String(strHash) == parts[1];
            }
        }
    }
}