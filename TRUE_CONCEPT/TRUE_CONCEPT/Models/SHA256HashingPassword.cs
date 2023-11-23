using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TRUE_CONCEPT.Models
{
	public class SHA256HashingPassword
	{
        public static string GetSHA256Hash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Chuyển đổi input thành mảng byte và tính toán mã băm
                byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Tạo một StringBuilder để chứa các byte đã được mã hóa thành chuỗi hex
                StringBuilder stringBuilder = new StringBuilder();

                // Chuyển đổi mỗi byte trong mảng băm thành chuỗi hex và thêm vào stringBuilder
                for (int i = 0; i < data.Length; i++)
                {
                    stringBuilder.Append(data[i].ToString("x2"));
                }

                // Trả về chuỗi đã được mã hóa SHA-256
                return stringBuilder.ToString();
            }
        }
    }
}