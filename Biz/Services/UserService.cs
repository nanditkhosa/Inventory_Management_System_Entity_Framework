using System.Linq;
using System;
using System.Net.Mail;
using Biz.Interfaces;
using Core.Domains;
using Data.Repositories;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
using Core.Helpers.Security;

namespace Biz.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepo;

        public UserService()
        {

            _userRepo = new UserRepository();
        }

        public UserService(UserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public IQueryable<User> GetAll()
        {
            return _userRepo.UserTable;
        }

        public IQueryable<User> GetAllInactive()
        {
            return _userRepo.InactiveUserTable;
        }

        public User GetByUserName(string mail)
        {
            return _userRepo.GetUserByUserName(mail);            
        }

        public User GetById(int id)
        {
            return _userRepo.GetUserByUserId(id);
        }

        public void Insert(User user, List<int> listOfFacilityIds)
        {
            if (user.Id == 0)
            {
                DateTime currentdateTime = new DateTime();
                user.CreatedTimeStamp = currentdateTime;
                user.LastModifiedTimeStamp = currentdateTime;
                sendEmailToUser(user);
                _userRepo.InsertNewUserForExisitingFacility(user, listOfFacilityIds);
            }
        }

        public void Update(User user, List<int> ListOfFacilityIds)
        {
            DateTime currentdateTime = new DateTime();
            user.LastModifiedTimeStamp = currentdateTime;
            _userRepo.UpdateExisitingUserWithFacility(user, ListOfFacilityIds);
        }

        public void sendEmailToUser(User user)
        {
            // Command line argument must the the SMTP host.
            SmtpClient client = new SmtpClient();
            string Body = "Please use below credential to login to IMS.\n\n Your Login Id is:   "+user.UserName+"\n Your Password is:     " + user.PasswordHash;
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("noreplyinventory13@gmail.com", "Employee#123");

            MailMessage mm = new MailMessage("noreplyinventory13@gmail.com", user.UserName, "PasswordDetails", Body);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);
        }

    }
}
