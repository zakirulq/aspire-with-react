using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskManagementApp.ApiService.Models
{
    public class TaskItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DueDate { get; set; }
    }
} 