using SameBoringToDoList.Domain.ValueObjects;
using SameBoringToDoList.Shared.Domain.Primitives;
using System.Security.Cryptography;

namespace SameBoringToDoList.Domain.Entities
{
    public class Credential: Entity<CredentialId>
    {
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }

        public Credential(CredentialId id, byte[] passwordHash, byte[] passwordSalt): base(id)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public Credential(CredentialId id, Password password): base(id)
        {
            CreatePasswordHash(password.Value, out byte[] passwordHash, out byte[] passwordSalt);

            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public bool ValidatePassword(string password)
        {
            using (var hmac = new HMACSHA512(PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(PasswordHash);
            }
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
