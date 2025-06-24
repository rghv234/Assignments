﻿namespace EasypayBackend.Dtos
{
    public class AuditLogDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
        public string Details { get; set; }
    }
}
