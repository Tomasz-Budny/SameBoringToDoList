using SameBoringToDoList.Shared.Domain.Primitives;
using SameBoringToDoList.Shared.Errors;
using System.Security.Cryptography;

namespace SameBoringToDoList.Domain.ValueObjects
{
    public record Credential
    {
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }

        public Credential(byte[] passwordHash, byte[] passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public static Result<Credential> Create(Password password)
        {
            CreatePasswordHash(password.Value, out byte[] passwordHash, out byte[] passwordSalt);

            return new Credential(passwordHash, passwordSalt);
        }

        public bool ValidatePassword(string password)
        {
            using (var hmac = new HMACSHA512(PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(PasswordHash);
            }
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
