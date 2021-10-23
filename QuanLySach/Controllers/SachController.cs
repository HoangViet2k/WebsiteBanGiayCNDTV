using QuanLySach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace QuanLySach.Controllers
{
    public class SachController : ApiController
    {
        Sach[] sachs = new Sach[]
        {
            new Sach{Id =1 ,Title="Tôi thấy hoa vàng trên cỏ xanh", AuthorName="Nguyễn Nhật Anh"
            ,Price=1, Content="Truyện kể về tuổi thơ..."},
            new Sach{Id =2 ,Title="Pro ASP.NET MVC5", AuthorName="Adam Freeman"
            ,Content="The ASP.NET MVC 5 Framework is the latest evolution of Microsoft’s ASP.NET web platform" ,Price=3.75M},
        };
        public IEnumerable<Sach> GetAll()
        {
            return sachs;
        }

        private static string removeVietnameseTone(string text)
        {
            string result = text.Trim().ToLower();
            result = Regex.Replace(result, "à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ|/g", "a");
            result = Regex.Replace(result, "è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ|/g", "e");
            result = Regex.Replace(result, "ì|í|ị|ỉ|ĩ|/g", "i");
            result = Regex.Replace(result, "ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ|/g", "o");
            result = Regex.Replace(result, "ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ|/g", "u");
            result = Regex.Replace(result, "ỳ|ý|ỵ|ỷ|ỹ|/g", "y");
            result = Regex.Replace(result, "đ", "d");
            return result;
        }
        public dynamic GetSach(string searchKey)
        {
            var list = new List<Sach>();
            int id = 0;
            if (int.TryParse(searchKey, out id))
            {
                foreach (var item in sachs)
                {
                    if (removeVietnameseTone(item.Title).Contains(removeVietnameseTone(searchKey))
                        || item.Id == id
                        )
                    {
                        list.Add(item);
                    }
                }
            }
            else
            {
                foreach (var item in sachs)
                {
                    if (removeVietnameseTone(item.Title).Contains(removeVietnameseTone(searchKey))
                        )
                    {
                        list.Add(item);
                    }
                }
            }
            return Ok(list);
        }
        //public IHttpActionResult GetSach(int id)
        //{
        //    var sach = sachs.FirstOrDefault((p) => p.Id == id);
        //    if (sach == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(sach);
        //}

    }
}
