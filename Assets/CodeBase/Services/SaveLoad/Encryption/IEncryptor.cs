using System;

namespace BlackBall.Services.SaveLoad.Encryption
{
    public interface IEncryptor
    {
        public byte[] Encrypt(byte[] data);
        public byte[] Decrypt(byte[] encryptedData);
    }

    public class Encryptor : IEncryptor
    {
        public byte[] Encrypt(byte[] data)
        {
            throw new System.NotImplementedException();
        }

        public byte[] Decrypt(byte[] encryptedData)
        {
            throw new System.NotImplementedException();
        }
    }
}