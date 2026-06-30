namespace HealthFlow.Services
{
    public class AuthService
    {
        private bool _isAuthenticated = false;
        private string _userEmail = "";

        public bool IsAuthenticated => _isAuthenticated;
        public string UserEmail => _userEmail;

        public async Task<bool> LoginAsync(string email, string password)
        {
            
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            
            await Task.Delay(500); 

            _isAuthenticated = true;
            _userEmail = email;
            return true;
        }

        public void Logout()
        {
            _isAuthenticated = false;
            _userEmail = "";
        }
    }
}
