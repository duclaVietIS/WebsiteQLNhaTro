using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using WebsiteQLNhaTro.DTOs.statistics;
using WebsiteQLNhaTro.Models;

namespace WebsiteQLNhaTro.Services
{
    public class EmailService
    {
        private readonly SmtpSettings _smtpSettings;
        
        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendUnpaidRoomNotification(string toEmail, UnpaidRoomStatisticsDto room)
        {
            var smtpClient = new SmtpClient(_smtpSettings.Host)
            {
                Port = _smtpSettings.Port,
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpSettings.FromEmail, "Quản lý nhà trọ"),
                Subject = "Thông báo tiền phòng chưa thanh toán đủ",
                IsBodyHtml = true,
                Body = $@"
                    <h2>Thông báo tiền phòng chưa thanh toán đủ</h2>
                    <p>Kính gửi {room.TenantName},</p>
                    <p>Chúng tôi thông báo phòng {room.RoomNumber} của bạn còn khoản tiền chưa thanh toán của tháng trước:</p>
                    <ul>
                        <li>Tổng tiền: {room.TotalPrice:N0} VNĐ</li>
                        <li>Đã thanh toán: {room.TotalPaid:N0} VNĐ</li>
                        <li>Còn nợ: {room.RemainingDebt:N0} VNĐ</li>
                    </ul>
                    <p>Vui lòng thanh toán số tiền còn lại trong thời gian sớm nhất.</p>
                    <p>Trân trọng,</p>
                    <p>Ban quản lý</p>"
            };
            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}