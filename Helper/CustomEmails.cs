namespace OISPublic.Helper
{
    public class CustomEmails
    {



        public string GenerateEmailBodyForFirstTimeLogin(string email)
        {
            var name = email.Split('@')[0];
            var loginTime = DateTime.Now.ToString("f");

            return $@"
<!DOCTYPE html>
<html>
<head>
<style>
    body {{
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        background-color: #f4f6f8;
        color: #333;
        margin: 0;
        padding: 0;
    }}
    .container {{
        max-width: 600px;
        margin: 30px auto;
        background-color: #ffffff;
        padding: 30px;
        border-radius: 10px;
        box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
    }}
    .banner {{
        width: 100%;
        border-radius: 10px 10px 0 0;
    }}
    h2 {{
        color: #2c3e50;
        font-size: 22px;
    }}
    p {{
        font-size: 16px;
        line-height: 1.6;
    }}
    .highlight {{
        background-color: #eaf4ff;
        padding: 15px;
        border-left: 4px solid #007bff;
        margin-top: 20px;
        border-radius: 8px;
    }}
    .footer {{
        text-align: center;
        margin-top: 40px;
        font-size: 13px;
        color: #777;
    }}
    .footer img {{
        width: 180px;
        margin-top: 10px;
    }}
</style>
</head>
<body>
    <div class='container'>
        <img src='https://nexavault.s3.ap-south-1.amazonaws.com/Documents/Media.jpg/b7657b67-8c10-43ab-a7f6-c319fa050d57/Media.jpg' class='banner' alt='Nexa Vault Banner'/>
        <h2>Welcome, {name}!</h2>
        <p>We're excited to let you know that you've successfully logged in to the <strong>Nexa Vault DataRoom</strong> for the first time.</p>

        <div class='highlight'>
            <p><strong>Login Time:</strong> {loginTime}</p>
            <p><strong>Email:</strong> {email}</p>
        </div>

        <p>You now have access to securely manage your documents, collaborate with your team, and explore powerful features.</p>
        
        <p>If you have any questions or need help, feel free to reach out to our support team.</p>

        <p>Thanks for joining Nexa Vault!</p>

        <div class='footer'>
            <p>© 2025 Nexa Vault DataRoom, India</p>
            <img src='https://nexavault.s3.ap-south-1.amazonaws.com/Documents/Media.jpg/b7657b67-8c10-43ab-a7f6-c319fa050d57/NexaVault-logo.png' alt='Nexa Vault Logo'/>
        </div>
    </div>
</body>
</html>";
        }

        public string GenerateEmailBodyForNewPassword(string email, string password)
        {
            var name = email.Split('@')[0];

            return $@"
<!DOCTYPE html>
<html>
<head>
<style>
    body {{
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        background-color: #f9fbfd;
        color: #333;
        margin: 0;
        padding: 0;
    }}
    .container {{
        max-width: 600px;
        margin: 30px auto;
        background-color: #ffffff;
        padding: 30px;
        border-radius: 10px;
        box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
    }}
    .banner {{
        width: 100%;
        border-radius: 10px 10px 0 0;
    }}
    h2 {{
        color: #2c3e50;
        font-size: 22px;
    }}
    p {{
        font-size: 16px;
        line-height: 1.6;
    }}
    .highlight {{
        background-color: #e8f4ff;
        padding: 15px;
        border-left: 4px solid #007bff;
        margin-top: 20px;
        border-radius: 8px;
        font-family: monospace;
    }}
    .footer {{
        text-align: center;
        margin-top: 40px;
        font-size: 13px;
        color: #777;
    }}
    .footer img {{
        width: 160px;
        margin-top: 10px;
    }}
</style>
</head>
<body>
    <div class='container'>
        <img src='https://nexavault.s3.ap-south-1.amazonaws.com/Documents/Media.jpg/b7657b67-8c10-43ab-a7f6-c319fa050d57/Media.jpg' class='banner' alt='Nexa Vault Banner'/>
        <h2>Hello {name},</h2>
        <p>Your login credentials for <strong>Nexa Vault DataRoom</strong> have been generated.</p>

        <div class='highlight'>
            <p><strong>Email:</strong> {email}</p>
            <p><strong>New Password:</strong> {password}</p>
        </div>

        <p>We recommend that you log in and change your password immediately for security purposes.</p>
        <p>Thank you for using Nexa Vault DataRoom.</p>

        <div class='footer'>
            <p>© 2025 Nexa Vault DataRoom, India</p>
            <img src='https://nexavault.s3.ap-south-1.amazonaws.com/Documents/Media.jpg/b7657b67-8c10-43ab-a7f6-c319fa050d57/NexaVault-logo.png' alt='Nexa Vault Logo'/>
        </div>
    </div>
</body>
</html>";
        }


    }
}
