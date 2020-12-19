using ReportWebsite.Entities;
using ReportWebsite.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReportWebsite.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "لطفا نام کاربری را وارد کنید .")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "لطفا رمزعبور را وارد کنید .")]
        public string Password { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        private EN_Role _role { get; set; }
        private bool _get;
        public EN_Role Role {
            get
            {
                if (!_get && _role == null)
                {
                   

                    var repo = new RoleRepository();
                    _role = repo.GetRole(RoleId);

                    _get = true;
                }

                return _role;
            }
            set
            {
                _role = value;
                _get = true;
            }
        }
        public DateTime CreateDate { get; set; }
        public bool IsLock { get; set; }
        public string Email { get; set; }

        public static implicit operator User(Entities.EN_User model)
        {
            if (model == null) return null;
            return new Entities.EN_User {
                CreateDate = model.CreateDate,
                Email = model.Email,
                FirstName = model.FirstName,
                Id = model.Id,
                IsLock = model.IsLock,
                LastName = model.LastName,
                Password = model.Password,
                Phone = model .Phone ,
                RoleId = model.RoleId,
                UserName = model.UserName
            };
           
        }
        public Entities.EN_User ToUser()
        {
            return new Entities.EN_User()
            {
                UserName = UserName,
                RoleId = RoleId,
                CreateDate = CreateDate,
                Email = Email,
                FirstName = FirstName,
                Id = Id,
                IsLock = IsLock ,
                LastName=LastName,
                Password=Password,
                Phone=Phone
            };
        }
    }
}