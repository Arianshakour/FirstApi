﻿namespace FirstApi.Services.Interfaces
{
    public interface IMailService
    {
        void Send(string subject, string message);
    }
}