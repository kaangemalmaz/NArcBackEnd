using NArcBackEnd.Business.Repositories.EmailParameterRepository.Constants;
using NArcBackEnd.Core.Utilities.Result.Abstract;
using NArcBackEnd.Core.Utilities.Result.Concrete;
using NArcBackEnd.DataAccess.Repositories.EmailParameterRepository;
using NArcBackEnd.Entities.Concrete;
using System.Net;
using System.Net.Mail;

namespace NArcBackEnd.Business.Repositories.EmailParameterRepository
{
    public class EmailParameterManager : IEmailParameterService
    {
        private readonly IEmailParameterDal _emailParameterDal;

        public EmailParameterManager(IEmailParameterDal emailParameterDal)
        {
            _emailParameterDal = emailParameterDal;
        }

        public IResult Add(EmailParameter emailParameter)
        {
            //kontroller.
            _emailParameterDal.Add(emailParameter);
            return new SuccessResult(EmailParameterMessages.AddedEmailParameter);
        }

        public IResult Delete(EmailParameter emailParameter)
        {
            _emailParameterDal.Delete(emailParameter);
            return new SuccessResult(EmailParameterMessages.DeletedEmailParameter);
        }

        public IDataResult<EmailParameter> GetById(int id)
        {
            return new SuccessDataResult<EmailParameter>(_emailParameterDal.Get(o => o.Id == id));
        }

        public IDataResult<List<EmailParameter>> GetList()
        {
            return new SuccessDataResult<List<EmailParameter>>(_emailParameterDal.GetAll());
        }

        public IResult SendEmail(EmailParameter emailParameter, string body, string subject, string emails)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                string[] setEmails = emails.Split(",");

                mailMessage.From = new MailAddress(emailParameter.Email);

                foreach (var email in setEmails) // birden fazla email e gönderim yapılacak!
                {
                    mailMessage.To.Add(email);
                }

                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = emailParameter.Html;

                using (SmtpClient smtpClient = new SmtpClient())
                {
                    smtpClient.Port = emailParameter.Port;
                    smtpClient.EnableSsl = emailParameter.SSL;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(emailParameter.Email, emailParameter.Password);
                    smtpClient.Send(mailMessage);
                }

                return new SuccessResult(EmailParameterMessages.EmailSendSuccessfully);
            }
        }

        public IResult Update(EmailParameter emailParameter)
        {

            _emailParameterDal.Update(emailParameter);
            return new SuccessResult(EmailParameterMessages.UpdatedEmailParameter);
        }
    }
}
